using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Visportfolio.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<VisUser> VisUser { get; set; }
        string connectionString = "Data Source=DESKTOP-6QK329A\\SQLEXPRESS; UID=sa; Password=Chingo123; Database=VispiresTest;";

        public List<Category> GetAllCategoires()
        {
            List<Category> lstcategory = new List<Category>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_SelectCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Category category = new Category();

                        category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                        category.CategoryName = rdr["CategoryName"].ToString();

                        lstcategory.Add(category);
                    }
                    con.Close();
                }

            }
            return lstcategory;
        }
        public List<SubCategory> GetAllSubCategories(int CategoryId)
        {
            List<SubCategory> lstsubcategory = new List<SubCategory>();

            using (SqlConnection con = new SqlConnection(connectionString))//
            {
                using (SqlCommand cmd = new SqlCommand("sp_SelectSubCategories", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(("@CategoryId"), SqlDbType.Int).Value = CategoryId;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        SubCategory subcategory = new SubCategory();

                        subcategory.SubCategoryId = Convert.ToInt32(rdr["SubCategoryId"]);
                        subcategory.SubCategoryName = rdr["SubCategoryName"].ToString();

                        lstsubcategory.Add(subcategory);
                    }
                    con.Close();
                }
            }
            return lstsubcategory;
        }

    }
}
