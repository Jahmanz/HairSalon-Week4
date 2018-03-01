using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {

        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<Client> allClients = Client.GetAll();
            return View(allClients);
        }

        [HttpGet("/clients/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/clients")]
        public ActionResult Create()
        {
          Client newClient = new Client(Request.Form["client-name"],Request.Form["client-email"]);
          newClient.Save();
          return RedirectToAction("Success", "Home");
        }
        //ONE TASK
        [HttpGet("/clients/{id}")]
        public ActionResult Details(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Client selectedClient = Client.Find(id);
          List<Stylist> clientStylists = selectedClient.GetStylists();
          List<Stylist> allStylists = Stylist.GetAll();
          model.Add("client", selectedClient);
          model.Add("clientStylists", clientStylists);
          model.Add("allStylists", allStylists);
          return View(model);
        }

        [HttpGet("/clients/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
            Client thisClient = Client.Find(id);
            return View(thisClient);
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult Update(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.Edit(Request.Form["newname"],Request.Form["newemail"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/clients/{clientId}/stylists/new")]
        public ActionResult AddStylist(int clientId)
        {
            Client client = Client.Find(clientId);
            Stylist category = Stylist.Find(Int32.Parse(Request.Form["category-id"]));
            client.AddStylist(category);
            return RedirectToAction("Success", "Home");
        }
        [HttpGet("/clients{id}/delete")]
        public ActionResult DeleteOne(int id)
        {
            Client thisClient = Client.Find(id);
            thisClient.Delete();
            return RedirectToAction("index");
        }

        [HttpPost("/clients/delete")]
        public ActionResult DeleteAll()
        {
          Client.DeleteAll();
          return View();
        }
    }
}
