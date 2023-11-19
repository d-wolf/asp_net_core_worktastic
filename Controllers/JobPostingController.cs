using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using worktastic.Data;
using worktastic.Models;

namespace worktastic.Controllers;

[Authorize]
public class JobPostingController : Controller
{

    private readonly ApplicationDbContext _context;

    public JobPostingController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
        {
            var jobPostingsFromDb = _context.JobPostings.ToList();
            return View(jobPostingsFromDb);
        }
        else
        {
            var jobPostingsFromDb = _context.JobPostings.Where(x => x.OwnerUserName == User.Identity!.Name).ToList();
            return View(jobPostingsFromDb);
        }

    }

    public IActionResult CreateEditJobPosting(int id)
    {
        if (id != 0)
        {
            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);

            if (jobPostingFromDb == null)
            {
                return NotFound();
            }

            if (jobPostingFromDb!.OwnerUserName != User.Identity?.Name && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            return View(jobPostingFromDb);
        }

        return View();
    }

    [HttpPost]
    public IActionResult DeleteJobPostingById(int id)
    {
        if (id != 0)
        {
            var jobPostingFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);
            if (jobPostingFromDb != null)
            {
                _context.JobPostings.Remove(jobPostingFromDb);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        return NotFound();
    }


    public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
    {
        jobPosting.StartDate = DateTime.SpecifyKind(jobPosting.StartDate, DateTimeKind.Utc);
        jobPosting.OwnerUserName = User.Identity!.Name!;

        if (file != null)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var bytes = ms.ToArray();
            jobPosting.CompanyImage = bytes;
        }
        else
        {
            jobPosting.CompanyImage = Array.Empty<byte>();
        }

        // default int is 0
        if (jobPosting.Id == 0)
        {
            _context.JobPostings.Add(jobPosting);
        }
        else
        {
            var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == jobPosting.Id);

            if (jobFromDb == null)
            {
                return NotFound();
            }

            jobFromDb.CompanyImage = jobPosting.CompanyImage;
            jobFromDb.CompanyName = jobPosting.CompanyName;
            jobFromDb.Salary = jobPosting.Salary;
            jobFromDb.ContactMail = jobPosting.ContactMail;
            jobFromDb.ContactPhone = jobPosting.ContactPhone;
            jobFromDb.ContactWebsite = jobPosting.ContactWebsite;
            jobFromDb.JobTitle = jobPosting.JobTitle;
            jobFromDb.JobDescription = jobPosting.JobDescription;
            jobFromDb.JobLocation = jobPosting.JobLocation;
            jobFromDb.StartDate = jobPosting.StartDate;
            // jobFromDb.OwnerUserName = jobPosting.OwnerUserName;
        }


        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
