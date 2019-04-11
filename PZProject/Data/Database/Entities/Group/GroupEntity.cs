using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZProject.Data.Database.Entities.Group
{
    [Table("Group")]
    public class GroupEntity
    {
        [Key, Required]
        public int GroupId { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public int CreatorId { get; set; }

        public List<UserGroupEntity> Users { get; set; }
    }
}
