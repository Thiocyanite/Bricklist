using System;
namespace zadanie2ubi.ObjectTypes
{
    public class ItemType
    {
        public int id;
        public string Code;
        public string Name;
        public string NamePl;


        public ItemType()
        {
        }

        public ItemType(int _id, string _Code, string _Name, string _NamePl)
        {
            id = _id;
            Code = _Code;
            Name = _Name;
            NamePl = _NamePl;
        }
    }
}
