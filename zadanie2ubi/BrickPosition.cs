using System;
namespace zadanie2ubi
{
    public class BrickPosition
    {
        public BrickPosition() { }
        public BrickPosition(char type, int quantity, string number, int color)
        {
            Type = type;
            Quantity = quantity;
            InInventory = 0;
            Number = number;
            Color = color;
        }

        private char Type { get; set; }
        private int Quantity { get; set; }
        private int InInventory { get; set; }
        private string Number { get; set; }
        private int Color { get; set; }

        public void AddBrick()
        {
            if (InInventory < Quantity)
                InInventory++;
        }

        public void RemoveBrick()
        {
            if (InInventory > 0)
                InInventory--;
        }
    }
}