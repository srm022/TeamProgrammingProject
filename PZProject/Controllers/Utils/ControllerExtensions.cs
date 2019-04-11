using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace PZProject.Controllers.Utils
{
    static class ControllerExtensions
    {
        public static int GetUserId(this Controller controller)
        {
            return int.Parse(controller.User.Claims.SingleOrDefault()?.Value);
        }
    }
}
