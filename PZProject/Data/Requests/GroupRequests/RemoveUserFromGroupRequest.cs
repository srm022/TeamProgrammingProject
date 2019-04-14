namespace PZProject.Data.Requests.GroupRequests
{
    public class RemoveUserFromGroupRequest
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
    }
}