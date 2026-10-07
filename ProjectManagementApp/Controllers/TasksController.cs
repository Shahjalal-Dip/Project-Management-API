using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Dtos;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        [HttpPost("assign")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AssignTask(TaskDto dto, string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return NotFound("User not found");

            var task = _mapper.Map<TaskItem>(dto);
            task.ProjectId = dto.Id;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok("Task assigned successfully");
        }

        [HttpGet("mine")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyTasks()
        {
            var username = User.Identity?.Name;
            var tasks = await _context.Tasks
                .Include(t => t.Project)
                .Where(t => t.Project.Tasks.Any())
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<TaskDto>>(tasks));
        }

        [HttpPut("{id}/complete")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.IsCompleted = true;
            await _context.SaveChangesAsync();

            return Ok("Task marked as completed");
        }

    }
}
