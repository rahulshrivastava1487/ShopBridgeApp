using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel objUserModel)
        {
            if (Request.HttpMethod == "POST")
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|ShopBridgeDB.mdf;Integrated Security=True;User Instance=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_UserEnrollment", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@FirstName", objUserModel.FirstName);
                            cmd.Parameters.AddWithValue("@MiddleName", objUserModel.MiddleName);
                            cmd.Parameters.AddWithValue("@LastName", objUserModel.LastName);
                            cmd.Parameters.AddWithValue("@EmailID", objUserModel.EmailID);
                            cmd.Parameters.AddWithValue("@AddressLine1", objUserModel.AddressLine1);
                            cmd.Parameters.AddWithValue("@AddressLine2", objUserModel.AddressLine2);
                            cmd.Parameters.AddWithValue("@City", objUserModel.City);
                            cmd.Parameters.AddWithValue("@State", objUserModel.State);
                            cmd.Parameters.AddWithValue("@ZipCode", objUserModel.ZipCode);
                            cmd.Parameters.AddWithValue("@UserName", objUserModel.UserName);
                            cmd.Parameters.AddWithValue("@UserPassword", objUserModel.UserPassword);
                            cmd.Parameters.AddWithValue("@Status", "INSERT");

                            con.Open();

                            ViewData["Result"] = cmd.ExecuteNonQuery();

                            con.Close();
                        }
                    }
                }
                catch(Exception ex)
                {

                }                
            }
            return View();
        }
    }
}