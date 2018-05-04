using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalPortfolio.Models
{
    [Table("Blogs")]
    public class Blog
    {
        [Key]
        public int BlogKey { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
