using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{
    public class LoginVM
    {
       public User newUser {get; set;}
       public LoginUser existing {get; set;}
    }
}