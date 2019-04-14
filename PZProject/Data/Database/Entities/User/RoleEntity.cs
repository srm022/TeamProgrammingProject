using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZProject.Data.Database.Entities.User
{
    [Table("Role")]
    public class RoleEntity
    {
        [Key, Required]
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}