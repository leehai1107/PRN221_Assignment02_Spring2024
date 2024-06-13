using BusinessObjects;

namespace Repositories.Interface
{
    public interface ISystemAccountRepo
    {
        Task AddSystemAccountAsync(SystemAccount account);
        Task<SystemAccount> GetAccountByEmailAsync(string email);
        Task<List<SystemAccount>> GetAccountsAsync();
        Task<SystemAccount> GetSystemAccountByIdAsync(short id);
        Task RemoveSystemAccountAsync(short id);
        Task UpdateSystemAccountAsync(SystemAccount newAccount);
        Task<List<SystemAccount>> SearchAccountsByNameAsync(string accountName);
    }
}
