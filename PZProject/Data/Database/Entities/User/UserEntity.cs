using PZProject.Data.Database.Entities.Group;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZProject.Data.Database.Entities.User
{
    [Table("User")]
    public class UserEntity
    {
        [Key, Required]
        public int UserId { get; set; }

        [Required, MaxLength(128)]
        public string Email { get; set; }

        [Required, MaxLength(32)]
        public string FirstName { get; set; }

        [Required, MaxLength(32)]
        public string LastName { get; set; }

        [Required, MaxLength(1024)]
        public byte[] PasswordHash { get; set; }

        [Required, MaxLength(512)]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public RoleEntity Role { get; set; }

        public List<UserGroupEntity> UserGroups { get; set; }
    }
}