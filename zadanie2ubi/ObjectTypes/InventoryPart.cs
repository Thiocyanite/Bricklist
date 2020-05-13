using System;
namespace zadanie2ubi.ObjectTypes
{
    public class InventoryPart
    {
        private int id { get; set; }
        private int InventoryID { get; set; }
        private int TypeID { get; set; }
        private int ItemID { get; set; }
        private int QuantityInSet { get; set; }
        private int QuantityInStore { get; set; }
        private int ColorID { get; set; }
        private int Extra { get; set; }
        public InventoryPart()
        {
        }
    }
}
