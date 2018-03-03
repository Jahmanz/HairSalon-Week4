using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon
{
  public class StylistController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult StylistIndex()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult StylistCreateForm()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult StylistCreate()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
      newStylist.Save();

      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult StylistDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);

      List<Client> stylistClients = selectedStylist.GetClients();

      List<Client> allClients = Client.GetAll();

      model.Add("stylist", selectedStylist);
      model.Add("stylistClients", stylistClients);
      model.Add("allClients", allClients);

      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/new")]
    public ActionResult AddClient(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(Int32.Parse(Request.Form["client-id"]));
      stylist.AddClient(client);

      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/stylists/{id}/update")]
    public ActionResult StylistUpdateForm(int id)
    {
      Stylist thisStylist = Stylist.Find(id);

      return View("StylistUpdate", thisStylist);
    }

    [HttpPost("/stylist/{id}/update")]
    public ActionResult Update(int id)
    {
      string newName = Request.Form["new-name"];
      Stylist thisStylist = Stylist.Find(id);

      thisStylist.UpdateName(newName);
      return RedirectToAction("StylistIndex");
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult DeleteOne(int stylistId)
    {
      Stylist thisStylist = Stylist.Find(stylistId);
      thisStylist.DeleteOne();

      return RedirectToAction("StylistIndex");
    }
  }
}
