using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Database.Entities.Group;

namespace PZProject.Data.Database
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
    }
}