using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
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
    public ActionResult Create(string name, int id)
    {
      Stylist newStylist = new Stylist(name, id);
      newStylist.Save();
      return RedirectToAction("Index");
    }

    // New Stylists
    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      List<Specialty> allSpecialties = Specialty.GetAll();
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      model.Add("specialties", allSpecialties);
      model.Add("stylistSpecialties", stylistSpecialties);
      return View(model);
    }

    // New Specialty
    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult AddSpecialty(int stylistId, int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Stylist stylist = Stylist.Find(stylistId);
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    // New Clients
    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      List<Specialty> allSpecialties = Specialty.GetAll();
      List<Specialty> stylistSpecialties = foundStylist.GetSpecialties();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      model.Add("specialties", allSpecialties);
      model.Add("stylistSpecialties", stylistSpecialties);
      return View("Show", model);
    }

    [HttpPost("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.ClearAll();
      return View();
    }

    [HttpPost("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Delete();
      return View();
    }

    [HttpGet("/stylists/{stylistId}/edit")]
    public ActionResult Edit(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    [HttpPost("/stylists/{stylistId}")]
    public ActionResult Update(int stylistId, string newName)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Edit(newName);
      List<Client> stylistClients = stylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylist", stylist);
      model.Add("clients", stylistClients);
      return View("Show", model);
    }

  }
}
