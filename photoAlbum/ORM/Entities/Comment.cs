using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ORM.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        public int Id { get; set; }

        [BsonElement(elementName: "photo_id")]
        public int PhotoId { get; set; }

        [BsonElement(elementName: "posted")]
        public DateTime Posted { get; set; }

        [BsonElement(elementName: "text")]
        public string Text { get; set; }

        [BsonElement(elementName: "author")]
        public Author Author { get; set; }

    }
}
