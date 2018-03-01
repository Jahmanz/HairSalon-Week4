using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

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
        public ActionResult CreateForm()
        {
          return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
          Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-specialty"]);
          newStylist.Save();
          return RedirectToAction("Success", "Home");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult StylistsDetail(int id)
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
        [HttpGet("/stylists/{stylistId}/update")]
        public ActionResult UpdateForm(int stylistId)
        {
          Stylist thisStylist = Stylist.Find(stylistId);
          return View("update", thisStylist);
        }

        [HttpPost("/stylists{stylistId}/update")]
        public ActionResult Update(int stylistId)
        {
          Stylist thisStylist = Stylist.Find(stylistId);
          thisStylist.Edit(Request.Form["newname"],Request.Form["newspecialty"]);
          return RedirectToAction("Index");
        }

        [HttpGet("/stylists/{stylistId}/delete")]
        public ActionResult DeleteOne(int stylistId)
        {
          Stylist thisStylist = Stylist.Find(stylistId);
          thisStylist.Delete();
          return RedirectToAction("index");
        }

        [HttpPost("/stylists/delete")]
        public ActionResult DeleteAll()
        {
          Stylist.DeleteAll();
          return View();
        }
    }
}
