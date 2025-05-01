using System.Text.Json.Serialization;
using TaskManagerAPI.Enum;
using TaskStatus = TaskManagerAPI.Enum.TaskStatus;

namespace TaskManagerAPI.Models {
    public class TaskItem {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int UserId { get; set; }
            
            [JsonIgnore]
            public User? User { get; set; }
            public TaskStatus Status { get; set; }

            [JsonIgnore]
            public ICollection<TaskComment>? Comments { get; set; }
        }
    }
