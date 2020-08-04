﻿#region Namespaces
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
        public readonly WallSetup SkirtingBoardSetup;
        public WallDialogBox(UIDocument UIDoc, WallSetup skirtingBoardSetup)
        {
            InitializeComponent();
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            SkirtingBoardSetup = skirtingBoardSetup;

            //Fill out Text in form
            this.Title = Util.GetLanguageResources.GetString("roomFinishes_TaskDialogName", Util.Cult);
            this.all_rooms_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_all_rooms_radio", Util.Cult);           
            this.select_wall_label.Content = Util.GetLanguageResources.GetString("roomFinishes_select_wall_label", Util.Cult);
            this.selected_rooms_radio.Content = Util.GetLanguageResources.GetString("roomFinishes_selected_rooms_radio", Util.Cult);
            this.Cancel_Button.Content = Util.GetLanguageResources.GetString("roomFinishes_Cancel_Button", Util.Cult);
            this.Ok_Button.Content = Util.GetLanguageResources.GetString("roomFinishes_OK_Button", Util.Cult);
            this.groupboxParam.Header = Util.GetLanguageResources.GetString("roomFinishes_groupboxName", Util.Cult);
            this.HeightTextBox.Text = "0";


            //Select the wall type in the document
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


            if (roomList.Count != 0)
            {
                // Заполнение необходимыми парметрами выпадающую полосу
                Room room = roomList.First() as Room;
                List<Parameter> doubleParam = new List<Parameter>(3);
                doubleParam.Insert(0, room.get_Parameter(BuiltInParameter.ROOM_UPPER_OFFSET));
                doubleParam.Insert(1, room.get_Parameter(BuiltInParameter.ROOM_LEVEL_ID));
                paramSelector.ItemsSource = doubleParam;
                paramSelector.DisplayMemberPath = "Definition.Name";
                paramSelector.SelectedIndex = 0;
            }
            else
            {

                paramSelector.IsEnabled = false;

            }


        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Util.GetFromString(HeightTextBox.Text, _doc.GetUnits()) != null)
            {                
                SkirtingBoardSetup.BoardHeight = (double)Util.GetFromString(HeightTextBox.Text, _doc.GetUnits());

                if (WallTypeListBox.SelectedItem != null)
                {
                    //Select wall type for skirting board
                    SkirtingBoardSetup.JoinWall = true;
                    SkirtingBoardSetup.SelectedWallType = WallTypeListBox.SelectedItem as WallType;

                    this.DialogResult = true;
                    this.Close();

                    //Select the rooms
                    SkirtingBoardSetup.SelectedRooms = SelectRooms().ToList();
                }
            }
            else
            {
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

            return ModelRooms;
        }

        private void Height_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Util.GetFromString(HeightTextBox.Text, _doc.GetUnits()) != null)
            {
                SkirtingBoardSetup.BoardHeight = (double)Util.GetFromString(HeightTextBox.Text, _doc.GetUnits());
                HeightTextBox.Text = UnitFormatUtils.Format(_doc.GetUnits(), UnitType.UT_Length, SkirtingBoardSetup.BoardHeight, true, true);

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
