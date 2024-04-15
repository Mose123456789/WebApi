using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CoursesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _context.Courses.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        if (course != null)
        {
            return Ok(course);
        }

        return NotFound();
    }
}