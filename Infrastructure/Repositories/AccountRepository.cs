using System.Security.Cryptography;
using System.Text;
using Infrastructure.IRepositories;
using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private ProjectPrn221Context _context;

    public AccountRepository(ProjectPrn221Context context)
    {
        _context = context;
    }
    public IEnumerable<Account> GetAllAccount()
    {
        try
        {
            return _context.Accounts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Account? GetAccountById(int id)
    {
        try
        {
            return _context.Accounts.FirstOrDefault(x => x.AccountId == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public int GetNextId()
    {
        try
        {
            return _context.Accounts.OrderByDescending(x => x.AccountId).First().AccountId + 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void AddAccount(Account account)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateAccount(Account account)
    {
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteAccount(Account account)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Account? GetAccountLogin(LoginDto loginDto)
    {
        try
        {
            var loginAccount = _context.Accounts.FirstOrDefault(x => x.Email.ToUpper().Equals(loginDto.Email.ToUpper()));
            if (loginAccount != null && IsPasswordMatch(loginDto.Password, loginAccount.Password))
                return loginAccount;
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static string EncodePassword(string password)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(password);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        for(var i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("X2"));
        }

        return sb.ToString();
    }
    
    public static bool IsPasswordMatch(string inputPassword, string hashedPassword)
    {
        var encodedInputPassword = EncodePassword(inputPassword);
        var check =  encodedInputPassword.Trim().Equals(hashedPassword.Trim());
        return check;
    }
}