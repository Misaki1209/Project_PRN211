using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Project_PRN_Razor.Pages.Admin;

[Authorize(Roles = "Admin")]
public class Index : PageModel
{
    public void OnGet()
    {
        
    }
}