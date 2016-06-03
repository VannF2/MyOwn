using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lab6
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index"}
            );

            routes.MapRoute(
                name: "CreateNotepad",
                url: "Notepad/Create",
                defaults: new { controller = "Notepad", action = "Create" }
            );

            routes.MapRoute(
                name: "ImageNotepad",
                url: "Notepad/Image",
                defaults: new { controller = "Notepad", action = "Image", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SaveNotepad",
                url: "Notepad/Save",
                defaults: new { controller = "Notepad", action = "Save", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LoadNotepad",
                url: "Notepad/Load",
                defaults: new { controller = "Notepad", action = "Load"}
            );

            routes.MapRoute(
                name: "RemoveNotepad",
                url: "Notepad/Remove",
                defaults: new { controller = "Notepad", action = "Remove" }
            );

            routes.MapRoute(
                name:"NoteBlin",
                url: "Notepad/{name}",
                defaults: new { controller = "Notepad", action = "Select", name = "{name}" }
                );
        }
    }
}
