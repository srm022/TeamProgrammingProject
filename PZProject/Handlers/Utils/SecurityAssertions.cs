using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using System.Linq;
using System.Security;

namespace PZProject.Handlers.Utils
{
    public static class SecurityAssertions
    {
        public static void AssertThatIssuerIsAuthorizedToManageGroup(GroupEntity group, int userId)
        {
            if (group.CreatorId != userId)
                throw new SecurityException($"User with ID: {userId} does not have access to this operation.");
        }

        public static void AssertThatIssuerIsAuthorizedToEditNote(NoteEntity note, int userId)
        {
            if (note.CreatorId != userId)
                throw new SecurityException($"User with ID: {userId} does not have access to this operation.");
        }


        public static void AssertThatIssuerIsAuthorizedToDeleteNote(NoteEntity note, GroupEntity group, int userId)
        {
            if (note.CreatorId != userId && group.CreatorId != userId)
                throw new SecurityException($"User with ID: {userId} does not have access to this operation.");
        }

        public static void AssertThatUserBelongsToGroup(GroupEntity group, int userId)
        {
            var userGroups = group.UserGroups;

            if (!userGroups.Any(u => u.UserId == userId))
                throw new SecurityException($"User with ID: {userId} does not have access to this operation.");
        }    }
}