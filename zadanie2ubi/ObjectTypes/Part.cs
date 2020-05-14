using System;
namespace zadanie2ubi.ObjectTypes
{
    public class Part
    {
        public int id;
        public int TypeID;
        public string Code;
        public string Name;
        public string NamePL;
        public int categoryID;

        public Part()
        {
        }

        public Part(int _id, int _TypeID, string _Code, string _Name, string _NamePL, int _categoryID)
        {
            id = _id;
            TypeID = _TypeID;
            Code = _Code;
            Name = _Name;
            NamePL = _NamePL;
            categoryID = _categoryID;
        }
    }
}
