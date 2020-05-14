using System;
namespace zadanie2ubi.ObjectTypes
{
    public class Code
    {
        public int id;
        public int ItemID;
        public int ColorId;
        public int code;

        public Code()
        {
        }
        public Code(int _id, int _ItemID, int _ColorId, int _code)
        {
            id = _id;
            ItemID = _ItemID;
            ColorId = _ColorId;
            code = _code;
        }
    }
}
