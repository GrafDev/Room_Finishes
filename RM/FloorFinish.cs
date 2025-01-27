﻿using System;
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
    public class FloorFinish : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument UIdoc = commandData.Application.ActiveUIDocument;
            Document doc = UIdoc.Document;

            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    // Add Your Code Here
                    FinishFloor(UIdoc, tx);
                    // Return Success
                    return Result.Succeeded;
                }

                catch (Autodesk.Revit.Exceptions.OperationCanceledException exceptionCanceled)
                {
                    message = exceptionCanceled.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                catch (ErrorMessageException errorEx)
                {
                    // checked exception need to show in error messagebox
                    message = errorEx.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
                catch (Exception ex)
                {
                    // unchecked exception cause command failed
                    message = Util.GetLanguageResources.GetString("floor_unexpectedError", Util.Cult) + ex.Message;
                    //Trace.WriteLine(ex.ToString());
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
            }
        }

        void FinishFloor(UIDocument UIDoc, Transaction tx)
        {
            Document document = UIDoc.Document;
            
            FloorsSetup floorsFinishesSetup = new FloorsSetup();
            //Load the selection form

            FloorDialogBox floorsFinishesControl = new FloorDialogBox(UIDoc, floorsFinishesSetup);

                floorsFinishesControl.InitializeComponent();

                if (floorsFinishesControl.ShowDialog() == true)
                {
                    CreateFloors(document, floorsFinishesSetup, tx);
                }
                else
                {
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                }

        }

        public void CreateFloors(Document document, FloorsSetup floorsFinishesSetup, Transaction tx)
        {
            tx.Start(Util.GetLanguageResources.GetString("floor_transactionName", Util.Cult));

            foreach (Room room in floorsFinishesSetup.SelectedRooms)
            {
                if (room != null)
                {
                    if (room.UnboundedHeight != 0)
                    {
                        //Get all finish properties
                        double height;
                        
                        Parameter roomParameter = room.get_Parameter(floorsFinishesSetup.RoomParameter.Definition);
                        height = roomParameter.AsDouble()+ floorsFinishesSetup.OffsetFloorHeight;
                        
                        SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();

                        IList<IList<Autodesk.Revit.DB.BoundarySegment>> boundarySegments = room.GetBoundarySegments(opt);

                        CurveArray curveArray = new CurveArray();

                        if (boundarySegments.Count != 0)
                        {
                            foreach (Autodesk.Revit.DB.BoundarySegment boundSeg in boundarySegments.First())
                            {
                                curveArray.Append(boundSeg.GetCurve());
                            }


                            //Retrive room info
                            Level rmLevel = document.GetElement(room.LevelId) as Level;
                            Parameter param = room.get_Parameter(BuiltInParameter.ROOM_HEIGHT);
                            double rmHeight = param.AsDouble();

                            if (curveArray.Size != 0)
                            {
                                Autodesk.Revit.DB.Floor floor = document.Create.NewFloor(curveArray, floorsFinishesSetup.SelectedFloorType, rmLevel, false);

                                //Change some param on the floor
                                param = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                                param.Set(height);
                            }
                        }
                    }
                }

            }

            tx.Commit();
        }
    }
}
