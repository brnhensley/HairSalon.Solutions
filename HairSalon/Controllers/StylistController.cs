// using System.Collections.Generic;
// using System;
// using Microsoft.AspNetCore.Mvc;
// using HairSalon.Models;
//
// namespace HairSalon.Controllers
// {
//   public class CategoriesController : Controller
//   {
//
//     [HttpGet("/stylists")]
//     public ActionResult Index()
//     {
//       List<Category> allCategories = Category.GetAll();
//       return View(allCategories);
//     }
//
//     [HttpGet("/stylists/new")]
//     public ActionResult New()
//     {
//       return View();
//     }
//
//     [HttpPost("/stylists")]
//     public ActionResult Create(string stylistName)
//     {
//       Category newCategory = new Category(stylistName);
//       List<Category> allCategories = Category.GetAll();
//       return View("Index", allCategories);
//     }
//
//     [HttpGet("/stylists/{id}")]
//     public ActionResult Show(int id)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category selectedCategory = Category.Find(id);
//       List<Client> stylistClients = selectedCategory.GetClients();
//       model.Add("stylist", selectedCategory);
//       model.Add("clients", stylistClients);
//       return View(model);
//     }
//
//     // This one creates new Clients within a given Category, not new Categories:
//     [HttpPost("/stylists/{stylistId}/clients")]
//     public ActionResult Create(int stylistId, string clientDescription)
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Category foundCategory = Category.Find(stylistId);
//       Client newClient = new Client(clientDescription);
//       newClient.Save();
//       foundCategory.AddClient(newClient);
//       List<Client> stylistClients = foundCategory.GetClients();
//       model.Add("clients", stylistClients);
//       model.Add("stylist", foundCategory);
//       return View("Show", model);
//     }
//
//   }
// }
