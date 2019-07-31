using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Visportfolio.Models;
using Visportfolio.Models.DB;

namespace Visportfolio.Pages
{
    public class CreatePortfolioModel : PageModel
    {

        CategoryDAL objCategory = new CategoryDAL();
        public List<CategoryTB> category { get; set; }


        public void OnGet()
        {
            category = objCategory.GetAllCategoires().ToList();
        }
        
    }
}