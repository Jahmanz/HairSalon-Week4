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
      return View("stylists",allStylists);
    }
    [HttpGet("/stylists/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/stylists")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("stylists", allStylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetAllClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      return View("details", model);
    }
    [HttpPost("/stylists/{id}")]
    public ActionResult Add(int id)
    {
      Client newClient = new Client (Request.Form["client-name"],Request.Form["client-email"],Int32.Parse(Request.Form["stylist-id"]));
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return RedirectToAction("Details");
    }
    [HttpGet("/stylists/{id}/client/new")]
    public ActionResult CreateClientForm()
    {
      return View("Success");
    }
  }
}
