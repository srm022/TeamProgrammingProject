using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace PZProject.Handlers.Utils
{
    static class ControllerExtensions
    {
        public static int GetUserId(this Controller controller)
        {
            return int.Parse(controller.User.Claims.FirstOrDefault().Value);
        }
    }
}
