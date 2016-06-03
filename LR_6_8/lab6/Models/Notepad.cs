using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab6.Models;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace lab6.Models
{
    public class Notepad
    {
        public static List<Note> list;
        public List <Note> _list { get { return list; } }
        public string myPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data","DB.json");

        public void CreateNotepad(string name)
        {
            if (list == null) { list = new List<Note>(); }
            if (!string.IsNullOrWhiteSpace(name))
            {
                File.WriteAllText(myPath, JsonConvert.SerializeObject(list));
            }
        }
        public void ReadAllNotepad()
        {
            if (File.Exists(myPath))
            {
                var text = File.ReadAllText(myPath);
                list = JsonConvert.DeserializeObject<List<Note>>(text);
            }
        }
        public void SaveToFile(string name, string body)
        {
            if (list == null) { list = new List<Note>(); }
            bool create = true;
            foreach (var item in list)
            {
                if(item.Name == name)
                {
                    item.Content = body;
                    create = false;
                    break;
                }
            }
            if (create) list.Add(new Note(name, body));
            File.WriteAllText(myPath, JsonConvert.SerializeObject(list));
        }
        public void Remove(string name)
        {
            if (list == null) { return; }
            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    list.Remove(item);
                    break;
                }
            }
            File.WriteAllText(myPath, JsonConvert.SerializeObject(list));
        }
    }
}