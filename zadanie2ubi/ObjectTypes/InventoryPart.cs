using System;
using SQLite;
namespace zadanie2ubi.ObjectTypes
{
    public class InventoryPart
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int InventoryID     {get;set;}
        public int TypeID          {get;set;}
        public int ItemID          {get;set;}
        public int QuantityInSet   {get;set;}
        public int QuantityInStore {get;set;}
        public int ColorID         {get;set;}
        public int Extra           {get;set;}
        public InventoryPart()
        {
        }

        public InventoryPart(int _id, int _InventoryID, int _TypeID, int _ItemID, int _QuantityInSet,int _ColorID, int _Extra)
        {
            Id = _id;
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
