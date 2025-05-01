using System.Text.Json.Serialization;
using TaskManagerAPI.Enum;

namespace TaskManagerAPI.Models {
    public class User {
            public int Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public UserRole Role { get; set; }
            public ICollection<TaskItem>? Tasks { get; set; }
            [JsonIgnore]
            public ICollection<TaskComment>? Comments { get; set; }
        }
    }
