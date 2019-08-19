using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Visportfolio.Models;

namespace Visportfolio.Pages
{
    public class CreatePortfolioModel : PageModel
    {
        public ICategoryService categoryService;
        [BindProperty(SupportsGet =true)]
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public SelectList categorylist { get; set; }
        public DatabaseContext _context { get; set; }

        public CreatePortfolioModel(ICategoryService categoryService, DatabaseContext context)
        {
            this.categoryService = categoryService;
            _context = context;
        }

        public void OnGet()
        {
             categorylist = new SelectList(categoryService.GetCategories(), nameof(Category.CategoryId), nameof(Category.CategoryName));
        }

        public JsonResult OnGetSubCategories()
        {
            return new JsonResult(categoryService.GetSubCategories(CategoryId));
        }
    }
}