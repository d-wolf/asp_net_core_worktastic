using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using worktastic.Models;

namespace worktastic.Controllers;

public class JobPostingController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateEditJobPosting(int id)
    {
        return View();
    }

    public IActionResult CreateEditJob(JobPosting jobPosting)
    {
        Console.WriteLine("");
        // TODO: write posting to db
        return RedirectToAction("Index");
    }
}
