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
    class AboutWindow : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string messageErr, ElementSet elements)
        {
            UIDocument UIdoc = commandData.Application.ActiveUIDocument;
            Document doc = UIdoc.Document;
            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    Document document = doc;                   
                    //FloorsSetup floorsFinishesSetup = new FloorsSetup();
                    //Load the selection form

                    AboutWindowBox aboutWindow = new AboutWindowBox(UIdoc);

                    //aboutWindow.InitializeComponent();
                    
                    return Result.Succeeded;
                }

                catch (Autodesk.Revit.Exceptions.OperationCanceledException exceptionCanceled)
                {
                    messageErr = exceptionCanceled.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Cancelled;
                }
                catch (ErrorMessageException errorEx)
                {
                    // checked exception need to show in error messagebox
                    messageErr = errorEx.Message;
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
                catch (Exception ex)
                {
                    // unchecked exception cause command failed
                    messageErr = Util.GetLanguageResources.GetString("floor_unexpectedError", Util.Cult) + ex.Message;
                    //Trace.WriteLine(ex.ToString());
                    if (tx.HasStarted())
                    {
                        tx.RollBack();
                    }
                    return Autodesk.Revit.UI.Result.Failed;
                }
                
            }

        

        }

    }
}
