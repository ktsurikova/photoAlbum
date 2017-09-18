using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace ORM.Entities
{
    public class Photo
    {
        public Photo()
        {
            Tags = new HashSet<string>();
            UserLikes = new HashSet<int>();
        }

        [BsonId]
        [BsonElement(elementName: "_id")]
        public int Id { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement(elementName: "name")]
        public string Name { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement(elementName: "desc")]
        public string Description { get; set; }

        [BsonElement(elementName: "likesCount")]
        public int NumberOfLikes { get; set; }

        [BsonElement(elementName: "upDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement(elementName: "user_id")]
        public int UserId { get; set; }

        [BsonElement(elementName: "tags")]
        public IEnumerable<string> Tags { get; set; }

        [BsonElement(elementName: "likes")]
        public IEnumerable<int> UserLikes { get; set; }

        [BsonElement(elementName: "image")]
        public byte[] Image { get; set; }
    }
}
