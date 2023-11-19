using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public IActionResult GetById(int id)
    {
        var posting = _context.JobPostings.SingleOrDefault(x => x.Id == id);
        if (posting == null) return NotFound();
        return Ok(posting);
    }

    [HttpPost("Create")]
    public IActionResult Create(JobPosting jobPosting)
    {
        if (jobPosting.Id != 0)
        {
            return BadRequest();
        }

        _context.JobPostings.Add(jobPosting);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var posting = _context.JobPostings.SingleOrDefault(x => x.Id == id);
        if (posting == null) return NotFound();
        _context.JobPostings.Remove(posting);
        _context.SaveChanges();
        return Ok();
    }


    [HttpPut("Update")]
    public IActionResult Update(JobPosting jobPosting)
    {
        // https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
        var posting = _context.JobPostings.AsNoTracking().SingleOrDefault(x => x.Id == jobPosting.Id);
        if (posting == null) return BadRequest();
        _context.JobPostings.Update(jobPosting);
        _context.SaveChanges();
        return Ok();
    }
}