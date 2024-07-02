using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_GCH1108.Data;
using ASP_GCH1108.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ASP_GCH1108.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobApplicationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var userApplications = _context.JobApplication
                                          .Include(j => j.JobList)
                                          //.Include(j => j.Qualification)
                                          .Include(j => j.User)
                                          .Where(j => j.UserId == userId);
            return View(await userApplications.ToListAsync());
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> EmployerView()
        {
            var userId = _userManager.GetUserId(User);

            // Fetch JobIds that belong to the current Employer
            var employerJobIds = await _context.JobList
                                               .Where(j => j.UserId == userId)
                                               .Select(j => j.JobId)
                                               .ToListAsync();

            // Fetch JobApplications that have JobIds in the employerJobIds list
            var employerApplications = await _context.JobApplication
                                                     .Include(j => j.JobList)
                                                     //.Include(j => j.Qualification)
                                                     .Include(j => j.User)
                                                     .Where(j => employerJobIds.Contains(j.JobId))
                                                     .ToListAsync();

            return View(employerApplications);
        }


        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication
                                               .Include(j => j.JobList)
                                               //.Include(j => j.Qualification)
                                               .Include(j => j.User)
                                               .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DetailEmployer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication
                                               .Include(j => j.JobList)
                                               //.Include(j => j.Qualification)
                                               .Include(j => j.User)
                                               .FirstOrDefaultAsync(m => m.ApplicationId == id);

            if (jobApplication == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var employerJobIds = await _context.JobList
                                               .Where(j => j.UserId == userId)
                                               .Select(j => j.JobId)
                                               .ToListAsync();

            if (!employerJobIds.Contains(jobApplication.JobId))
            {
                return Forbid();
            }

            return View(jobApplication);
        }


        [Authorize(Roles = "JobSeeker")]
        public IActionResult Create(int jobId)
        {
            ViewData["JobId"] = jobId;
            ViewData["ProfileId"] = new SelectList(_context.Profile, "ProfileId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Create(JobApplication jobApplication, int jobId)
        {
            if (ModelState.IsValid)
            {
                jobApplication.Status = false;
                jobApplication.JobId = jobId;
                jobApplication.UserId = _userManager.GetUserId(User);

                if (jobApplication.Picture != null)
                {
                    jobApplication.UrlImage = UploadedFile(jobApplication);
                }

                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = jobId;
            //ViewData["QualificationId"] = new SelectList(_context.Qualification, "QualificationId", "Name", jobApplication.QualificationId);
            return View(jobApplication);
        }
        private string UploadedFile(JobApplication model)
        {
            string uniqueFileName = "";
            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["JobList"] = jobApplication.JobList;
            ViewData["JobId"] = jobApplication.JobId;
            //ViewData["QualificationId"] = new SelectList(_context.Profile, "QualificationId", "Name", jobApplication.QualificationId);
            return View(jobApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Edit(int id,JobApplication jobApplication)
        {
            if (id != jobApplication.ApplicationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (jobApplication.Picture != null)
                    {
                        jobApplication.UrlImage = UploadedFile(jobApplication);
                    }
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.ApplicationId))
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
            //ViewData["QualificationId"] = new SelectList(_context.Qualification, "QualificationId", "DegreeType", jobApplication.QualificationId);
            return View(jobApplication);
        }

        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication
                                               .Include(j => j.JobList)
                                               //.Include(j => j.Qualification)
                                               .Include(j => j.User)
                                               .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplication = await _context.JobApplication.FindAsync(id);
            _context.JobApplication.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteEP(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication
                                               .Include(j => j.JobList)
                                               //.Include(j => j.Qualification)
                                               .Include(j => j.User)
                                               .FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        [HttpPost, ActionName("DeleteEP")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> DeleteConfirmedEP(int id)
        {
            var jobApplication = await _context.JobApplication.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            _context.JobApplication.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EmployerView));
        }


        // POST: JobApplications/Allow/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> Allow(int id)
        {
            var jobApplication = await _context.JobApplication.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            jobApplication.Status = true;
            _context.Update(jobApplication);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(EmployerView));
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplication.Any(e => e.ApplicationId == id);
        }
    }
}
