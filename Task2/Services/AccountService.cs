using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Models;

namespace Task2.Services
{
    public class AccountService : IAccountService
    {
        ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<Account>> CreateAccountAsync(Account account)
        {
            if (account != null)
            {
                await _context.Database.MigrateAsync();
                await _context.SaveChangesAsync();
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return new ResponseData<Account>()
                {
                    Success = true,
                    Data = account,
                    ErrorMessage = string.Empty
                };
            }
            else
            {
                return new ResponseData<Account>()
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = "Error! Account is null pointer"
                };
            }
        }

        public async Task<ResponseData<bool>> DeleteAccountAsync(string accountNumber)
        {
            if (accountNumber != "0")
            {
                var game = await _context.Accounts.FindAsync(accountNumber);
                if (game != null)
                {
                    await _context.Database.MigrateAsync();
                    await _context.SaveChangesAsync();
                    _context.Accounts.Remove(game);
                    await _context.SaveChangesAsync();
                    return new ResponseData<bool>()
                    {
                        Success = true,
                        Data = true,
                        ErrorMessage = string.Empty
                    };
                }
                else
                {
                    return new ResponseData<bool>()
                    {
                        Success = false,
                        Data = false,
                        ErrorMessage = $"Error! account with accountNumber {accountNumber} is not found"
                    };
                }
            }
            else
            {
                return new ResponseData<bool>()
                {
                    Success = false,
                    Data = false,
                    ErrorMessage = "Error! accountNumber equals to 0"
                };
            }
        }


        public async Task<ResponseData<List<Account>>> GetAccountListAsync()
        {
            var accounts = _context.Accounts.ToList();
            return new ResponseData<List<Account>>()
            {
                Success = true,
                Data = accounts,
                ErrorMessage = string.Empty
            };
        }

        public async Task<ResponseData<Account>> GetAccountByaccountNumberAsync(string accountNumber)
        {
            if (accountNumber == "0")
            {
                return new ResponseData<Account>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"accountNumber cant be null (accountNumber = {accountNumber})"
                };
            }
            await _context.Database.MigrateAsync();
            await _context.SaveChangesAsync();
            var account = await _context.Accounts.FindAsync(accountNumber);
            if (account == null)
            {
                return new ResponseData<Account>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"Account with {accountNumber} is not found"
                };
            }
            return new ResponseData<Account>()
            {
                Data = account,
                Success = true,
                ErrorMessage = null
            };
        }

        public async Task<ResponseData<Account>> UpdateAccountAsync(string accountNumber, Account account)
        {
            await _context.Database.MigrateAsync();
            await _context.SaveChangesAsync();
            var accountToUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
            if (accountToUpdate != null)
            {
                accountToUpdate.Class = account.Class;
                accountToUpdate.IncomingBalanceActive = account.IncomingBalanceActive;
                accountToUpdate.IncomingBalancePassive = account.IncomingBalancePassive;
                accountToUpdate.TurnoverDebit = account.TurnoverDebit;
                accountToUpdate.TurnoverCredit = account.TurnoverCredit;
                accountToUpdate.OutgoingBalanceActive = account.OutgoingBalanceActive;
                accountToUpdate.OutgoingBalancePassive = account.OutgoingBalancePassive;
                accountToUpdate.UploadedFileId = account.UploadedFileId;
                _context.Accounts.Update(accountToUpdate);
                await _context.SaveChangesAsync();
                return new ResponseData<Account>()
                {
                    Data = account,
                    Success = true,
                    ErrorMessage = string.Empty
                };
            }
            else
            {
                return new ResponseData<Account>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"cant find a account to update with accountNumber = {accountNumber}"
                };
            }
        }

        public async Task<ResponseData<List<Account>>> GetAccountsFromFileAsync(string fileId) 
        {
            var uploadedFile = await _context.UploadedFiles.FirstOrDefaultAsync(f => f.Id == fileId);
            if (uploadedFile != null)
            {
                var accounts = await _context.Accounts
                    .Where(a => a.UploadedFileId == fileId)
                    .ToListAsync();
                return new ResponseData<List<Account>>()
                {
                    Data = accounts,
                    Success = true,
                    ErrorMessage = string.Empty
                };   
            } 
            else
            {
                return new ResponseData<List<Account>>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"cant find a file with id = {fileId}"
                }; 
            }
        }

        public async Task<ResponseData<UploadedFile>> CreateUploadedFile(UploadedFile file)
        {
            try
            {
                await _context.UploadedFiles.AddAsync(file);
                return new ResponseData<UploadedFile>()
                {
                    Data = file,
                    Success = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (System.Exception ex)
            {
                return new ResponseData<UploadedFile>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ResponseData<List<UploadedFile>>> GetUploadedFilesListAsync()
        {
            try
            {
                var files = await _context.UploadedFiles.ToListAsync();
                return new ResponseData<List<UploadedFile>>()
                {
                    Data = files,
                    Success = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (System.Exception ex)
            {
                return new ResponseData<List<UploadedFile>>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}