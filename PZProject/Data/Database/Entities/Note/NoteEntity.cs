using PZProject.Data.Database.Entities.Group;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PZProject.Data.Database.Entities.Note
{
    [Table("Note")]
    public class NoteEntity
    {
        [Key, Required]
        public int NoteId { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        public string AttachmentIdentity { get; set; }

        [Required]
        public GroupEntity Group { get; set; }
    }
}
