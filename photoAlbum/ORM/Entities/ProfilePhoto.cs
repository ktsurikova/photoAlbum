using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ORM.Entities
{
    public class ProfilePhoto
    {
        [BsonElement(elementName: "type")]
        public string Type { get; set; }

        [BsonElement(elementName: "url")]
        public string Url { get; set; }
    }
}
