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
    public void OnGet()
    {
        
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
}