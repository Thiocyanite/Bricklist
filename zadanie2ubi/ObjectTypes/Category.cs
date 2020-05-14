using System;
namespace zadanie2ubi.ObjectTypes
{
    public class Category
    {
        public int id;
        public int Code;
        public string Name;
        public string NamePL;
        public Category()
        {
        }
        public Category(int _id, int _Code, string _Name, string _NamePL)
        {
            id = _id;
            Code = _Code;
            Name = _Name;
            NamePL = _NamePL;
        }
    }
}
