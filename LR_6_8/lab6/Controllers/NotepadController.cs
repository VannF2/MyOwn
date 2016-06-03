using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab6.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace lab6.Controllers
{
    public class NotepadController : Controller
    {
        Notepad filedb = new Notepad();
        public ActionResult Select(string name)
        {
            ViewBag.notepadname = name;
            return View("../Home/Index");
        }

        public ActionResult Save([JsonBinder] Note note)
        {
            filedb.SaveToFile(note.Name, note.Content);
            var message = string.Format("Success added: {0} : {1}", note.Name, note.Content);
            return Json(new { message });
        }

        public JsonResult Remove(string Name)
        {
            filedb.Remove(Name);
            var message = string.Format("Success removed: {0}", Name);
            return Json(new { message });
        }

        [HttpGet]
        public JsonResult Load()
        {
            filedb.ReadAllNotepad();
            return Json(new {filedb._list}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Image(string name)
        {
            var stream = name.ToImage().ToStream();
            return File(stream, "image/png");
        }

        public ActionResult Create()
        {
            return PartialView("_Create");
        }
    }
}
