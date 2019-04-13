using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace PZProject.Controllers.Utils
{
    static class ControllerExtensions
    {
        public static int GetUserId(this Controller controller)
        {
            return int.Parse(controller.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Name)?.Value);
        }
    }
}
