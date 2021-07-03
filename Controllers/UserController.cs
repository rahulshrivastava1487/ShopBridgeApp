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
        DBHelper objDBHelper = new DBHelper();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AddUser(UserModel objUser)
        {
            return Json(objDBHelper.AddUser(objUser), JsonRequestBehavior.AllowGet);
        }
    }
}