using System.Collections.Generic;
using LiteDB;
using Newtonsoft.Json;

namespace LinCon.Core.Models
{
    public class Case
    {
        [BsonId]
        [JsonIgnore]
        public int ID {get;set;}
        public string Name { get;set; }
        public List<Link> Links {get;set;} = new List<Link>();
    }
}