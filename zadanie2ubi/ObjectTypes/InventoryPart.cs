using System;
namespace zadanie2ubi.ObjectTypes
{
    public class InventoryPart
    {
        public int id;
        public int InventoryID;
        public int TypeID;
        public int ItemID;
        public int QuantityInSet;
        public int QuantityInStore;
        public int ColorID;
        public int Extra;
        public InventoryPart()
        {
        }

        public InventoryPart(int _id, int _InventoryID, int _TypeID, int _ItemID, int _QuantityInSet,int _ColorID, int _Extra)
        {
            id = _id;
            InventoryID = _InventoryID;
            TypeID = _TypeID;
            ItemID = _ItemID;
            QuantityInSet = _QuantityInSet;
            QuantityInStore = 0;
            ColorID = _ColorID;
            Extra = _Extra;
        }
    }
}
