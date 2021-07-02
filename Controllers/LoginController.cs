﻿using System;
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
        // GET: Login
        public string Status;
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel e)
        {
            String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
            SqlConnection con = new SqlConnection(SqlCon);
            string SqlQuery = "select Email,Password from Enrollment where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@UserName", e.UserName);
            cmd.Parameters.AddWithValue("@UserPassword", e.UserPassword);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["UserName"] = e.UserName.ToString();
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (e.UserName.ToString() != null)
            {
                Session["UserName"] = e.UserName.ToString();
                Status = "1";
            }
            else
            {
                Status = "3";
            }

            con.Close();
            return View();
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