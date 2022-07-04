using Ajax_Core_141_30.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajax_Core_141_30.Controllers
{
    public class HW2Controller : Controller
    {
        private readonly DemoContext _context;

        public HW2Controller(DemoContext context)  //注入
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomeWork2()
        {
            return View();
        }
        public IActionResult hw2Register(User user)
        {  

            Member mb = _context.Members.FirstOrDefault(n => n.Email == user.email);
            if (String.IsNullOrEmpty(user.email))
            {
             user.emailState = "請填入信箱";
            }    
            else if(mb!=null)
             {
                user.emailState = "帳號已存在";
            }
            else
            {
               user.emailState = "帳號可使用";
            }
               


            return Content(user.emailState, "text/html", System.Text.Encoding.UTF8);
        }
    }
}
