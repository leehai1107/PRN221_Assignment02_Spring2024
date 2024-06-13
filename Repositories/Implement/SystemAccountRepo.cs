using BusinessObjects;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Implement
{
    public class SystemAccountRepo : ISystemAccountRepo
    {
        public async Task AddSystemAccountAsync(SystemAccount account)
        {
            using (FunewsManagementDbContext _context = new())
            {
                try
                {
                    _context.SystemAccounts.Add(account);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Can not add this account", ex);

                }
            }
        }

        public async Task<SystemAccount> GetAccountByEmailAsync(string email)
        {
            using (FunewsManagementDbContext _context = new())
            {
                try
                {
                    return await _context.SystemAccounts.FirstOrDefaultAsync(x => x.AccountEmail == email);
                }
                catch (Exception ex)
                {
                    throw new Exception("Can not get account", ex);
                }
            }
        }

        public async Task<List<SystemAccount>> GetAccountsAsync()
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.SystemAccounts.Include(x => x.NewsArticles).ToListAsync();
            }
        }

        public async Task<SystemAccount> GetSystemAccountByIdAsync(short id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                return await _context.SystemAccounts.FindAsync(id);
            }
        }

        public async Task RemoveSystemAccountAsync(short id)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var accountToRemove = await _context.SystemAccounts.FindAsync(id);
                if (accountToRemove != null)
                {
                    try
                    {
                        _context.SystemAccounts.Remove(accountToRemove);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Can not delete this account", ex);

                    }
                }
                else
                {
                    throw new ArgumentException("Account not found", nameof(id));
                }
            }
        }

        public async Task UpdateSystemAccountAsync(SystemAccount newAccount)
        {
            using (FunewsManagementDbContext _context = new())
            {
                var existAccount = await _context.SystemAccounts.FindAsync(newAccount.AccountId);
                if (existAccount != null)
                {
                    try
                    {
                        existAccount.AccountName = newAccount.AccountName;
                        existAccount.AccountEmail = newAccount.AccountEmail;
                        existAccount.AccountRole = newAccount.AccountRole;
                        existAccount.AccountPassword = newAccount.AccountPassword;

                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Can not update this account", ex);

                    }
                }
                else { throw new ArgumentException("Account not found", newAccount.AccountId.ToString()); }
            }
        }
        public async Task<List<SystemAccount>> SearchAccountsByNameAsync(string accountName)
        {
            using (FunewsManagementDbContext _context = new())
            {
                // Query the database for categories matching the provided name
                return await _context.SystemAccounts
                               .Where(a => a.AccountName.Contains(accountName))
                               .ToListAsync();
            }
        }
    }
}
