using PZProject.Data.Database.Entities.Group;
using System.Security;

namespace PZProject.Handlers.Utils
{
    public static class SecurityAssertions
    {
        public static void AssertThatIssuerIsAuthorizedToOperation(GroupEntity group, int userId)
        {
            if (group.CreatorId != userId)
                throw new SecurityException($"User with ID: {userId} does not have access to this operation.");
        }
    }
}