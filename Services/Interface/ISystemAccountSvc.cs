using BusinessObjects;

namespace Services.Interface
{
    public interface ISystemAccountSvc
    {
        Task AddSystemAccountAsync(SystemAccount account);
        Task<List<SystemAccount>> SearchAccountsByNameAsync(string accountName);
        Task<SystemAccount> GetAccountByEmailAsync(string email);
        Task<List<SystemAccount>> GetAccountsAsync();
        Task<SystemAccount> GetSystemAccountByIdAsync(short id);
        Task RemoveSystemAccountAsync(short id);
        Task UpdateSystemAccountAsync(SystemAccount newAccount);
        Task<bool> ValidateAsync(string email, string password);
    }
}
