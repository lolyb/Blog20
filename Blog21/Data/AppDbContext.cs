using Blog21.Models;
using Microsoft.EntityFrameworkCore;
//using Blog21.Models.Comments;
using Blog21.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog21.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        public DbSet<Post> Posts {get; set;}
    }
}
