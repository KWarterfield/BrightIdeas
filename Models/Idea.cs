using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId {get;set;}

        [Required]
        [Display(Name="post something witty here...")]
        [MinLength(5, ErrorMessage = "Idea must be at least 5 characters in length")]
        public string Content {get;set;}

        public DateTime CreatedAt {get;set;}

        public Idea()
        {
            CreatedAt = DateTime.Now;
        }

        public User Creator {get;set;}

        public List<Like> Likes {get;set;}
    }
}