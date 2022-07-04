using Ajax_Core_141_30.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajax_Core_141_30.Controllers
{
    public class ApiController : Controller
    {
        //public IActionResult Index(string name,int age=0)
        private readonly DemoContext _context;

        public ApiController(DemoContext conetxt)  //注入
        {
            _context = conetxt;
        }

        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(5000);   //睡覺
            if (String.IsNullOrEmpty(user.name))
                user.name = "Ajax";
            return Content($"<h1>HI,{user.name}!! 你好, 您今年{user.age}歲</h1>", "text/html", System.Text.Encoding.UTF8);
        }

    }
}
