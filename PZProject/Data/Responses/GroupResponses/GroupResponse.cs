using System;
using System.Collections.Generic;
using PZProject.Data.Database.Entities.Group;

namespace PZProject.Data.Responses.GroupResponses
{
    public class GroupResponse
    {
        public int id { get; set; }
        public int creatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<UserGroupEntity> userGroups { get; set; }

        public GroupResponse(int id, int creatorId, string name, string description, List<UserGroupEntity> userGroups)
        {
            this.id = id;
            this.creatorId = creatorId;
            this.name = name;
            this.description = description;
            this.userGroups = userGroups;
        }
    }
}
