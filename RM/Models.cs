using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM
{
    public class WallSetup
    {
        public WallType SelectedWallType { get; set; }
        public double OffsetWallHeight { get; set; }
        public bool JoinWall = true;
        public List<Room> SelectedRooms { get; set; }
        public bool FromLevel { get; set; }
    }

    public class FloorsSetup
    {
        public FloorType SelectedFloorType { get; set; }
        public double OffsetFloorHeight { get; set; }
        public Parameter RoomParameter { get; set; }
        public List<Room> SelectedRooms { get; set; }

    }
}
