using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Database.Entities.User;

namespace PZProject.Data.Database
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<UserGroupEntity> UserGroups { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
    }
}