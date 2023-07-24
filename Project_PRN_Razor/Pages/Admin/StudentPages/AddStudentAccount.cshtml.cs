using Domain.Constants;
using Domain.Models;
using Infrastructure.IRepositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;

namespace Project_PRN_Razor.Pages.Admin.StudentPages;

public class AddStudentAccount : PageModel
{
    private IAccountRepository _accountRepository;
    public Account AccountCreate { get; set; }

    public AddStudentAccount(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost(Account accountCreate)
    {
        accountCreate.Role = Common.Roles.Student;
        accountCreate.Status = 1;
        accountCreate.Password = "12345678";
        _accountRepository.AddAccount(accountCreate);
        var bodyMail = $"Hello {accountCreate.Email}, your student account has been created. /n Your default password is 12345678";
        SendEmail(accountCreate.Email, "Account Created", bodyMail);
        return RedirectToPage("Index");
    }

    private void SendEmail(string recipientEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Trinh Dinh Khai", "khaitdhe160797@fpt.edu.vn"));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;

        message.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, true);
            client.Authenticate("khaitdhe160797@fpt.edu.vn", "khai1209");

            client.Send(message);
            client.Disconnect(true);
        }
    }
}