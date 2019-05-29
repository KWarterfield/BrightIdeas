using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{
    public class MainVM
    {
        public User User {get;set;}

        public Idea Idea {get;set;}

        public List<Idea> allIdeas {get;set;}
    }
}