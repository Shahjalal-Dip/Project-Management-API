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
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateProject(ProjectDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ProjectDto>(project));
        }

        // Any logged-in user can view projects
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects.Include(p => p.Tasks).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
        }
    }
}
