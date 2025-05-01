using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dto;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context) {
            _context = context;
            }

        [HttpPost("CreateTasks")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDto dto) {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound("Assigned user not found.");

            var task = new TaskItem {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId,
                Status = Enum.TaskStatus.InProgress
                };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto { Status = Enum.Resultstatus.SUCCESS, Message = "Added" });
            }

        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(int id) {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
                return NotFound();

            return Ok(new { status = Enum.Resultstatus.SUCCESS, Data = task });
            }

        [HttpGet("GetTaskForUser/{userId}")]
        public async Task<IActionResult> GetTasksForUser(int userId) {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(new CustomResponseDto{ Status = Enum.Resultstatus.SUCCESS, Data = tasks });
            }
        }
    }
