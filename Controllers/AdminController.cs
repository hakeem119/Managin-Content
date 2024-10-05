using ManagingContent.Db;
using ManagingContent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ManagingContent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Teacher> _userManager;

        public AdminController(AppDbContext context,UserManager<Teacher>  userManager)
        {
            _context = context;
            _userManager= userManager;

        }

        public async Task<IActionResult> AssignTeacher([FromQuery] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!await _userManager.IsInRoleAsync(user, "teacher"))
            {
                var result = await _userManager.AddToRoleAsync(user, "teacher");
                if (!result.Succeeded)
                {
                    return BadRequest("Failed to assign role");
                }
            }
            return Ok("User successfully assigned to teacher role");
        }


        [HttpPost("approve/{studentId}")]
        public async Task<IActionResult> ApproveStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                return NotFound();

            student.IsVerified = true;
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPost("reject/{studentId}")]
        public async Task<IActionResult> RejectStudent(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null)
                return NotFound();

            student.IsVerified = false;
            await _context.SaveChangesAsync();
            return Ok(student);
        }
    }
}
