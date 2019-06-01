using System;
namespace PZProject.Data.Requests.GroupRequests
{
    public class EditGroupRequest
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
    }
}
