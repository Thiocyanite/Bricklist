using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using SQLite;
using System.IO.Compression;
using zadanie2ubi.ObjectTypes;


namespace zadanie2ubi
{
    public class Backend
    {
        private List<String> BrickSetsNames;
        private SQLiteConnection db;
        public string ChosenSet { get; set; }
        public List<BrickInGui> Bricks {get;set; }
        private static readonly Lazy<Backend>
        lazy =
        new Lazy<Backend>
            (() => new Backend());

        public static Backend Instance { get { return lazy.Value; } }


        private Backend()
        {
            BrickSetsNames = new List<string>();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "database.db3");
            if (File.Exists(dbPath))
            {
                db = new SQLiteConnection(dbPath);
                
            }
            else
            {
                db = new SQLiteConnection(dbPath);
                CreateDB();
                CreateExampleData();
            }
            ChosenSet = "None";
            Bricks = new List<BrickInGui>();
        }

        public void GetInventoryParts(int id)
        {
            var table = db.Table<InventoryPart>().Where(i => i.InventoryID == id);
            List<BrickInGui> bricks = new List<BrickInGui>();
            foreach(InventoryPart part in table)
            {
                bricks.Add(new BrickInGui(part));
            }
            Bricks = bricks;
        }

        public List<string> GetBricksStableInfo()
        {
            List<string> vs = new List<string>();
            vs.Add("Typ Id Kolor Ekstra");
            foreach (var brick in Bricks)
                vs.Add(brick.StaticValues());
            return vs;
        }

        public List<string> GetBricksNums()
        {
            List<string> vs = new List<string>();
            vs.Add("Ile na");
            foreach (var brick in Bricks)
                vs.Add(brick.BricksNum());
            return vs;
        }

        public List<string> GetSetNames()
        {
            List<string> setlist = new List<string>();
            var table = db.Table<Inventory>();
            foreach (Inventory inventory in table)
            {
                var name = inventory.Id + " " + inventory.Name;
                setlist.Add(name);
            }
            return setlist;
        }

        public void CreateExampleData()
        {
            var inventory = new Inventory(7, "Nazwa", 0, 0);
            db.Insert(inventory);
            Random rnd = new Random();
            InventoryPart part;
            for (int i=0; i < 10; i++)
            {
                part = new InventoryPart(i, 7, i, i, i, 0, 0);
                db.Insert(part);
            }
        }

        /// <summary>
        /// This function shall download xml file with Lego set.
        /// To do: check why it doesn't want to download file
        /// </summary>
        /// <param name="id"> Number of set, for example 615</param> 
        public void AddInventory(int id, string name)
        {
            try
            {
                BrickSetsNames.Add(id.ToString()+" "+name);
                WebClient client = new WebClient();
                var source = "http://fcds.cs.put.poznan.pl/MyWeb/BL/" + id.ToString() + ".xml";
                var destination = Environment.GetFolderPath(Environment.SpecialFolder.Personal)+id.ToString() + ".xml";
                File.Create(destination);
                client.DownloadFile(source, id.ToString() + ".xml");
                System.Console.WriteLine("Udalo sie");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This function writes how many bricks you shall buy to have full set
        /// </summary>
        /// <param name="id"> Number of set </param>
        private void WriteXML(int id)
        {
            var table = db.Table<InventoryPart>();
            table = table.Where(i => i.InventoryID == id);
            table = table.Where(i => i.QuantityInStore < i.QuantityInSet);
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Result.xml"))
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Result.xml");
            }
            using (var file = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "Result.xml"))
            {
                file.WriteLine("<INVENTORY>");
                foreach(var brick in table)
                {
                    string[] lines =
                    {
                        "<ITEM>",
                        "<ITEMTYPE>"+brick.ItemID.ToString()+"</ITEMTYPE>",
                        "<ITEMID>"+brick.Id.ToString()+"</ITEMID>",
                        "<COLOR>"+brick.ColorID.ToString()+"</COLOR>",
                        "<QTYFILLED>"+(brick.QuantityInSet-brick.QuantityInStore).ToString()+"</QTYFILLD>",
                        "</ITEM>"
                    };
                    foreach (var line in lines)
                        file.WriteLine(line);
                }
                file.WriteLine("</INVENTORY>");
                file.Close();
            }
        }

