using Infrastructure.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Domain.Constants;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Project_PRN_Razor.Pages;

[AllowAnonymous]
public class LoginPage : PageModel
{
    private IAccountRepository _accountRepository;

    public LoginPage(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public LoginDto LoginDto { get; set; }
    public bool IsLoginFailed { get; set; } = false;
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (userRole == Common.Roles.Admin)
            {
                // if role is Admin, redirect to page Admin/Index
                return RedirectToPage("/Admin/Index");
            }
            else if (userRole == Common.Roles.Teacher)
            {
                // if role is teacher, redirect to page Teacher/Index
                return RedirectToPage("/Teacher/Index");
            }
            else if(userRole == Common.Roles.Student)
            {
                // if role is student, redirect to page Student/Index
                return RedirectToPage("/Student/Index");
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPost(LoginDto loginDto)
    {
        var loginAccount = _accountRepository.GetAccountLogin(loginDto);
        if (loginAccount != null)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, loginDto.Email),
                new Claim(ClaimTypes.Role, loginAccount.Role.Trim()),
                new Claim(ClaimTypes.Sid, loginAccount.AccountId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = loginDto.RememberMe
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
            if (loginAccount.Role.Trim() == Common.Roles.Admin)
                return RedirectToPage("/Admin/Index");
            else if (loginAccount.Role.Trim() == Common.Roles.Teacher)
                return RedirectToPage("/Teacher/Index");
            else
                return RedirectToPage("/Student/Index");
        }

        IsLoginFailed = true;
        return RedirectToPage("LoginPage");
    }

    public async Task<IActionResult> OnGetLogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToPage("LoginPage");
    }
}