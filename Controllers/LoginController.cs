using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    public class LoginController : Controller
    {
        DBHelper objDBHelper = new DBHelper();

        // GET: Login
        public string Status;
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoginUser(UserModel objUser)
        {
            var User = objDBHelper.GetUsers().Find(x => x.UserName.Equals(objUser.UserName) && x.UserPassword.Equals(objUser.UserPassword));

            if (User != null)
            {
                if (!string.IsNullOrEmpty(User.UserName))
                {
                    FormsAuthentication.SetAuthCookie(User.UserName, false);
                    Session["UserName"] = User.FirstName + " " + User.LastName;
                    Session["User"] = User.UserName;
                }
            }

            return Json(User, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult Welcome(UserModel e)
        //{
        //    UserModel user = new UserModel();
        //    DataSet ds = new DataSet();

        //    using (SqlConnection con = new SqlConnection("Data Source=PRIYANKA\\SQLEXPRESS;Integrated Security=true;Initial Catalog=Sample"))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("sp_GetEnrollmentDetails", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Session["Email"].ToString();
        //            con.Open();
        //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //            sda.Fill(ds);
        //            List<UserModel> userlist = new List<UserModel>();
        //            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //            {
        //                UserModel uobj = new UserModel();
        //                uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
        //                uobj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
        //                uobj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
        //                uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
        //                uobj.Email = ds.Tables[0].Rows[i]["Email"].ToString();
        //                uobj.PhoneNumber = ds.Tables[0].Rows[i]["Phone"].ToString();
        //                uobj.SecurityAnwser = ds.Tables[0].Rows[i]["SecurityAnwser"].ToString();
        //                uobj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();

        //                userlist.Add(uobj);

        //            }
        //            user.Enrollsinfo = userlist;
        //        }
        //        con.Close();

        //    }
        //    return View(user);
        //}
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}