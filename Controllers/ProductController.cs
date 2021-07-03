using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    public class ProductController : Controller
    {
        DBHelper objDBHelper = new DBHelper();
        // GET: Product
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View("List");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        [Authorize]
        public JsonResult List()
        {
            return Json(objDBHelper.ListAllProducts(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(ProductModel objProduct)
        {
            return Json(objDBHelper.AddProduct(objProduct), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var Product = objDBHelper.ListAllProducts().Find(x => x.ProductID.Equals(ID));
            return Json(Product, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(ProductModel objProduct)
        {
            return Json(objDBHelper.UpdateProduct(objProduct), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(objDBHelper.DeleteProduct(ID), JsonRequestBehavior.AllowGet);
        }
    }
}