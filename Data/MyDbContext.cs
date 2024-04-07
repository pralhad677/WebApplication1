using System.Collections.Generic;
using WebApplication1.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class MyDbContext :DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
