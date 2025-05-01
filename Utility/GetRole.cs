using TaskManagerAPI.Enum;

namespace TaskManagerAPI.Utility {
    public class Getrole {
        public static UserRole GetRole(string str) {
            return str.ToLower() switch {
                "admin" => UserRole.Admin,
                "user" => UserRole.User,
                _ => UserRole.User
                };
            }
        }
    }
