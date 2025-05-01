using System.Text.Json.Serialization;

namespace TaskManagerAPI.Models {
    public class TaskComment {
            public int Id { get; set; }
            public string? CommentText { get; set; }
            public int TaskItemId { get; set; }
            public TaskItem? TaskItem { get; set; }
            
            public int UserId { get; set; }
            [JsonIgnore]
            public User? User { get; set; }
        }
    }
