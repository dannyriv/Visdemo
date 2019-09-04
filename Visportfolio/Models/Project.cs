using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Visportfolio.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public String ProjectName { get; set; }
        public String ProjectDescription { get; set; }
        public int FileUpload { get; set; }
        public int SubCategoryId { get; set; }
    }
}
