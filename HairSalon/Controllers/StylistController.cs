using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("stylists", selectedStylist);
      model.Add("clients", stylistClients);
      return View(model);
    }

    // This one creates new Clients within a given Stylist, not new Stylists:
    [HttpPost("/stylists/{stylistId}/items/new")]
    public ActionResult AddClient(int stylistId, int clientId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client newClient = Client.Find(clientId);
      stylist.AddClient(newClient);
      return RedirectToAction("Show", new { id = stylistId });
    }


    // [HttpPost("/stylists/{stylistId}/clients")]
    // public ActionResult Create(int stylistId, string clientName)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist foundStylist = Stylist.Find(stylistId);
    //   Client newClient = new Client(clientName, stylistId);
    //   newClient.Save();
    //   foundStylist.AddClient(newClient);
    //   List<Client> stylistClients = foundStylist.GetClients();
    //   model.Add("clients", stylistClients);
    //   model.Add("stylists", foundStylist);
    //   return View("Show", model);
    // }

  }
}