        /// <summary>
        /// This function reads xml file and add set and its parts to db
        /// </summary>
        /// <param name="id"> Number of set </param>
        /// <param name="name"> Name of set </param>
        private void ReadXML(int id, string name)
        {
            string line;
            var file= new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + id.ToString() + ".xml");
            InventoryPart part = new InventoryPart();
            db.Insert(new Inventory(id, name, 1, 0));
            while ((line = file.ReadLine()) != null)
            {
                var mylist = line.Split(">");
                switch (mylist[0])
                {
                    case "<ITEM":
                        part = new InventoryPart();
                        break;
                    case "<ITEMTYPE":
                        var type = mylist[1].Split("<")[0];
                        part.TypeID = int.Parse(type);
                        break;
                    case "<QTY":
                        var quantity = mylist[1].Split("<")[0];
                        part.QuantityInSet = int.Parse(quantity);
                        break;
                    case "<ITEMID":
                        var itemid = mylist[1].Split("<")[0];
                        part.ItemID = int.Parse(itemid);
                        break;
                    case "<COLOR":
                        var color = mylist[1].Split("<")[0];
                        part.ColorID = int.Parse(color);
                        break;
                    case "<EXTRA":
                        if (mylist[1][1] == 'N')
                            part.Extra = 0;
                        else
                            part.Extra = 1;
                        break;
                    case "</ITEM":
                        part.InventoryID = id;
                        db.Insert(part);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// This function deletes set and its parts from db
        /// </summary>
        /// <param name="id"> Number of set </param>
        public void DeleteInventory(int id)
        {
            var tab = db.Table<InventoryPart>().Where(i => i.InventoryID == id);
            foreach (InventoryPart part in tab)
                db.Delete(part);
            Inventory inventory = db.Table<Inventory>().Where(i => i.Id == id).First();
            db.Delete(inventory);
            ChosenSet = "None";
        }


        /// <summary>
        /// This function creates category and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_Code"> Code </param>
        /// <param name="_Name"> English name </param>
        /// <param name="_NamePL"> Polish name </param>
        public void CreateCategory(int _id, int _Code, string _Name, string _NamePL)
        {
            var cat = new Category(_id, _Code, _Name, _NamePL);
            db.Insert(cat);
        }

        /// <summary>
        /// This function creates code (mix of color and item id, I guess) and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_ItemID"> Item id </param>
        /// <param name="_ColorId"> Color id </param>
        /// <param name="_code"> Code </param>
        public void CreateCode(int _id, int _ItemID, int _ColorId, int _code)
        {
            var code = new Code(_id, _ItemID, _ColorId, _code);
            db.Insert(code);
        }

        /// <summary>
        /// This function creates color and adds it do db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_Code"> Code </param>
        /// <param name="_Name"> English name </param>
        /// <param name="_NamePl"> Polish name </param>
        public void CreateColor(int _id, int _Code, string _Name, string _NamePl)
        {
            var color = new Color(_id, _Code, _Name, _NamePl);
            db.Insert(color);
        }

        /// <summary>
        /// This function creates inventory and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_Name"> Name </param>
        /// <param name="_Active"> Active (?) </param>
        /// <param name="_LastAccessed"> Last Accessed (when?) </param>
        public void CreateInventory(int _id, string _Name, int _Active, int _LastAccessed)
        {
            var invent = new Inventory(_id, _Name, _Active, _LastAccessed);
            db.Insert(invent);
        }

        /// <summary>
        /// This function creates inventory part and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_InventoryID"> Id of set </param>
        /// <param name="_TypeID"> Id of type </param>
        /// <param name="_ItemID"> Id of item </param>
        /// <param name="_QuantityInSet"> How many bricks of this type are in set </param>
        /// <param name="_ColorID"> Id of color </param>
        /// <param name="_Extra"> Extra(?) </param>
        public void CreatInventoryPart(int _id, int _InventoryID, int _TypeID, int _ItemID, int _QuantityInSet, int _ColorID, int _Extra)
        {
            var part = new InventoryPart(_id, _InventoryID, _TypeID, _ItemID, _QuantityInSet, _ColorID, _Extra);
            db.Insert(part);
        }


        /// <summary>
        /// This function creates item type and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_Code"> Code </param>
        /// <param name="_Name"> English name </param>
        /// <param name="_NamePl"> Polish name </param>
        public void CreateItemType(int _id, string _Code, string _Name, string _NamePl)
        {
            var type = new ItemType(_id, _Code, _Name, _NamePl);
            db.Insert(type);
        }

        /// <summary>
        /// This function creates part and adds it to db
        /// </summary>
        /// <param name="_id"> Id </param>
        /// <param name="_TypeID"> Id of type </param>
        /// <param name="_Code"> Code </param>
        /// <param name="_Name"> English name </param>
        /// <param name="_NamePL"> Polish name</param>
        /// <param name="_categoryID"> Id of category </param>
        public void CreatePart(int _id, int _TypeID, string _Code, string _Name, string _NamePL, int _categoryID)
        {
            var part = new Part(_id, _TypeID, _Code, _Name, _NamePL, _categoryID);
            db.Insert(part);
        }

        /// <summary>
        /// This function creates all tables in db
        /// </summary>
        private void CreateDB()
        {
            db.CreateTable<Category>();
            db.CreateTable<Code>();
            db.CreateTable<Color>();
            db.CreateTable<Inventory>();
            db.CreateTable<InventoryPart>();
            db.CreateTable<ItemType>();
            db.CreateTable<Part>();
        }

    }
}


