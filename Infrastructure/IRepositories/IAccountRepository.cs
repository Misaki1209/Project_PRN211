using Domain.Models;
using Infrastructure.Dtos;

namespace Infrastructure.IRepositories;

public interface IAccountRepository
{
    public IEnumerable<Account> GetAllAccount();
    public Account? GetAccountById(int id);
    public int GetNextId();
    public void AddAccount(Account account);
    public void UpdateAccount(Account account);
    public void DeleteAccount(Account account);
    public void ChangePassword(int accountId, string newPass);
    public Account? GetAccountLogin(LoginDto loginDto);
}