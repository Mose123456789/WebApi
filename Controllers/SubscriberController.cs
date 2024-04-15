using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscriberController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    #region CREATE
    [HttpPost]
    public async Task<IActionResult> Create(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == email))
            {
                try
                {
                    var subscriberEntity = new SubscriberEntity { Email = email };
                    _context.Subscribers.Add(subscriberEntity);
                    await _context.SaveChangesAsync();

                    return Created("", null);
                }

                catch
                {
                    return Problem("Unable to create subscription.");
                }
            }

            return Conflict("Your email address is already subscribed.");
        }

        return BadRequest("You must enter a valid email address.");
    }
    #endregion

    #region READ
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _context.Subscribers.ToListAsync();
        if (subscribers.Any())
        {
            return Ok(subscribers);
        }

        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var subscribers = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscribers == null)
        {
            return Ok(subscribers);
        }

        return NotFound();
    }
    #endregion

    #region UPDATE
    [HttpPut]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        var subscribers = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscribers == null)
        {
            subscribers.Email = email;
            _context.Subscribers.Update(subscribers);
            await _context.SaveChangesAsync();
            return Ok(subscribers);
        }

        return NotFound();
    }
    #endregion

    #region DELETE
    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        var subscribers = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscribers != null)
        {
            _context.Subscribers.Remove(subscribers);
            await _context.SaveChangesAsync();
            return Ok();
        }

        return NotFound();
    }
    #endregion
}