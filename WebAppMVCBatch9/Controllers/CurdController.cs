using Microsoft.AspNetCore.Mvc;
using WebAppMVCBatch9.Models;
using System.Data.SqlClient;

namespace WebAppMVCBatch9.Controllers
{

    public class CurdController : Controller
    {
        public readonly string conn;
        SqlConnection con;
        public CurdController()
        {
            var dbconfig = new ConfigurationBuilder().
                    SetBasePath(Directory.GetCurrentDirectory()).
                    AddJsonFile("appsettings.json").Build();
            conn = dbconfig["ConnectionStrings:Constr"];

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel obj)
        {
            if (ModelState.IsValid)  // bool 
            {
                try
                {
                    con = new SqlConnection(conn);
                    con.Open();
                    SqlCommand cmd = new("sp_login_curd", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@emailid", obj.EmailID);
                    cmd.Parameters.AddWithValue("@password", obj.Password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        return RedirectToAction("HomePage", "Curd");
                    }
                    else
                    {
                        ViewData["errormsg"] = "EmailID or Password is not correct";
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }


            return View();
        }



        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModel obj)
        {
            try
            {
                con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_insert_Curd", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;  // we are passing sp in Cmd class
                cmd.Parameters.AddWithValue("@name", obj.Name);
                cmd.Parameters.AddWithValue("@EmailID", obj.EmailID);
                cmd.Parameters.AddWithValue("@password", obj.Password);
                cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(obj.Dob));
                cmd.Parameters.AddWithValue("@mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@gender", obj.Gender);
                cmd.Parameters.AddWithValue("@dept", obj.Dept);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(obj.Salary));
                cmd.Parameters.AddWithValue("@status", Convert.ToBoolean(obj.Status));
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return RedirectToAction("Login", "Curd");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult DisplayData()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
