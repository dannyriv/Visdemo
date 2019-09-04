using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Visportfolio.Models;

namespace Visportfolio.Pages
{
    /// <Continue>
    /// 1. Implement ajax call in order to display forms only upon button clicks
    /// 2. Create OnPost for submitting SubCategory form to database
    /// 3. Create Grid or Table to display Projects that exist in a SubCategory
    /// </Continue>
    public class CreatePortfolioModel : PageModel
    {
        public ICategoryService categoryService;
        [BindProperty(SupportsGet =true)]
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public Category Category { get; set; }  //Not sure if needed
        public SubCategory SubCategory { get; set; }    //Not sure if needed
        [BindProperty]
        public Project Project { get; set; }    
        public SelectList categorylist { get; set; }
        public DatabaseContext _context { get; set; }

        public CreatePortfolioModel(ICategoryService categoryService, DatabaseContext context)
        {
            this.categoryService = categoryService;
            _context = context;
            Project = new Project();
        }

        //Used to populate the category dropdownlist
        public void OnGet()
        {
             categorylist = new SelectList(categoryService.GetCategories(), nameof(Category.CategoryId), nameof(Category.CategoryName));
        }

        //Used to populate the subcategory dropdownlist
        public JsonResult OnGetSubCategories()
        {
            return new JsonResult(categoryService.GetSubCategories(CategoryId));
        }

        //Used to populate the project grid
        public JsonResult OnGetProjects()
        {
            return new JsonResult(categoryService.GetProjects(SubCategoryId));
        }

        public IActionResult OnPost()
        {
            Project.ProjectId = 0;
            Project.SubCategoryId = 6;
            var result = _context.Add(Project);
            _context.SaveChanges();

            Project = new Project();

            return Page();
        }

    }
}