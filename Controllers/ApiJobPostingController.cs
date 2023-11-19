using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using worktastic.Data;
using worktastic.Models;

namespace worktastic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiJobPostingController : ControllerBase {
 
    [HttpGet("")] //Matches GET api/admin <-- Would also work with [HttpGet]
    public IActionResult Get() {
        return Ok();
    }

    [HttpGet("{id}")] //Matches GET api/admin/5
    public IActionResult Get(int id) {
        return Ok("value");
    }    

    //...other code removed for brevity
}