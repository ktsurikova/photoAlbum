using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ORM.Entities;

namespace ORM
{
    public class ModelContext
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private static ModelContext _modelContext;

        private ModelContext() { }

        public static ModelContext Create()
        {
            if (_modelContext == null)
            {
                _modelContext = new ModelContext();
                string connectionString = "mongodb://admin:6D9e3m5i4d8@ds135384.mlab.com:35384/photoalbum";
                _modelContext.Client = new MongoClient(connectionString);
                _modelContext.Database = _modelContext.Client.GetDatabase("photoalbum");
            }
            return _modelContext;
        }

        //public void TestConnection()
        //{
        //    var dbsCursor = _modelContext.Client.ListDatabases();
        //    var dbsList = dbsCursor.ToList();
        //    foreach (var db in dbsList)
        //    {
        //        Console.WriteLine(db);
        //    }
        //}

        public IMongoCollection<User> Users
        {
            get { return Database.GetCollection<User>("users"); }
        }

        public IMongoCollection<Photo> Photos
        {
            get { return Database.GetCollection<Photo>("photos"); }
        }

        public IMongoCollection<Comment> Comments
        {
            get { return Database.GetCollection<Comment>("comments"); }
        }
    }
}
