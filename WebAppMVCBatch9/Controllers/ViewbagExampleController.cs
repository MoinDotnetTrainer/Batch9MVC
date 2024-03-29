using Microsoft.AspNetCore.Mvc;

namespace WebAppMVCBatch9.Controllers
{
    public class ViewbagExampleController : Controller
    {
        public IActionResult StoringData()// In this action we have strored the data
        {
            ViewBag.key1 = "Some Data from ViewBag"+ System.DateTime.Now.ToShortTimeString();
            ViewData["key2"] = "Some Data from view data" + System.DateTime.Now.ToShortTimeString();
            TempData["key3"] = "Some Data from tempdata" + System.DateTime.Now.ToShortTimeString();
         //   return View();
            return RedirectToAction("somedatahere", "ViewbagExample");
        }

        public IActionResult getDatahere()
        {
            return View();
        }

        public IActionResult somedatahere()
        {
            return View();
        }
    }
}
