using Domain.IRepositories;
using Domain.Models;

namespace Domain.Repositories;

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
}