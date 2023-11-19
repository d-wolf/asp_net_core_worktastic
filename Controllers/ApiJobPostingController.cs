using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using worktastic.Data;
using worktastic.Models;

namespace worktastic.Controllers;

[Route("api/jobposting")]
[ApiController]
public class ApiJobPostingController : ControllerBase
{

    private readonly ApplicationDbContext _context;

    public ApiJobPostingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var all = _context.JobPostings.ToList();
        return Ok(all);
    }

    [HttpGet("GetById")]
    public IActionResult Get(int id)
    {
        var posting = _context.JobPostings.SingleOrDefault(x => x.Id == id);
        if (posting == null) return NotFound();
        return Ok(posting);
    }

    // [HttpGet("{id}")]
    // public IActionResult Get(int id) {
    //     return Ok("value");
    // }
}