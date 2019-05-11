using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{

    public class ClientsController : Controller
    {
        [HttpGet("/stylists/{stylistId}/clients/new")]
        public ActionResult New(int stylistId)
        {
            Stylist stylist = Stylist.Find(stylistId);
            return View(stylist);
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string name, int id)
        {
          Stylist newStylist = new Stylist(name, id);
          newStylist.Save();
          return RedirectToAction("Index");
        }
    }
}
