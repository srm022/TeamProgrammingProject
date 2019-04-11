using System;

namespace PZProject.Data.Database.Entities
{
    public static class EntityAssertExtensions
    {
        public static void AssertThatExists<T>(this T entity)
        {
            if (entity == null)
                throw new Exception($"Could not find entity {nameof(entity)}");
        }
    }
}