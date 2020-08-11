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
            string panelName = Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);//Имя панели плагина
            string imageWallSmall = "RM.RM2_wall_Large.ico";/// Иконки комманд
            string imageWallLarge = "RM.RM2_wall_Large.ico";
            string imageFloorSmall = "RM.RM2_floor_Large.ico";
            string imageFloorLarge = "RM.RM2_floor_Large.ico";
            string imageRibbonSmall = "RM.RM2_Large.ico";
            string imageRibbonLarge = "RM.RM2_Large.ico";//Пришлось поставить маленькую иконку. Большая не помещается

            string imageAboutSmall = "RM.iconParameters16.png";
            string imageAboutLarge = "RM.iconParameters32.png";///

            string classWallName = "RM.WallFinish";// Имя Класса для маркировки
            string classFroolName = "RM.FloorFinish";//Имя класса для очистки
            string classAbout = "RM.AboutWindow";// Имя класса для параметров
            string groupTitle= Util.GetLanguageResources.GetString("groupTitle_ribbonPanel", Util.Cult);
            string wallTitle = Util.GetLanguageResources.GetString("wallTitle_ribbonPanel", Util.Cult);
            string floorTitle = Util.GetLanguageResources.GetString("floorTitle_ribbonPanel", Util.Cult);
            string aboutTitle = Util.GetLanguageResources.GetString("aboutTitle_ribbonPanel", Util.Cult);
            


            string thisAssembyPath = Assembly.GetExecutingAssembly().Location;
            Autodesk.Revit.UI.RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);//Установка панели
            ribbonPanel.Enabled = true;
            ribbonPanel.Visible = true;                 


            PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup", groupTitle);// Установка группы комманд
            group1Data.Image = GetEmbeddedImage(imageRibbonSmall);
            group1Data.LargeImage = GetEmbeddedImage(imageRibbonLarge);
            PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;

            PushButtonData buttonWallData = new PushButtonData("Name1", wallTitle, thisAssembyPath, classWallName);//Комманда стен
            PushButton pushWallButton = group1.AddPushButton(buttonWallData) as PushButton;
            pushWallButton.Image = GetEmbeddedImage(imageWallSmall);
            pushWallButton.LargeImage = GetEmbeddedImage(imageWallLarge);            
            pushWallButton.ClassName = classWallName;

            PushButtonData buttonFloorData = new PushButtonData("Name2", floorTitle, thisAssembyPath, classFroolName);//Комманда пола
            PushButton pushFloorButton = group1.AddPushButton(buttonFloorData) as PushButton;
            pushFloorButton.Image = GetEmbeddedImage(imageFloorSmall);
            pushFloorButton.LargeImage = GetEmbeddedImage(imageFloorLarge);
            pushFloorButton.ClassName = classFroolName;
            

            group1.AddSeparator();

            PushButtonData buttonAboutData = new PushButtonData("Name3", aboutTitle, thisAssembyPath, classAbout);//Команда параметров
            PushButton pushAboutButton = group1.AddPushButton(buttonAboutData) as PushButton;
            pushAboutButton.Image = GetEmbeddedImage(imageAboutSmall);
            pushAboutButton.LargeImage = GetEmbeddedImage(imageAboutLarge);
            pushAboutButton.ClassName = classAbout;

            return Result.Succeeded;

        }

        static BitmapSource GetEmbeddedImage(string name)// Получение иконок из сборки
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
