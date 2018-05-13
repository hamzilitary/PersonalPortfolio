using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalPortfolio.Models
{

    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int CommentKey { get; set; }
        
        [NotMapped]
        public int BlogKey { get; set; }

        public virtual Blog AssociatedBlog { get; set; }
        
        public DateTime ReplyDtm { get; set; }

        public string CommentText { get; set; }
    }
}