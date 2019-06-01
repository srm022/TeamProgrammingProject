using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Note;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Note.Attachment
{
    public interface IAttachmentHandler
    {
        CloudBlockBlob GetAttachmentForNote(int userId, int noteId);
        void CreateAttachment(IFormFile file, int userId, int noteId);
        void DeleteAttachment(int userId, int noteId);
    }

    public class AttachmentHandler : IAttachmentHandler
    {
        private readonly INoteRepository _noteRepository;
        private readonly ICloudBlobService _cloudBlobService;

        public AttachmentHandler(ICloudBlobService cloudBlobService,
            INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
            _cloudBlobService = cloudBlobService;
        }

        public CloudBlockBlob GetAttachmentForNote(int userId, int noteId)
        {
            var note = GetNote(noteId);
            SecurityAssertions.AssertThatUserBelongsToGroup(note.Group, userId);

            var file = GetBlobFromStorage(note.AttachmentIdentity);

            return file;
        }

        public void CreateAttachment(IFormFile file, int userId, int noteId)
        {
            var note = GetNote(noteId);
            AssertThatUserIsNoteCreator(note, userId);

            SaveToStorage(file);
            UpdateNote(note, file.FileName);
        }

        public async void DeleteAttachment(int userId, int noteId)
        {
            var note = GetNote(noteId);
            AssertThatUserIsNoteCreator(note, userId);

            var blob = GetBlobFromStorage(note.AttachmentIdentity);

            DeleteAttachmentForNote(note);
            await blob.DeleteIfExistsAsync();
        }

        private void DeleteAttachmentForNote(NoteEntity note)
        {
            _noteRepository.DeleteAttachmentReference(note);
        }

        private void UpdateNote(NoteEntity note, string fileName)
        {
            _noteRepository.CreateAttachmentReference(note, fileName);
        }

        private CloudBlockBlob GetBlobFromStorage(string attachment)
        {
            return _cloudBlobService.GetBlobForFile(attachment);
        }

        private void SaveToStorage(IFormFile file)
        {
            _cloudBlobService.CreateBlobForFile(file);
        }

        private NoteEntity GetNote(int noteId)
        {
            return _noteRepository.GetNoteById(noteId);
        }

        private void AssertThatUserIsNoteCreator(NoteEntity note, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToOperation(note, userId);
        }
    }
}