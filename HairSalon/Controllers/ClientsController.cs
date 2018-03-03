using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon
{
  public class ClientController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult ClientIndex()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult ClientCreateForm()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult ClientCreate()
    {
      string name = Request.Form["client-name"];
      string email = Request.Form["client-email"];
      Client newClient = new Client(name, email);
      newClient.Save();

      return RedirectToAction("ClientIndex");
    }

    [HttpGet("/clients/{id}")]
    public ActionResult ClientDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client selectedClient = Client.Find(id);

      List<Stylist> clientStylists = selectedClient.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();

      model.Add("client", selectedClient);
      model.Add("clientStylists", clientStylists);
      model.Add("allStylists", allStylists);

      return View("ClientDetails",model);
    }

    [HttpPost("/clients/{clientId}/stylists/new")]
    public ActionResult AddStylist(int clientId)
    {
      Client client = Client.Find(clientId);
      Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
      client.AddStylist(stylist);

      return View("Success");
    }

    [HttpGet("/clients/{id}/update")]
    public ActionResult ClientUpdateForm(int id)
    {
      Client thisClient = Client.Find(id);

      return View("ClientUpdate", thisClient);
    }

    [HttpPost("/clients/{id}/update")]
    public ActionResult Update(int id)
    {
      string newName = Request.Form["newname"];
      Client thisClient = Client.Find(id);

      thisClient.UpdateName(newName);
      return RedirectToAction("ClientDetails");
    }

    [HttpGet("/clients/{clientId}/delete")]
    public ActionResult DeleteOne(int clientId)
    {
      Client thisClient = Client.Find(clientId);
      thisClient.DeleteOne();

      return RedirectToAction("ClientIndex");
    }
  }
}
