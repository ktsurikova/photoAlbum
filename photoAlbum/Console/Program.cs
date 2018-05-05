using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //ModelContext modelContext = new ModelContext();

            string connectionString = "mongodb://admin:6D9e3m5i4d8@ds135384.mlab.com:35384/photoalbum";
            IMongoClient client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase("photoalbum");

            var col = database.GetCollection<User>("users");
        }

        public class User
        {
            [BsonId]
            [BsonElement(elementName: "_id")]
            public int Id { get; set; }

            [BsonElement(elementName: "role")]
            public IEnumerable<string> Roles { get; set; }

            [BsonElement(elementName: "login")]
            public string Login { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement(elementName: "name")]
            public string Name { get; set; }

            [BsonElement(elementName: "email")]
            public string Email { get; set; }

            [BsonElement(elementName: "passwd")]
            public string Password { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement(elementName: "phone")]
            public string Phone { get; set; }

            [BsonIgnoreIfNull]
            [BsonElement(elementName: "image")]
            public byte[] ProfilePhoto { get; set; }

        }
    }
}
