using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab6.Models
{
    public class Note
    {
        public string Name {get;set; }
        public string Content {get;set; }
        public Note(string name, string content)
        {
            Name = name;
            Content = content;
        }
        public Note()
        {
        }
    }
}