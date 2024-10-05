using ManagingContent.Db;
using ManagingContent.Models;
using ManagingContent.VmModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagingContent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterStudent(RegisterVM registerVM, [FromForm] IFormFile paymentProof)
        {
            var paymentProofUrl = $"/uploads/{paymentProof.FileName}";

            var student = new Student
            {
                Name = registerVM.Name,
                Course = registerVM.Course,
                PaymentProof = paymentProofUrl,
                IsVerified = false
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }
    }
}
