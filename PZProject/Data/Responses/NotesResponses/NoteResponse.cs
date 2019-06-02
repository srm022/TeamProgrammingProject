using PZProject.Data.Database.Entities.Note;

namespace PZProject.Data.Responses.NotesResponses
{
    public class NoteResponse
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AttachmentIdentity { get; set; }

        public NoteResponse(NoteEntity noteEntity)
        {
            Id = noteEntity.NoteId;
            CreatorId = noteEntity.CreatorId;
            GroupId = noteEntity.Group.GroupId;
            Name = noteEntity.Name;
            Description = noteEntity.Description;
            AttachmentIdentity = noteEntity.AttachmentIdentity;
        }
    }
}
