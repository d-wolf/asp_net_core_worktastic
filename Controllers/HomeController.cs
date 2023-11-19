using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Worktastic.Data;
using Worktastic.Models;

namespace Worktastic.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpGet]
    public IActionResult GetJobPosting(int id)
    {
        if (id == 0)
            return BadRequest();
        var posting = _context.JobPostings.SingleOrDefault(x => x.Id == id);
        if (posting == null)
            return NotFound();
        return Ok(posting);
    }


    public IActionResult GetJobPostingsPartial(string query)
    {
        List<JobPosting> jobPostings = new();
        if (string.IsNullOrWhiteSpace(query))
        {
            jobPostings = _context.JobPostings.ToList();
        }
        else
        {
            jobPostings = _context.JobPostings.Where(x => x.JobTitle.Contains(query)).ToList();
        }

        return PartialView("_JobPostingListPartial", jobPostings);
    }
}
