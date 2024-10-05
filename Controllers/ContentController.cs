using ManagingContent.Db;
using ManagingContent.Models;
using ManagingContent.VmModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagingContent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("search")]
        public IActionResult SearchContent(string title, int page = 1, int pageSize = 10)
        {
            var contents = _context.Contents
                .Where(c => c.Title.Contains(title))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Ok(contents);
        }

        [Authorize(Roles = "teacher")]
        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadFile(IFormFile  file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine("root/uploads", file.FileName);

            var filePathReturned = Path.Combine("Uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                FileSize = file.Length,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow
            };
            
            _context.UploadedFiles.Add(uploadedFile);
            _context.SaveChanges();
            return Ok(uploadedFile);

            return Ok(new {message="File Uploaded successfully ",filePath= filePathReturned });
        }



        [HttpPut("publish/{id}")]
        public async Task<IActionResult> PublishContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
                return NotFound();

            content.IsPublished = true;
            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
                return NotFound();

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return Ok("Content deleted.");
        }
    }
}
