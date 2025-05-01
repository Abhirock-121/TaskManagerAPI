using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Dto;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class TaskCommentsController : ControllerBase {
        private readonly AppDbContext _context;

        public TaskCommentsController(AppDbContext context) {
            _context = context;
            }

        [HttpGet("GetCommentForTask/{taskId}")]
        public async Task<IActionResult> GetCommentsForTask(int taskId) {
            var comments = await _context.TaskComments
                .Where(c => c.TaskItemId == taskId)
                .Include(c => c.TaskItem)
                .ToListAsync();

            return Ok(new CustomResponseDto{ Status = Enum.Resultstatus.SUCCESS, Data = comments });
            }

        [HttpGet("GetCommentById/{id}")]
        public async Task<IActionResult> GetComment(int id) {
            var comment = await _context.TaskComments
                .Include(x => x.TaskItem)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
                return NotFound();

            return Ok(new CustomResponseDto{ Status = Enum.Resultstatus.SUCCESS, Data = comment });
            }

        [HttpPost("AddCommentForTask")]
        [Authorize]
        public async Task<IActionResult> AddComment(TaskCommentDto comment) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskExists = await _context.Tasks.AnyAsync(t => t.Id == comment.TaskItemId);
            var userExists = await _context.Users.AnyAsync(u => u.Id == comment.UserId);

            if (!taskExists || !userExists)
                return BadRequest("Invalid task or user.");
            var commenttoadd = new TaskComment {
                CommentText = comment.CommentText,
                TaskItemId = comment.TaskItemId,
                UserId = comment.UserId,
                TaskItem = await _context.Tasks.FindAsync(comment.TaskItemId),
                User = await _context.Users.FindAsync(comment.UserId),
                };
            _context.TaskComments.Add(commenttoadd);
            await _context.SaveChangesAsync();

            return Ok(new ResponseDto { Status = Enum.Resultstatus.SUCCESS, Message = "Added" });
            }
        }
    }

