using TaskManagerAPI.Enum;

namespace TaskManagerAPI.Dto {
    public class TaskDto {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int UserId { get; set; }
        }
    }
