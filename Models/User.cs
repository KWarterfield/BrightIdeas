using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Must only contain letters and spaces!")]
        [Display(Name = "Name")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Alias is required!")]
        [MinLength(2, ErrorMessage = "Must be at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Must only contain numbers and letters! ")]
        [Display(Name = "Alias")]
        public string Alias {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail Address")]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters!")]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$")]
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public User()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Compare {get; set;}

        public List<Idea> CreatedIdeas {get; set;}
        public List<Like> CreatedLikes {get; set;}
    }

    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}