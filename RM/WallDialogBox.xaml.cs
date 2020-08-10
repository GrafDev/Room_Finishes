#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Architecture;
using System.Globalization;
using System.Resources;
using RM;

#endregion


namespace RM
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class WallDialogBox : Window
    {
        private Document _doc;
        private UIDocument _UIDoc;

        private IEnumerable<WallType> _wallTypes;
        public readonly WallSetup WallSetup;
        public WallDialogBox(UIDocument UIDoc, WallSetup skirtingBoardSetup)
        {
            InitializeComponent();
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            WallSetup = skirtingBoardSetup;
            

            //Fill out Text in form
            this.Title = Util.GetLanguageResources.GetString("roomFinishes_TaskDialogName", Util.Cult);
            this.all_rooms_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_all_rooms_radio", Util.Cult);           
            this.select_wall_label.Content = Util.GetLanguageResources.GetString("roomFinishes_select_wall_label", Util.Cult);
            this.selected_rooms_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_selected_rooms_radio", Util.Cult);
            this.Cancel_Button.Content = Util.GetLanguageResources.GetString("roomFinishes_Cancel_Button", Util.Cult);
            this.Ok_Button.Content = Util.GetLanguageResources.GetString("roomFinishes_OK_Button", Util.Cult);
            this.groupboxParam.Header = Util.GetLanguageResources.GetString("roomFinishes_groupboxParamName", Util.Cult);
            this.from_level_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_from_level_Radio", Util.Cult);
            this.to_height_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_to_height_Radio", Util.Cult);
            this.HeightTextBox.Text = Util.GetLanguageResources.GetString("roomFinishes_height_textBox", Util.Cult);
            this.groupBoxRoom.Header = Util.GetLanguageResources.GetString("roomFinishes_groupboxRoomName", Util.Cult);
            this.HeightTextBox.BorderBrush = Brushes.DarkGray;
            this.HeightTextBox.BorderThickness = new Thickness(1, 1, 1, 1);
            // Выбор типа стены в файле
            _wallTypes = from elem in new FilteredElementCollector(_doc).OfClass(typeof(WallType))
                         let type = elem as WallType
                         where type.Kind == WallKind.Basic
                         select type;

            _wallTypes = _wallTypes.OrderBy(wallType => wallType.Name);

            // Bind ArrayList with the ListBox
            WallTypeListBox.ItemsSource = _wallTypes;
            WallTypeListBox.SelectedItem = WallTypeListBox.Items[0];

            // Обнаружение помещений для вытаскивания парметров
            IList<Element> roomList = new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_Rooms).ToList();

        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка на коректность вводимого значения смещения
            if (Util.GetFromString(HeightTextBox.Text, _doc.GetUnits()) != null)
            {
                // Конвертация смещения и записывание в поле типа 
                WallSetup.OffsetWallHeight = (double)Util.GetFromString(HeightTextBox.Text, _doc.GetUnits());

                // Проверка выбора отсчета от уровня
                if (from_level_radio.IsChecked.Value)
                {                        
                    WallSetup.FromLevel = true;
                    // Проверка является ли число не нулем, при отсчете от уровня это важно
                    if (Util.GetFromString(HeightTextBox.Text, _doc.GetUnits()) != 0)
                    {
                        if (WallTypeListBox.SelectedItem != null)
                        {
                            //  Выбор стен отделки                 
                            WallSetup.SelectedWallType = WallTypeListBox.SelectedItem as WallType;

                            this.DialogResult = true;
                            this.Close();

                            // выбор помещений отделки
                            WallSetup.SelectedRooms = SelectRooms().ToList();
                        }
                    }
                    else
                    {

                        //TaskDialog.Show(Util.GetLanguageResources.GetString("roomFinishes_TaskDialogName", Util.Cult),
                        //    Util.GetLanguageResources.GetString("roomFinishes_heightValueError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);

                        this.HeightTextBox.BorderBrush = Brushes.Red;
                        this.HeightTextBox.BorderThickness = new Thickness(2, 2, 2, 2);
                        this.Activate();
                    }
                }
                else
                {
                    WallSetup.FromLevel = false;
                    if (WallTypeListBox.SelectedItem != null)
                    {
                        // Выбор стен отделки                       
                        WallSetup.SelectedWallType = WallTypeListBox.SelectedItem as WallType;

                        this.DialogResult = true;
                        this.Close();

                        // Выбор помещений отделки
                        WallSetup.SelectedRooms = SelectRooms().ToList();
                    }

                }


            }
            else
            {
                // Давайка еще раз попробуй ввести данные по смещению
                TaskDialog.Show(Util.GetLanguageResources.GetString("roomFinishes_TaskDialogName", Util.Cult),
                    Util.GetLanguageResources.GetString("roomFinishes_heightValueError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
                this.Activate();
            }

 


        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private IEnumerable<Room> SelectRooms()
        {
            //Create a set of selected elements ids
            ICollection<ElementId> selectedObjectsIds = _UIDoc.Selection.GetElementIds();

            //Create a set of rooms
            IEnumerable<Room> ModelRooms = null;
            IList<Room> tempList = new List<Room>();

            if (all_rooms_radio.IsChecked.Value)
            {
                // Find all rooms in current view
                ModelRooms = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                             let room = elem as Room
                             select room;
            }
            else
            {
                if (selectedObjectsIds.Count != 0)
                {
                    // Find all rooms in selection
                    ModelRooms = from elem in new FilteredElementCollector(_doc, selectedObjectsIds).OfClass(typeof(SpatialElement))
                                 let room = elem as Room
                                 select room;
                    tempList = ModelRooms.ToList();
                }


                if (tempList.Count == 0)
                {
                    //Create a selection filter on rooms
                    ISelectionFilter filter = new RoomSelectionFilter();

                    IList<Reference> rs = _UIDoc.Selection.PickObjects(ObjectType.Element, filter,
                        Util.GetLanguageResources.GetString("roomFinishes_SelectRooms", Util.Cult));

                    foreach (Reference r in rs)
                    {
                        tempList.Add(_doc.GetElement(r) as Room);
                    }


                    ModelRooms = tempList;
                }
            }
            if (ModelRooms.LongCount() == 0 && all_rooms_radio.IsChecked.Value)
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("roomSelectError_TitleDialogBox", Util.Cult),
                Util.GetLanguageResources.GetString("roomAutoSelectError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
            }
            else if (ModelRooms.LongCount() == 0)
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("roomSelectError_TitleDialogBox", Util.Cult),
                Util.GetLanguageResources.GetString("roomSelectError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
            }
            return ModelRooms;
        }

        private void Height_TextBox_Wall(object sender, RoutedEventArgs e)
        {
            if (Util.GetFromString(HeightTextBox.Text, _doc.GetUnits()) != null)
            {
                WallSetup.OffsetWallHeight = (double)Util.GetFromString(HeightTextBox.Text, _doc.GetUnits());
                HeightTextBox.Text = UnitFormatUtils.Format(_doc.GetUnits(), UnitType.UT_Length, WallSetup.OffsetWallHeight, true, true);
            }
            else
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("roomFinishes_TaskDialogName", Util.Cult),
                    Util.GetLanguageResources.GetString("roomFinishes_heightValueError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
                this.Activate();
            }
        }

    }

    public class RoomSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element element)
        {
            if (element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_Rooms)
            {
                return true;
            }
            return false;
        }

        public bool AllowReference(Reference refer, XYZ point)
        {
            return false;
        }
    }
    
}
