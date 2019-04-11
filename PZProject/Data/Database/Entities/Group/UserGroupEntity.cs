using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZProject.Data.Database.Entities.Group
{
    [Table("UserGroup")]
    public class UserGroupEntity
    {
       [Key, Required]
       public int UserGroupId { get; set; }

       [Required]
       public int GroupId { get; set; }
    
       [Required]
       public int UserId { get; set; }

    }
}
