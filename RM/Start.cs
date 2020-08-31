using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using RM.Properties;
using System.Runtime.CompilerServices;
using Autodesk.Revit.Creation;
using System.Windows.Media;
using Autodesk.Windows;

namespace RM
{
    public class Start:IExternalApplication
    {

        //Создание панели и основных кнопок управления
        public Result OnStartup(UIControlledApplication application)
        {
            UIControlledApplication app = application;
            Util.GetLocalisationValues(app);
            { // Лента
                string thisAssembyPath = Assembly.GetExecutingAssembly().Location;
                string panelName = Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);
                Autodesk.Revit.UI.RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);
                ribbonPanel.Enabled = true;
                ribbonPanel.Visible = true;

                { // Выпадающее меню
                    string groupTitle = Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);
                    PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup", groupTitle);
                    PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;
                    group1.Image = GetEmbeddedImage("RM.RM2Ribbon.ico");
                    group1.LargeImage = GetEmbeddedImage("RM.RM2Ribbon.ico");


                    {
                        string classWallName = "RM.WallFinish";
                        string wallTitle = Util.GetLanguageResources.GetString("wallTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonWallData = new PushButtonData("Name1", wallTitle, thisAssembyPath, classWallName);
                        PushButton pushWallButton = group1.AddPushButton(buttonWallData) as PushButton;
                        pushWallButton.Image = GetEmbeddedImage("RM.RM2wall.ico");
                        pushWallButton.LargeImage = GetEmbeddedImage("RM.RM2wall.ico");
                    }

                    {
                        string classFroolName = "RM.FloorFinish";
                        string floorTitle = Util.GetLanguageResources.GetString("floorTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonFloorData = new PushButtonData("Name2", floorTitle, thisAssembyPath, classFroolName);
                        PushButton pushFloorButton = group1.AddPushButton(buttonFloorData) as PushButton;
                        pushFloorButton.Image = GetEmbeddedImage("RM.RM2floor.ico");
                        pushFloorButton.LargeImage = GetEmbeddedImage("RM.RM2floor.ico");
                        
                    }
                    group1.AddSeparator();
                    {
                        string classAbout = "RM.AboutWindow";
                        string aboutTitle = Util.GetLanguageResources.GetString("aboutTitle_ribbonPanel", Util.Cult);
                        PushButtonData buttonAboutData = new PushButtonData("Name3", aboutTitle, thisAssembyPath, classAbout);
                        PushButton pushAboutButton = group1.AddPushButton(buttonAboutData) as PushButton;
                        pushAboutButton.Image = GetEmbeddedImage("RM.iconParameters16.png");
                        pushAboutButton.LargeImage = GetEmbeddedImage("RM.iconParameters32.png");
                    }
                }
                return Result.Succeeded;
            }
        }

    

        static ImageSource GetEmbeddedImage(string name)// Получение иконок из сборки
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(name);
                return BitmapFrame.Create(s);
            }
            catch
            {
                return null;
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;

        }

    }
}
