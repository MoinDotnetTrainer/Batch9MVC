using Microsoft.AspNetCore.Mvc;

namespace WebAppMVCBatch9.Controllers
{
    
    public class CookieExampleController : Controller
    {
        public IActionResult StoreData()
        {
            CookieOptions obj = new CookieOptions();
            obj.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("key1", "some value", obj);
            Response.Cookies.Append("key2", "some other value", obj);
            return View();
        }
        public IActionResult RetriveData()
        {
            return View();
        }

        }
}
