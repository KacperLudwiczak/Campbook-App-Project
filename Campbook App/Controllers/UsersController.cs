using Campbook_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // GET: /Users/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Users/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            TempData["success"] = "Welcome to Yelp Camp!";
            return RedirectToAction("Index", "Campgrounds");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // GET: /Users/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Users/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            TempData["success"] = "Welcome back!";
            return Redirect(returnUrl ?? Url.Action("Index", "Campgrounds")!);
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }

    // POST: /Users/Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["success"] = "Goodbye!";
        return RedirectToAction("Index", "Campgrounds");
    }
}