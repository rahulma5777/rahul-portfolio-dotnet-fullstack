using NetFullStack.API.Models;

namespace NetFullStack.API.Data
{
    /// <summary>
    /// Seeds the database with initial data.  This is run at application
    /// startup to ensure there is always some data available for demo
    /// purposes.  In a production app you would use EF Core migrations
    /// instead.
    /// </summary>
    public static class DbSeeder
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // If there are any users already, assume the database has been seeded.
            if (context.Users.Any()) return;

            var users = new List<User>
            {
                new User { Name = "Alice Johnson", Email = "alice@example.com" },
                new User { Name = "Bob Smith", Email = "bob@example.com" },
                new User { Name = "Charlie Brown", Email = "charlie@example.com" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var tasks = new List<TaskItem>
            {
                new TaskItem { Title = "Set up the project structure", UserId = users[0].Id, IsCompleted = true },
                new TaskItem { Title = "Implement RESTful API", UserId = users[0].Id },
                new TaskItem { Title = "Design database schema", UserId = users[1].Id },
                new TaskItem { Title = "Write README documentation", UserId = users[2].Id, IsCompleted = true }
            };

            context.TaskItems.AddRange(tasks);
            context.SaveChanges();
        }
    }
}