﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controlers
{
    
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
