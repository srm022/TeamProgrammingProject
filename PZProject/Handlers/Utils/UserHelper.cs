using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PZProject.Handlers.Utils
{
    static class UserHelper
    {
        public static int GetUserId(this Controller controller)
        {
            return int.Parse(controller.User.Claims.First(i => i.Type == "UserId").Value);
        }
    }
}
