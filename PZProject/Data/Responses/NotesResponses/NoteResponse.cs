using System;
namespace PZProject.Data.Responses.NotesResponses
{
    public class NoteResponse
    {
        public int id { get; set; }
        public int creatorId { get; set; }
        public int groupId { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public NoteResponse(int id, int creatorId, int groupId, string name, string description)
        {
            this.id = id;
            this.creatorId = creatorId;
            this.groupId = groupId;
            this.name = name;
            this.description = description;
        }
    }
}
