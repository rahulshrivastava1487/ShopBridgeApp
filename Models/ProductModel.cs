using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Models
{
    public class ProductModel
    {
        [Display(Name ="Product ID")]
        public int ProductID { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Display(Name ="Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name ="Price")]
        public int Price { get; set; }
    }
}