using Ajax_Core_141_30.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ajax_Core_141_30.Controllers
{
    public class ApiController : Controller
    {
        //public IActionResult Index(string name,int age=0)
        private readonly DemoContext _dbcontext;
        private readonly IWebHostEnvironment _hostEnvironment;  //應用程式執行的伺服器環境

        public ApiController(DemoContext conetxt,IWebHostEnvironment hostEnvironment)  //注入
        {
            _dbcontext = conetxt;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(User user)
        {
            //System.Threading.Thread.Sleep(5000);   //睡覺
            if (String.IsNullOrEmpty(user.name))
                user.name = "Ajax";
            return Content($"<h1>HI,{user.name}!! 你好, 您今年{user.age}歲</h1>", "text/html", System.Text.Encoding.UTF8);
        }
        public IActionResult Register(Member member, IFormFile file)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, "uploads", file.FileName);  //wwwroot路徑和uploads資料夾名稱合併
            using (var fileStream = new FileStream(path, FileMode.Create))   //用完後關掉FileStream
            {
                file.CopyTo(fileStream);   //存檔
            }
            byte[] imgbyte=null;   //2進位byte陣列
            using(var memoryStream= new MemoryStream())
            {
                file.CopyTo(memoryStream);   //存到記憶體
                imgbyte = memoryStream.ToArray();   //轉成陣列
            }
            member.FileName = file.FileName;
            member.FileData = imgbyte;
            _dbcontext.Members.Add(member);  //存到db
            _dbcontext.SaveChanges();

            string info = $"{file.FileName} - {file.ContentType} - {file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);

        }

        //讀取所有城市的資料
        public IActionResult City()
        {
            var cities = _dbcontext.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }

        //根據城市名稱讀出鄉鎮區的資料
        public IActionResult Districts(string city)
        {
            var districts = _dbcontext.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }

        //根據鄉鎮區的資料讀出路名
        public IActionResult Roads(string district)
        {
            var roads = _dbcontext.Addresses.Where(a => a.SiteId == district).Select(a => a.Road);
            return Json(roads);
        }
        public IActionResult GetImageBytes(int id = 1)   //預設pK的id
        {
            Member member = _dbcontext.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
            //https://localhost:44372/api/GetimageBytes?id=2
        }
        public IActionResult Members()
        {
            return Json(_dbcontext.Members);
        }

    }
}
