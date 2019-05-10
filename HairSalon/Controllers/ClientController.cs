using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
                          //  THESE ROUTS NEED CHANGED TO BE WITHIN THE STYLISTS routes
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string name, int id)
    {
      Client newClient = new Client(name, id);
      newClient.Save();
      return RedirectToAction("Index");
    }


    [HttpGet("/clients/{id}")]
    public ActionResult Show(int id)
    {
      Client client = Client.Find(id);
      return View(client);
    }
  }
}

// [HttpPost("/clients/delete")]
// public ActionResult DeleteAll()
// {
//   Client.ClearAll();
//   return RedirectToAction("Index");
//
// }
//
// [HttpPost("/clients/{id}/delete")]
// public ActionResult Delete(int id)
// {
//   Client foundClient = Client.Find(id);
//   foundClient.DeleteClient();
//   return RedirectToAction("Index");
//   // return View();
// }
