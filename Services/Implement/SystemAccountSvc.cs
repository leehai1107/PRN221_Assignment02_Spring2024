using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implement
{
    public class SystemAccountSvc : ISystemAccountSvc
    {
        private readonly ISystemAccountRepo _systemAccountRepo;
        private string adminEmail;
        private string adminPassword;

        private void GetAdminAccount()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IConfigurationSection section = config.GetSection("AdminAccount");

            adminEmail = section["Email"];
            adminPassword = section["Password"];
        }

        private bool SignUpWithAdminAccount(string email)
        {
            if (email == adminEmail)
            {
                return true;
            }
            return false;
        }

        public SystemAccountSvc(ISystemAccountRepo systemAccountRepo)
        {
            _systemAccountRepo = systemAccountRepo;
        }

        public async Task AddSystemAccountAsync(SystemAccount account)
        {
            GetAdminAccount();
            if (SignUpWithAdminAccount(account.AccountEmail))
            {
                throw new Exception("Email already exists!");
            }

            SystemAccount existAccount = await _systemAccountRepo.GetAccountByEmailAsync(account.AccountEmail);
            if (existAccount != null)
            {
                throw new Exception("Email already exists!");
            }
            else
            {
                await _systemAccountRepo.AddSystemAccountAsync(account);
            }
        }

        public async Task<List<SystemAccount>> SearchAccountsByNameAsync(string accountName)
        {
            return await _systemAccountRepo.SearchAccountsByNameAsync(accountName);
        }

        public async Task<SystemAccount> GetAccountByEmailAsync(string email)
        {
            return await _systemAccountRepo.GetAccountByEmailAsync(email);
        }

        public async Task<List<SystemAccount>> GetAccountsAsync()
        {
            return await _systemAccountRepo.GetAccountsAsync();
        }

        public async Task<SystemAccount> GetSystemAccountByIdAsync(short id)
        {
            return await _systemAccountRepo.GetSystemAccountByIdAsync(id);
        }

        public async Task RemoveSystemAccountAsync(short id)
        {
            await _systemAccountRepo.RemoveSystemAccountAsync(id);
        }

        public async Task UpdateSystemAccountAsync(SystemAccount newAccount)
        {
            await _systemAccountRepo.UpdateSystemAccountAsync(newAccount);
        }

        public async Task<bool> ValidateAsync(string email, string password)
        {
            try
            {
                GetAdminAccount();
                if (SignUpWithAdminAccount(email))
                {
                    if (password == adminPassword)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


                var account = await _systemAccountRepo.GetAccountByEmailAsync(email);
                if (account == null)
                {
                    return false;
                }
                else if (account.AccountPassword != password)
                {
                    return false;
                }
                else if (account.AccountRole == 2)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("You have no permission to this system!", ex);
            }
        }
    }
}
