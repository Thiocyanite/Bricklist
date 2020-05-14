using System;
namespace zadanie2ubi.ObjectTypes
{
    public class Color
    {
        public int id;
        public int Code;
        public string Name;
        public string NamePl;

        public Color()
        {
        }

        public Color (int _id, int _Code, string _Name, string _NamePl)
        {
            id = _id;
            Code = _Code;
            Name = _Name;
            NamePl = _NamePl;
        }
    }
}
