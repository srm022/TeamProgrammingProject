using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PZProject.Data.Database.Entities.Group;

namespace PZProject.Data.Database.Entities.GroupNote
{
    [Table("GroupNote")]
    public class GroupNoteEntity
    {
        [Key, Required]
        public int NoteId { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public GroupEntity Group { get; set; }
    }
}
