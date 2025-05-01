using TaskManagerAPI.Enum;
using TaskManagerAPI.Models;
using TaskStatus = TaskManagerAPI.Enum.TaskStatus;

namespace TaskManagerAPI.Data {
    public static class DbSeeder {
        public static void Seed(IApplicationBuilder app) {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!db.Users.Any()) {
                db.Users.AddRange(
                    new User { Username = "admin", Password = "admin", Role = UserRole.Admin },
                    new User { Username = "user1", Password = "user1", Role = UserRole.User }
                );
                db.SaveChanges();
                }
            if (!db.Tasks.Any()) {
                db.Tasks.AddRange(
                        new TaskItem {
                                Title = "Initial Task",
                                Description = "This is a seeded task",
                                Status = TaskStatus.Pending,
                                UserId = 1,
                                User = new User { Username = "admin", Password = "admin", Role = UserRole.Admin },
                                Comments = new List<TaskComment> {
                                    new TaskComment
                                        {
                                            Id = 1,
                                            TaskItemId = 1,
                                            UserId = 2,
                                            CommentText = "Looks good!",
                                        }
                                    }
                                
                            }
                    );
                }
            }
            
           
        }

    }
    