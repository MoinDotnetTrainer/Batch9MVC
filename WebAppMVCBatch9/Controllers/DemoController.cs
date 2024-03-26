using Microsoft.AspNetCore.Mvc;
using WebAppMVCBatch9.Models;

namespace WebAppMVCBatch9.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();  // ui
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]  // load event
        public IActionResult DisplayDate()
        {
            ViewData["OnLoadDate"] = System.DateTime.Now.ToLocalTime();
            return View();
        }

        [HttpPost]  //button click event
        public IActionResult DisplayDate(string s)
        {
            ViewData["OnclickDate"] = System.DateTime.Now.ToLocalTime();
            return View();
        }


        [HttpGet]// on load 
        public IActionResult Addition()
        {
            ViewData["res"] = "No result on Load";
            return View();  // ui
        }

        [HttpPost]  // works as click event
        public IActionResult Addition(string s)
        {
            int x = int.Parse(Request.Form["txt1"].ToString());
            int y = int.Parse(Request.Form["txt2"].ToString());
            int z = x + y;
            ViewData["res"] = "Result after click:" + z;
            return View();  // ui
        }


        [HttpGet]// on load 
        public IActionResult Sub()
        {
            ViewData["res"] = "No result on Load";
            return View();  // ui
        }

        [HttpPost]  // works as click event
        public IActionResult Sub(Values obj)
        {
            int val1 = obj.x;
            int val2 = obj.y;
            int res = val1 - val2;
            ViewData["res"] = "Result after click:" + res;
            return View();  // ui
        }

    }
}
