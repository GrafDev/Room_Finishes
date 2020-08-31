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
using System.Reflection.Emit;
using System.Reflection;
#endregion

/// <summary>
/// Взаимодействие с Диалоговым окном.(FloorDialogBox.xaml)
/// </summary>
/// 


namespace RM
{
    public partial class FloorDialogBox: Window
    {
        private Document _doc;
        private UIDocument _UIDoc;

        public readonly FloorsSetup FloorSetup;

        public FloorDialogBox(UIDocument UIDoc, FloorsSetup floorsSetup)
        {
            InitializeComponent();
            _doc = UIDoc.Document;
            _UIDoc = UIDoc;
            FloorSetup = floorsSetup;

            // Заполнение текстовых пунктов согласно языка
            this.Title = Util.GetLanguageResources.GetString("floor_TaskDialogName", Util.Cult);
            this.allRoomsRadio.Content = Util.GetLanguageResources.GetString("roomFinishes_all_rooms_radio", Util.Cult);
            this.groupboxParam.Header = Util.GetLanguageResources.GetString("floor_groupboxName", Util.Cult);
            this.selectFoorLabel.Content = Util.GetLanguageResources.GetString("floor_select_floor_label", Util.Cult);
            this.selectRoomRadio.Content = Util.GetLanguageResources.GetString("roomFinishes_selected_rooms_radio", Util.Cult);
            this.cancelButton.Content = Util.GetLanguageResources.GetString("roomFinishes_Cancel_Button", Util.Cult);
            this.okButton.Content = Util.GetLanguageResources.GetString("roomFinishes_OK_Button", Util.Cult);
            this.groupboxRoomSelect.Header = Util.GetLanguageResources.GetString("floor_groupboxRoomSelectName", Util.Cult);



            // Выделение полов в файле
            IEnumerable<FloorType> floorTypes = from elem in new FilteredElementCollector(_doc).OfClass(typeof(FloorType))
                                               let type = elem as FloorType
                                               where type.IsFoundationSlab == false
                                               select type;

            floorTypes = floorTypes.OrderBy(floorType => floorType.Name);
            FloorTypeListBox.ItemsSource = floorTypes;
            FloorTypeListBox.SelectedItem = FloorTypeListBox.Items[0];

            // Обнаружение помещений для вытаскивания парметров
            IList<Element> roomList =   new FilteredElementCollector(_doc).OfCategory(BuiltInCategory.OST_Rooms).ToList();

            // Заполнение необходимыми парметрами выпадающую полосу
            if (roomList.Count != 0)
            {
                
                Room room = roomList.First() as Room;
                List<Parameter> doubleParam = new List<Parameter>(4);
                doubleParam.Insert(0, room.get_Parameter(BuiltInParameter.ROOM_LEVEL_ID));
                doubleParam.Insert(1, room.get_Parameter(BuiltInParameter.ROOM_LOWER_OFFSET));
                doubleParam.Insert(2, room.get_Parameter(BuiltInParameter.ROOM_UPPER_OFFSET));
                doubleParam.Insert(3, room.get_Parameter(BuiltInParameter.ROOM_COMPUTATION_HEIGHT));
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
            // Назначение пармемтра помещения от которого будет браться высота положения перекрытия 
            FloorSetup.RoomParameter = paramSelector.SelectedItem as Parameter;


            if (Util.GetFromString(Height_TextBox.Text, _doc.GetUnits()) != null)
            {
                FloorSetup.OffsetFloorHeight = (double)Util.GetFromString(Height_TextBox.Text, _doc.GetUnits());

                if (FloorTypeListBox.SelectedItem != null)
                {

                    FloorSetup.SelectedFloorType = FloorTypeListBox.SelectedItem as FloorType;

                    this.DialogResult = true;
                    this.Close();

                    FloorSetup.SelectedRooms = SelectRooms().ToList();
                }
            }
            else
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("floor_TaskDialogName", Util.Cult),
                    Util.GetLanguageResources.GetString("floor_heightValueError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
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
            // Создание списка идентификаторов выбранных элементов
            ICollection<ElementId> selectedObjectsIds = _UIDoc.Selection.GetElementIds();

            // Создание списка комнат
            IEnumerable<Room> ModelRooms = null;
            IList<Room> tempList = new List<Room>();

            if (allRoomsRadio.IsChecked.Value)
            {
                // Поиск всех помещений в текущем виде
                ModelRooms = from elem in new FilteredElementCollector(_doc, _doc.ActiveView.Id).OfClass(typeof(SpatialElement))
                             let room = elem as Room
                             select room;
            }
            else
            {
                if (selectedObjectsIds.Count != 0)
                {
                    // Список выбраных комнат
                    ModelRooms = from elem in new FilteredElementCollector(_doc, selectedObjectsIds).OfClass(typeof(SpatialElement))
                                 let room = elem as Room
                                 select room;
                    tempList = ModelRooms.ToList();
                }

                if (tempList.Count == 0)
                {
                    // Создание списка выбраных комнат
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
            if (ModelRooms.LongCount() == 0)
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("roomSelectError_TitleDialogBox", Util.Cult),
                Util.GetLanguageResources.GetString("roomSelectError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
            }

            return ModelRooms;
        }

        private void Height_TextBox_Floor(object sender, RoutedEventArgs e)
        {

            if (Util.GetFromString(Height_TextBox.Text, _doc.GetUnits()) != null)
            {
                FloorSetup.OffsetFloorHeight = (double)Util.GetFromString(Height_TextBox.Text, _doc.GetUnits());
                Height_TextBox.Text = UnitFormatUtils.Format(_doc.GetUnits(), UnitType.UT_Length, FloorSetup.OffsetFloorHeight, true, true);
            }
            else
            {
                TaskDialog.Show(Util.GetLanguageResources.GetString("floor_TaskDialogName", Util.Cult),
                    Util.GetLanguageResources.GetString("floor_heightValueError", Util.Cult), TaskDialogCommonButtons.Close, TaskDialogResult.Close);
                this.Activate();
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
}
