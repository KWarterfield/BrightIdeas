using Microsoft.EntityFrameworkCore;
 
namespace BrightIdeas.Models
{
    public class Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Idea> Ideas {get; set;}
        public DbSet<Like> Likes {get; set;}
    }
}