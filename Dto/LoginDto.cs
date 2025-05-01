using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Dto {
    public class LoginDto {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
