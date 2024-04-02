using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebAppMVCBatch9.Models;

namespace WebAppMVCBatch9.Controllers
{
    public class Std
    {
        public string Sname { get; set; }
    }
    public class ViewbagExampleController : Controller
    {
        public readonly string conn;
        SqlConnection con;
        public ViewbagExampleController()
        {
            var dbconfig = new ConfigurationBuilder().
                    SetBasePath(Directory.GetCurrentDirectory()).
                    AddJsonFile("appsettings.json").Build();
            conn = dbconfig["ConnectionStrings:Constr"];
        }
        public IActionResult StoringData()// In this action we have strored the data
        {
            ViewBag.key1 = "Some Data from ViewBag" + System.DateTime.Now.ToShortTimeString();
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


        public IActionResult StdData()
        {
            List<Std> obj = new List<Std>();
            obj.Add(new Std { Sname = "Raj" });
            obj.Add(new Std { Sname = "test" });
            obj.Add(new Std { Sname = "xyz" });
            obj.Add(new Std { Sname = "abc" });
            TempData["res"] = obj;
            //ViewBag.res = obj;

            return View();
        }

        [HttpGet]
        public IActionResult PopulateDropdown()
        {
            ViewBag.results = getData();
            return View();
        }
        public List<CountryModel> getData()
        {
            List<CountryModel> obj = new List<CountryModel>();

            using (con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select cname from country", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    obj.Add(new CountryModel
                    {
                        CName = dr["cname"].ToString()
                    });
                }
            }
            return obj;
        }

    }
}
