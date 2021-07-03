using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class DBHelper
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
        
        /// <summary>
        /// This method returns list of all available products.
        /// </summary>
        /// <returns>List</returns>
        public List<ProductModel> ListAllProducts()
        {
            List<ProductModel> lst = new List<ProductModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_GetAllProducts", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new ProductModel
                    {
                        ProductID = Convert.ToInt32(rdr["ProductID"]),
                        ProductName = Convert.ToString(rdr["ProductName"]),
                        ProductDescription = Convert.ToString(rdr["ProductDescription"]),
                        Price = Convert.ToInt32(rdr["ProductPrice"])
                    });
                }
                return lst;
            }
        }

        /// <summary>
        /// Add a product to list
        /// </summary>
        /// <param name="objProduct"></param>
        /// <returns>ID</returns>
        public int AddProduct(ProductModel objProduct)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_InsertUpdateProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductID", objProduct.ProductID);
                com.Parameters.AddWithValue("@ProductName", objProduct.ProductName);
                com.Parameters.AddWithValue("@ProductDescription", objProduct.ProductDescription);
                com.Parameters.AddWithValue("@ProductPrice", objProduct.Price);
                com.Parameters.AddWithValue("@Action", "INSERT");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="objProduct"></param>
        /// <returns>ID</returns>
        public int UpdateProduct(ProductModel objProduct)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_InsertUpdateProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductID", objProduct.ProductID);
                com.Parameters.AddWithValue("@ProductName", objProduct.ProductName);
                com.Parameters.AddWithValue("@ProductDescription", objProduct.ProductDescription);
                com.Parameters.AddWithValue("@ProductPrice", objProduct.Price);
                com.Parameters.AddWithValue("@Action", "UPDATE");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        /// <summary>
        /// Delete a product from list
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>ID</returns>
        public int DeleteProduct(int ID)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_DeleteProduct", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@ProductID", ID);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int AddUser(UserModel objUser)
        {
            int i;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_UserEnrollment", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FirstName", objUser.FirstName);
                com.Parameters.AddWithValue("@MiddleName", objUser.MiddleName == null ? "NA" : objUser.MiddleName);
                com.Parameters.AddWithValue("@LastName", objUser.LastName);
                com.Parameters.AddWithValue("@EmailID", objUser.EmailID);
                com.Parameters.AddWithValue("@MobileNumber", objUser.MobileNumber);
                com.Parameters.AddWithValue("@AddressLine1", objUser.AddressLine1 == null ? "NA" : objUser.AddressLine1);
                com.Parameters.AddWithValue("@AddressLine2", objUser.AddressLine2 == null ? "NA" : objUser.AddressLine2);
                com.Parameters.AddWithValue("@City", objUser.City == null ? "NA" : objUser.City);
                com.Parameters.AddWithValue("@State", objUser.State == null ? "NA" : objUser.State);
                com.Parameters.AddWithValue("@ZipCode", objUser.ZipCode);
                com.Parameters.AddWithValue("@UserName", objUser.UserName);
                com.Parameters.AddWithValue("@UserPassword", objUser.UserPassword);
                com.Parameters.AddWithValue("@Status", "INSERT");
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> lst = new List<UserModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand com = new SqlCommand("sp_GetAllUsers", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    lst.Add(new UserModel
                    {
                        FirstName = Convert.ToString(rdr["FirstName"]),
                        LastName = Convert.ToString(rdr["LastName"]),
                        UserName = Convert.ToString(rdr["UserName"]),
                        UserPassword = Convert.ToString(rdr["UserPassword"])
                    });
                }
                return lst;
            }
        }
    }
}