using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var adminRole = await _roleManager.FindByNameAsync("Admin");
        var admins = await _userManager.GetUsersInRoleAsync("Admin");

        var nonAdminUsers = users.Where(user => !admins.Contains(user)).ToList();

        return View(nonAdminUsers);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Pause(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            user.EmailConfirmed = !user.EmailConfirmed; // Toggle status
            await _userManager.UpdateAsync(user);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction(nameof(Index));
    }
}
