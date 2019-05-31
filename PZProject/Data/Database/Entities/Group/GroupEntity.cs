using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PZProject.Data.Database.Entities.Note;

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

        [MaxLength(256)]
        public string Description { get; set; }

        public List<UserGroupEntity> UserGroups { get; set; }

        public List<NoteEntity> Notes { get; set; }
    }
}
