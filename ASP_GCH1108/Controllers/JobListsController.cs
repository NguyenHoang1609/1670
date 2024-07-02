using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ASP_GCH1108.Data;
using ASP_GCH1108.Models;
using Microsoft.AspNetCore.Authorization;

public class JobListsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public JobListsController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Index()
    {
        string UserName = _userManager.GetUserName(User);
        var jobLists = await _context.JobList.Where(j => j.UserName.Equals(UserName)).ToListAsync();
        return View(jobLists);
    }

    [Authorize(Roles = "JobSeeker")]
    public async Task<IActionResult> AvailableJobs()
    {
        var jobLists = await _context.JobList.Where(j => j.status).ToListAsync();
        return View(jobLists);
    }

    [Authorize(Roles = "Employer")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Employer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(JobList jobList)
    {
        if (ModelState.IsValid)
        {
            jobList.UserId = _userManager.GetUserId(User);
            jobList.UserName = _userManager.GetUserName(User);
            jobList.status = false;
            _context.Add(jobList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(jobList);
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobList = await _context.JobList.FindAsync(id);
        if (jobList == null)
        {
            return NotFound();
        }
        return View(jobList);
    }

    [Authorize(Roles = "Employer")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("JobId,JobTitle,JobDescription,RequiredQualifications,ApplicationDeadline")] JobList jobList)
    {
        if (id != jobList.JobId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                var existingJob = await _context.JobList.FindAsync(id);
                if (existingJob == null)
                {
                    return NotFound();
                }

                existingJob.JobTitle = jobList.JobTitle;
                existingJob.JobDescription = jobList.JobDescription;
                existingJob.RequiredProfiles = jobList.RequiredProfiles;
                existingJob.ApplicationDeadline = jobList.ApplicationDeadline;

                _context.Update(existingJob);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobListExists(jobList.JobId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(jobList);
    }

    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobList = await _context.JobList
            .FirstOrDefaultAsync(m => m.JobId == id);
        if (jobList == null)
        {
            return NotFound();
        }

        return View(jobList);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Employer")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var jobList = await _context.JobList.FindAsync(id);
        _context.JobList.Remove(jobList);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Allow(int id)
    {
        var jobList = await _context.JobList.FindAsync(id);
        if (jobList == null)
        {
            return NotFound();
        }

        jobList.status = true;
        _context.Update(jobList);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(AdminManager));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DetailsAdmin(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var jobList = await _context.JobList
            .FirstOrDefaultAsync(m => m.JobId == id);
        if (jobList == null)
        {
            return NotFound();
        }

        return View(jobList);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminManager()
    {
        return View(await _context.JobList.Where(j => j.status == false).ToListAsync());
    }

    [HttpPost, ActionName("AdminDeleteConfirmed")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminDeleteConfirmed(int id)
    {
        var jobList = await _context.JobList.FindAsync(id);
        _context.JobList.Remove(jobList);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(AdminManager));
    }

    private bool JobListExists(int id)
    {
        return _context.JobList.Any(e => e.JobId == id);
    }
}
