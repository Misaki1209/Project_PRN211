using Domain.Models;

namespace Infrastructure.IRepositories;

public interface IAccountRepository
{
    public IEnumerable<Account> GetAllAccount();
    public Account? GetAccountById(int id);
    public int GetNextId();
    public void AddAccount(Account account);
    public void UpdateAccount(Account account);
    public void DeleteAccount(Account account);
}