using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon
{
  public class SpecialtyController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult SpecialtyIndex()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult SpecialtyCreateForm()
    {
      return View();
    }

    [HttpPost("/specialties")]
        public ActionResult SpecialtyCreate()
        {
            string name = Request.Form["specialty-name"];
            Specialty mySpecialty = new Specialty(name);
            mySpecialty.Save();
            Console.WriteLine(mySpecialty.GetId());

            return View("Info", mySpecialty);
        }
    //
    // [HttpPost("/specialties")]
    // public ActionResult SpecialtyCreate()
    // {
    //   Specialty newSpecialty = new Specialty(Request.Form["specialty-name"],Request.Form["specialty-specialty"]);
    //   newSpecialty.Save();
    //
    //   return RedirectToAction("Success", "Home");
    // }

    [HttpGet("/specialties/{id}")]
    public ActionResult SpecialtyDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);

      List<Stylist> specialtyStylists = selectedSpecialty.GetStylists();

      List<Stylist> allStylists = Stylist.GetAll();

      model.Add("specialty", selectedSpecialty);
      model.Add("specialtyStylists", specialtyStylists);
      model.Add("allStylists", allStylists);

      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult AddStylist(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      Stylist stylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
      specialty.AddStylist(stylist);

      return RedirectToAction("Success", "Home");
    }

    [HttpGet("/specialties/{id}/update")]
    public ActionResult SpecialtyUpdateForm(int id)
    {
      Specialty thisSpecialty = Specialty.Find(id);

      return View("SpecialtyUpdate", thisSpecialty);
    }

    [HttpPost("/specialty/{id}/update")]
    public ActionResult Update(int id)
    {
      string newSpecialty = Request.Form["specialty-name"];
      Specialty thisSpecialty = Specialty.Find(id);

      thisSpecialty.UpdateSpecialty(newSpecialty);
      return RedirectToAction("SpecialtyIndex");
    }

    [HttpGet("/specialties/{specialtyId}/delete")]
    public ActionResult DeleteOne(int specialtyId)
    {
      Specialty thisSpecialty = Specialty.Find(specialtyId);
      thisSpecialty.DeleteOne();

      return RedirectToAction("SpecialtyIndex");
    }
  }
}
