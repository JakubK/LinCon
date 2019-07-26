using System.Collections.Generic;
using LiteDB;

namespace LinCon.Core.Models
{
    public class Case
    {
        [BsonId]
        public int ID {get;set;}
        public string Name { get;set; }
        public List<string> Links {get;set;} = new List<string>();
    }
}