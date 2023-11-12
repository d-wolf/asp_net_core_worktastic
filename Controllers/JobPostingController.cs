using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using worktastic.Data;
using worktastic.Models;

namespace worktastic.Controllers;

public class JobPostingController : Controller
{

    private readonly ApplicationDbContext _context;

    public JobPostingController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateEditJobPosting(int id)
    {
        var jobPostingsFromDb = _context.JobPostings.Where(x => x.OwnerUserName == User.Identity!.Name).ToList();
        return View(jobPostingsFromDb);
    }

    public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
    {

        jobPosting.OwnerUserName = User.Identity!.Name!;

        if (file != null)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var bytes = ms.ToArray();
            jobPosting.CompanyImage = bytes;
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
            jobFromDb.OwnerUserName = jobPosting.OwnerUserName;
        }


        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
