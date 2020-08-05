using System;
using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Globalization;
using System.Resources;
using RM;

namespace RM
{
    [Transaction(TransactionMode.Manual)]
    class WallFinish: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string messageErr, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document document = uiDoc.Document;

            uiApp.Application.FailuresProcessing += new EventHandler<Autodesk.Revit.DB.Events.FailuresProcessingEventArgs>(FailuresProcessing);

            using (TransactionGroup txGroupe = new TransactionGroup(document))
            {
                using (Transaction tx = new Transaction(document))
                {
                    try
                    {
                        txGroupe.Start(Util.GetLanguageResources.GetString("roomFinishes_transactionName", Util.Cult));

                        // Мы писали мы писали, наши пальчики устали
                        // Мы немножко отдохнем и опять писать начнем

                        RoomFinish(uiDoc, tx);

                        if (tx.GetStatus() == TransactionStatus.RolledBack)
                        {
                            txGroupe.RollBack();
                        }
                        else
                        {
                            txGroupe.Assimilate();
                        }


                        uiApp.Application.FailuresProcessing -= FailuresProcessing;
                        return Result.Succeeded;
                    }
                    catch (Autodesk.Revit.Exceptions.OperationCanceledException exceptionCanceled)
                    {
                        messageErr = exceptionCanceled.Message;
                        if (tx.HasStarted())
                        {
                            tx.RollBack();
                        }
                                               uiApp.Application.FailuresProcessing -= FailuresProcessing;
                        return Autodesk.Revit.UI.Result.Cancelled;
                    }
                    catch (ErrorMessageException errorEx)
                    {
                        messageErr = errorEx.Message;
                        if (tx.HasStarted())
                        {
                            tx.RollBack();
                        }
                        uiApp.Application.FailuresProcessing -= FailuresProcessing;
                        return Autodesk.Revit.UI.Result.Failed;
                    }
                    catch (Exception ex)
                    {
                        messageErr = Util.GetLanguageResources.GetString("roomFinishes_unexpectedError", Util.Cult) + ex.Message;
                        //Trace.WriteLine(ex.ToString());
                        if (tx.HasStarted())
                        {
                            tx.RollBack();
                        }
                        uiApp.Application.FailuresProcessing -= FailuresProcessing;
                        return Autodesk.Revit.UI.Result.Failed;
                    }
                }
            }


        }
        private void RoomFinish(UIDocument uiDoc, Transaction tx)
        {
            Document doc = uiDoc.Document;
            WallSetup wallFinishSetup = new WallSetup();

            // Загрузка окна для выбора стен и параметров установки стен отделки
            WallDialogBox userControl = new WallDialogBox(uiDoc, wallFinishSetup);
            userControl.InitializeComponent();

            if (userControl.ShowDialog() == true)
            {
                CreateWallFinish(doc, tx, wallFinishSetup);
            }
            else
            {
                if (tx.HasStarted())
                {
                    tx.RollBack();
                }
            }
        }

        public void CreateWallFinish(Document doc, Transaction tx, WallSetup wallBoardSetup)
        {
            tx.Start(Util.GetLanguageResources.GetString("roomFinishes_transactionName", Util.Cult));

            WallType duplicatedWallType = DuplicateWallType(wallBoardSetup.SelectedWallType, doc);

            // Первод единиц в Футы
            double heightOffsetWall = UnitUtils.ConvertFromInternalUnits(wallBoardSetup.OffsetWallHeight, DisplayUnitType.DUT_MILLIMETERS);


           // Parameter roomParameter = room.get_Parameter(floorsFinishesSetup.RoomParameter.Definition);
          //  heightOffsetWall = roomParameter.AsDouble() + floorsFinishesSetup.OffsetHeight;

            Dictionary<ElementId, ElementId> skirtingDictionary = CreateWalls(doc,
                wallBoardSetup.SelectedRooms,
                heightOffsetWall, duplicatedWallType,wallBoardSetup);

            FailureHandlingOptions options = tx.GetFailureHandlingOptions();

            options.SetFailuresPreprocessor(new PlintePreprocessor());
            // Now, showing of any eventual mini-warnings will be postponed until the following transaction.
            tx.Commit(options);

            TransactionStatus transactionStatus = tx.GetStatus();

            if (transactionStatus != TransactionStatus.RolledBack)
            {
                tx.Start(Util.GetLanguageResources.GetString("roomFinishes_transactionName", Util.Cult));


                List<ElementId> addedIds = new List<ElementId>(skirtingDictionary.Keys);
                foreach (ElementId addedSkirtingId in addedIds)
                {
                    if (doc.GetElement(addedSkirtingId) == null)
                    {
                        skirtingDictionary.Remove(addedSkirtingId);
                    }
                }

                Element.ChangeTypeId(doc, skirtingDictionary.Keys, wallBoardSetup.SelectedWallType.Id);

                // Соединение стен
                if (wallBoardSetup.JoinWall)
                {
                    JoinGeometry(doc, skirtingDictionary);
                }

                doc.Delete(duplicatedWallType.Id);

                tx.Commit();
            }

        }

        private void JoinGeometry(Document doc, Dictionary<ElementId, ElementId> skirtingDictionary)
        {
            foreach (ElementId skirtingId in skirtingDictionary.Keys)
            {
                Wall skirtingWall = doc.GetElement(skirtingId) as Wall;

                if (skirtingWall != null)
                {
                    Parameter wallJustification = skirtingWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
                    wallJustification.Set(3);
                    Wall baseWall = doc.GetElement(skirtingDictionary[skirtingId]) as Wall;

                    if (baseWall != null)
                    {
                        JoinGeometryUtils.JoinGeometry(doc, skirtingWall, baseWall);
                    }
                }
            }
        }

        private Dictionary<ElementId, ElementId> CreateWalls(Document doc, IEnumerable<Room> modelRooms, double heightOffset, WallType newWallType,WallSetup wallFinishesSetup)
        {

            Dictionary<ElementId, ElementId> wallDictionary = new Dictionary<ElementId, ElementId>();


            // Перебор помещений для поиска границ
            foreach (Room currentRoom in modelRooms)
            {
                double VerityRoomHeight = 0;
                double height = 0;
                ElementId roomLevelId = currentRoom.LevelId;
                // Вычисление истиной высоты помещений (Объем/площадь)

                VerityRoomHeight = currentRoom.Volume / currentRoom.Area;


                SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();
                opt.SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish;
                // Проврка точки по уровню или по высоте.
                if (wallFinishesSetup.FromLevel)
                {
                    height = currentRoom.Level.Elevation+heightOffset;

                }
                else
                {
                    height = VerityRoomHeight + heightOffset;
                }
                                

                IList<IList<BoundarySegment>> boundarySegmentArray = currentRoom.GetBoundarySegments(opt);
                if (null == boundarySegmentArray)  //the room may not be bound
                {
                    continue;
                }

                foreach (IList<BoundarySegment> boundarySegArr in boundarySegmentArray)
                {
                    if (0 == boundarySegArr.Count)
                    {
                        continue;
                    }
                    else
                    {
                        //TaskDialog.Show("Check", heightOffset.ToString()+"\n"+ currentRoom.Level.Elevation.ToString());
                        foreach (BoundarySegment boundarySegment in boundarySegArr)
                        {
                            // Проверка границы на то, является ли она разделителем помещения
                            Element boundaryElement = doc.GetElement(boundarySegment.ElementId);

                            if (boundaryElement == null) { continue; }

                            Categories categories = doc.Settings.Categories;
                            Category RoomSeparetionLineCat = categories.get_Item(BuiltInCategory.OST_RoomSeparationLines);

                            if (boundaryElement.Category.Id != RoomSeparetionLineCat.Id)
                            {

                                Wall currentWall = Wall.Create(doc, boundarySegment.GetCurve(), newWallType.Id, roomLevelId, height, 0, false, false);
                                Parameter wallJustification = currentWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
                                wallJustification.Set(2);

                                wallDictionary.Add(currentWall.Id, boundarySegment.ElementId);
                            }
                        }
                    }
                }

            }

            return wallDictionary;
        }

        private WallType DuplicateWallType(WallType wallType, Document doc)
        {
            WallType newWallType;

            //Select the wall type in the document
            IEnumerable<WallType> _wallTypes = from elem in new FilteredElementCollector(doc).OfClass(typeof(WallType))
                                               let type = elem as WallType
                                               where type.Kind == WallKind.Basic
                                               select type;

            List<string> wallTypesNames = _wallTypes.Select(o => o.Name).ToList();

            if (!wallTypesNames.Contains("newWallTypeName"))
            {
                newWallType = wallType.Duplicate("newWallTypeName") as WallType;
            }
            else
            {
                newWallType = wallType.Duplicate("newWallTypeName2") as WallType;
            }

            CompoundStructure cs = newWallType.GetCompoundStructure();

            IList<CompoundStructureLayer> layers = cs.GetLayers();
            int layerIndex = 0;

            foreach (CompoundStructureLayer csl in layers)
            {
                double layerWidth = csl.Width * 2;
                if (cs.GetRegionsAssociatedToLayer(layerIndex).Count == 1)
                {
                    try
                    {
                        cs.SetLayerWidth(layerIndex, layerWidth);
                    }
                    catch
                    {
                        throw new ErrorMessageException(Util.GetLanguageResources.GetString("roomFinishes_verticallyCompoundError", Util.Cult));
                    }
                }
                else
                {
                    throw new ErrorMessageException(Util.GetLanguageResources.GetString("roomFinishes_verticallyCompoundError", Util.Cult));
                }

                layerIndex++;
            }

            newWallType.SetCompoundStructure(cs);

            return newWallType;
        }


        private void FailuresProcessing(object sender, Autodesk.Revit.DB.Events.FailuresProcessingEventArgs e)
        {
            FailuresAccessor failuresAccessor = e.GetFailuresAccessor();
            //failuresAccessor
            String transactionName = failuresAccessor.GetTransactionName();

            IList<FailureMessageAccessor> failures = failuresAccessor.GetFailureMessages();

            if (failures.Count != 0)
            {
                foreach (FailureMessageAccessor f in failures)
                {
                    FailureDefinitionId id = f.GetFailureDefinitionId();

                    if (id == BuiltInFailures.JoinElementsFailures.CannotJoinElementsError)
                    {
                        // only default option being choosen,  not good enough!
                        //failuresAccessor.DeleteWarning(f);
                        failuresAccessor.ResolveFailure(f);
                        //failuresAccessor.
                        e.SetProcessingResult(FailureProcessingResult.ProceedWithCommit);
                    }

                    return;
                }
            }

        }
    }
}
