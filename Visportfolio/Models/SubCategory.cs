using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models
{
    [Table("SubCategory")]

    public class SubCategory
    {
        [Key]
        public int SubCategoryId { get; set;}
        public String SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
