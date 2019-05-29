using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{
    public class UserVM
    {
        public User User {get;set;}

        public List<Like> allLikes {get;set;}
    }
}