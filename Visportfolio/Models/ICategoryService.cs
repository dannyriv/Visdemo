﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<SubCategory> GetSubCategories(int CategoryId);
        IEnumerable<Project> GetProjects(int SubCategoryId);
    }

    public class CategoryService : ICategoryService
    {
        DatabaseContext _context { get; set; }

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = _context.GetAllCategoires();
            return categories;
        }
        public IEnumerable<SubCategory> GetSubCategories(int CategoryId)
        {
            List<SubCategory> subcategories = _context.GetAllSubCategories(CategoryId);
            return subcategories;
        }
        public IEnumerable<Project> GetProjects(int SubCategoryId)
        {
            List<Project> projects = _context.GetAllProjects(SubCategoryId);
            return projects;
        }
    }
}
