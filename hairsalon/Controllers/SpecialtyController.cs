using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtyController : Controller
    {
        [HttpGet("/Specialty/")]
        public ActionResult SpecialtyIndex()
        {
            return View(Specialty.GetAll());
        }
    }
}
