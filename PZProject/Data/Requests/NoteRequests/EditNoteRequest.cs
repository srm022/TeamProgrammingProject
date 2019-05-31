using System;
namespace PZProject.Data.Requests.NoteRequests
{
    public class EditNoteRequest
    {
        public int NoteId { get; set; }
        public string NoteName { get; set; }
        public string NoteDescription { get; set; }
    }
}
