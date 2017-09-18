using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using ORM.Entities;
using System.Configuration;

namespace ORM
{
    public class ModelContext
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private static ModelContext modelContext;

        private ModelContext() { }

        public static ModelContext Create()
        {
            if (modelContext == null)
            {
                modelContext = new ModelContext();

                string connectionString = ConfigurationManager.
                    ConnectionStrings["PhotoAlbumConnectionString"].ConnectionString;
                modelContext.Client = new MongoClient(connectionString);

                modelContext.Database = modelContext.Client.
                    GetDatabase(ConfigurationManager.AppSettings["PhotoAlbumDatabaseName"]);
            }
            return modelContext;
        }

        public IMongoCollection<User> Users => Database.GetCollection<User>("users");

        public IMongoCollection<Photo> Photos => Database.GetCollection<Photo>("photos");

        public IMongoCollection<Comment> Comments => Database.GetCollection<Comment>("comments");

    }
}
