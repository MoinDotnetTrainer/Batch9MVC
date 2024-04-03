using Microsoft.AspNetCore.Mvc;
using WebAppMVCBatch9.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using System.Data;

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
                        HttpContext.Session.SetString("username", dr["name"].ToString());
                        HttpContext.Session.SetString("Logintime", System.DateTime.Now.ToLongTimeString());
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
        }


        [SetSessionGlobally]  // action filter
        public IActionResult HomePage()
        {
            return View();
        }


        [SetSessionGlobally]  // action filter
        public IActionResult DisplayData()
        {
            List<DisplayModel> obj = new List<DisplayModel>();
            using (con = new SqlConnection(conn))
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_getalldata", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    obj.Add(
                        new DisplayModel
                        {
                            ID = Convert.ToInt32(dr["Id"].ToString()),
                            Name = dr["Name"].ToString(),
                            EmailID = dr["EmailID"].ToString(),
                            Password = dr["Password"].ToString(),
                            Dob = Convert.ToDateTime(dr["Dob"].ToString()),
                            Mobile = dr["Mobile"].ToString(),
                            Gender = dr["Gender"].ToString(),
                            Dept = dr["Dept"].ToString(),
                            Salary = Convert.ToInt32(dr["Salary"].ToString()),
                            Status = Convert.ToBoolean(dr["Status"].ToString())
                        }
                        );
                }
            }
            return View(obj);
        }


        [HttpGet]//onload
        public IActionResult Edit(int ID)
        {
            RegisterModel obj = null;
            using (con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sP_getdatabyid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    obj = new RegisterModel();
                    obj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                    obj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    obj.EmailID = ds.Tables[0].Rows[i]["EmailID"].ToString();
                    obj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    obj.Dob = Convert.ToDateTime(ds.Tables[0].Rows[i]["Dob"].ToString());
                    obj.Mobile = ds.Tables[0].Rows[i]["Mobile"].ToString();
                    obj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();
                    obj.Dept = ds.Tables[0].Rows[i]["Dept"].ToString();
                    obj.Salary = Convert.ToInt32(ds.Tables[0].Rows[i]["Salary"].ToString());
                    obj.Status = Convert.ToBoolean(ds.Tables[0].Rows[i]["Status"].ToString());

                }
            }
            return View(obj);
        }


        [HttpPost]
        public IActionResult Edit(RegisterModel obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (con = new SqlConnection(conn))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_Update_Curd", con);
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
                        cmd.Parameters.AddWithValue("@id", obj.ID);
                        int x = cmd.ExecuteNonQuery();
                        if (x > 0)
                        {
                            return RedirectToAction("DisplayData", "Curd");
                        }
                        else
                        {
                            return View();
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }

        public IActionResult Delete(int ID)
        {
            using (con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_delete", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return RedirectToAction("Displaydata", "Curd");
                }

            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Curd");
        }

    }
}
