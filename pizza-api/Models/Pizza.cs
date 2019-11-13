using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace pizza_api.Models
{
    [BsonIgnoreExtraElements]
    public class Pizza : IEquatable<Pizza>
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("ingredients")]
        public List<string> Ingredients { get; set; }

        [BsonElement("size")]
        public string Size { get; set; }

        [BsonElement("slices")]
        public int Slices { get; set; }

        [BsonElement("extraChess")]
        public bool HasExtraCheese { get; set; }

        public bool Equals(Pizza other)
        {
            return Name.Equals(other.Name);
        }
    }
}
