﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Name = "dfsadfhd";
            return View();
        }
        [Route("abc")]
        public string Something()
        {
            return "sdfg";
        }

    }
}