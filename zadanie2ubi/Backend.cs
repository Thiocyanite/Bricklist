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

        private static readonly Lazy<Backend>
        lazy =
        new Lazy<Backend>
            (() => new Backend());

        public static Backend Instance { get { return lazy.Value; } }

        private Backend()
        {
            
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
        "database.db3");
            //File.Delete(dbPath);
            if (File.Exists(dbPath))
            {
                db = new SQLiteConnection(dbPath);
            }
            else
            {
                db = new SQLiteConnection(dbPath);
            }

        }

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


        public void AddInventory(int id)
        {
            try
            {
                BrickSetsNames.Add(id.ToString());
                var client = new WebClient();
                client.DownloadFile("http://fcds.cs.put.poznan.pl/MyWeb/BL/"+id+".xml", id+".xml");
                System.Console.WriteLine("Udalo sie");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }

        public void DeleteInventory(int id)
        {
            InventoryPart inventoryPart;
            do
            {
                inventoryPart = db.Table<InventoryPart>().Where(i => i.InventoryID == id).FirstOrDefault();
                db.Delete(inventoryPart);
            } while (inventoryPart != null);
            Inventory inventory = db.Table<Inventory>().Where(i => i.id == id).First();
            db.Delete(inventory);
        }
    }
}
