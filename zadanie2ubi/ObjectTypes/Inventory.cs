using System;
namespace zadanie2ubi.ObjectTypes
{
    public class Inventory
    {
        public int id;
        public string Name;
        public int Active;
        public int LastAccessed;
        public Inventory()
        {
        }

        public Inventory(int _id, string _Name, int _Active, int _LastAccessed)
        {
            id = _id;
            Name = _Name;
            Active = _Active;
            LastAccessed = _LastAccessed;
        }
    }
}
