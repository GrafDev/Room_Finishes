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

namespace RM
{
    public class Start:IExternalApplication
    {
        //Создание панели и основных кнопок управления
        public Result OnStartup(UIControlledApplication application)
        {
            UIControlledApplication app = application;
            Util.GetLocalisationValues(app);
            string panelName = "Room Finish";//Имя панели плагина
            string imageWallSmall = "RM.RM2_wall_Small.png";/// Иконки комманд
            string imageWallLarge = "RM.RM2_wall_Large.png";
            string imageFloorSmall = "RM.RM2_floor_Small.png";
            string imageFloorLarge = "RM.RM2_floor_Large.png";
            string imageRibbonSmall = "RM.RM2_Small.png";
            string imageRibbonLarge = "RM.RM2_Small.png";//Пришлось поставить маленькую иконку. Большая не помещается

            string imageParSmall = "RM.iconParametersSmall.png";
            string imageParLarge = "RM.iconParametersLarge.png";///

            string classWallName = "RM.WallFinish";// Имя Класса для маркировки
            string classFroolName = "RM.FloorFinish";//Имя класса для очистки
            string classParName = "RM.Parameters";// Имя класса для параметров


            
            string thisAssembyPath = Assembly.GetExecutingAssembly().Location;
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(panelName);//Установка панели
            ribbonPanel.Enabled = true;
            ribbonPanel.Visible = true;

            PulldownButtonData group1Data = new PulldownButtonData("PulldownGroup","RM");// Установка группы комманд
            group1Data.Image = GetEmbeddedImage(imageRibbonSmall);
            group1Data.LargeImage = GetEmbeddedImage(imageRibbonLarge);
            PulldownButton group1 = ribbonPanel.AddItem(group1Data) as PulldownButton;

            PushButtonData buttonWallData = new PushButtonData("Name1", "Wall", thisAssembyPath, classWallName);//Комманда стен
            PushButton pushMarkButton = group1.AddPushButton(buttonWallData) as PushButton;
            pushMarkButton.Image = GetEmbeddedImage(imageWallSmall);
            pushMarkButton.LargeImage = GetEmbeddedImage(imageWallLarge);
            pushMarkButton.ClassName = classWallName;

            PushButtonData buttonFloorData = new PushButtonData("Name2", "Floor", thisAssembyPath, classFroolName);//Комманда пола
            PushButton pushCleanButton = group1.AddPushButton(buttonFloorData) as PushButton;
            pushCleanButton.Image = GetEmbeddedImage(imageFloorSmall);
            pushCleanButton.LargeImage = GetEmbeddedImage(imageFloorLarge);
            pushCleanButton.ClassName = classFroolName;

            group1.AddSeparator();

            PushButtonData buttonParData = new PushButtonData("Name3", "Parameters", thisAssembyPath, classParName);//Команда параметров
            PushButton pushParButton = group1.AddPushButton(buttonParData) as PushButton;
            pushParButton.Image = GetEmbeddedImage(imageParSmall);
            pushParButton.LargeImage = GetEmbeddedImage(imageParLarge);
            pushParButton.ClassName = classParName;

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
