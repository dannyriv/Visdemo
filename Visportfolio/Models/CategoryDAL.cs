using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models.DB
{
    public class CategoryDAL
    {

        string connectionString = "Data Source=DANNY\\SQLEXPRESS; UID=sa; Password=Chingo123; Database=VispitesTest;";

        //To View all employees details    
        public IEnumerable<CategoryTB> GetAllCategoires()
        {
            List<CategoryTB> lstcategory = new List<CategoryTB>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SelectCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    CategoryTB category = new CategoryTB();

                    category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    category.CategoryName = rdr["CategoryName"].ToString();


                    lstcategory.Add(category);
                }
                con.Close();
            }
            return lstcategory;
        }

    }
}
