using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;

namespace ORM.Entities
{
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
