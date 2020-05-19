using System;
namespace zadanie2ubi.ObjectTypes
{
    public class BrickInGui
    {
        InventoryPart ThisPart;
        public BrickInGui(InventoryPart part)
        {
            ThisPart = part;
        }

        public String StaticValues()
        {
            return String.Format("{0,6}{0,7}{0,7}{0,7}",
                ThisPart.TypeID, ThisPart.ItemID, ThisPart.ColorID, ThisPart.Extra);
        }
        public String BricksNum()
        {
            return String.Format("{0,3}/{0,3}", ThisPart.QuantityInStore, ThisPart.QuantityInSet);
        }

        public void Add(int i = 1)
        {
            if (ThisPart.QuantityInStore < ThisPart.QuantityInSet)
                ThisPart.QuantityInStore += i;
        }

        public void Remove(int i = 1)
        {
            if (ThisPart.QuantityInStore > 0)
                ThisPart.QuantityInStore -= i;
        }
    }
}
