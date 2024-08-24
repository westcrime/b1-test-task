using Microsoft.EntityFrameworkCore;
using Task2.Data;
using Task2.Models;

namespace Task2.Services
{
    public class DatabaseService : IDatabaseService
    {
        ApplicationDbContext _context;

        // Конструктор класса, принимающий контекст базы данных
        public DatabaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для создания новой записи аккаунта в базе данных
        public async Task<ResponseData<Account>> CreateAccountAsync(Account account)
        {
            if (account != null)
            {
                await _context.Database.MigrateAsync(); // Применение миграций (если необходимо)
                await _context.SaveChangesAsync(); // Сохранение изменений в базе данных
                await _context.Accounts.AddAsync(account); // Добавление нового аккаунта
                await _context.SaveChangesAsync(); // Сохранение изменений
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
                    ErrorMessage = "Ошибка! Аккаунт равен null"
                };
            }
        }

        // Метод для удаления записи аккаунта из базы данных по его номеру
        public async Task<ResponseData<bool>> DeleteAccountAsync(string accountNumber)
        {
            if (accountNumber != "0")
            {
                var game = await _context.Accounts.FindAsync(accountNumber); // Поиск аккаунта по номеру
                if (game != null)
                {
                    await _context.Database.MigrateAsync(); // Применение миграций (если необходимо)
                    await _context.SaveChangesAsync(); // Сохранение изменений в базе данных
                    _context.Accounts.Remove(game); // Удаление аккаунта
                    await _context.SaveChangesAsync(); // Сохранение изменений
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
                        ErrorMessage = $"Ошибка! Аккаунт с номером {accountNumber} не найден"
                    };
                }
            }
            else
            {
                return new ResponseData<bool>()
                {
                    Success = false,
                    Data = false,
                    ErrorMessage = "Ошибка! accountNumber равно 0"
                };
            }
        }

        // Метод для получения списка всех аккаунтов из базы данных
        public async Task<ResponseData<List<Account>>> GetAccountListAsync()
        {
            var accounts = _context.Accounts.ToList(); // Получение всех аккаунтов
            return new ResponseData<List<Account>>()
            {
                Success = true,
                Data = accounts,
                ErrorMessage = string.Empty
            };
        }

        // Метод для получения аккаунта по его номеру
        public async Task<ResponseData<Account>> GetAccountByaccountNumberAsync(string accountNumber)
        {
            if (accountNumber == "0")
            {
                return new ResponseData<Account>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"accountNumber не может быть null (accountNumber = {accountNumber})"
                };
            }
            await _context.Database.MigrateAsync(); // Применение миграций (если необходимо)
            await _context.SaveChangesAsync(); // Сохранение изменений в базе данных
            var account = await _context.Accounts.FindAsync(accountNumber); // Поиск аккаунта по номеру
            if (account == null)
            {
                return new ResponseData<Account>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = $"Аккаунт с номером {accountNumber} не найден"
                };
            }
            return new ResponseData<Account>()
            {
                Data = account,
                Success = true,
                ErrorMessage = null
            };
        }

        // Метод для обновления данных аккаунта
        public async Task<ResponseData<Account>> UpdateAccountAsync(string accountNumber, Account account)
        {
            await _context.Database.MigrateAsync(); // Применение миграций (если необходимо)
            await _context.SaveChangesAsync(); // Сохранение изменений в базе данных
            var accountToUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber); // Поиск аккаунта по номеру
            if (accountToUpdate != null)
            {
                // Обновление данных аккаунта
                accountToUpdate.Class = account.Class;
                accountToUpdate.IncomingBalanceActive = account.IncomingBalanceActive;
                accountToUpdate.IncomingBalancePassive = account.IncomingBalancePassive;
                accountToUpdate.TurnoverDebit = account.TurnoverDebit;
                accountToUpdate.TurnoverCredit = account.TurnoverCredit;
                accountToUpdate.OutgoingBalanceActive = account.OutgoingBalanceActive;
                accountToUpdate.OutgoingBalancePassive = account.OutgoingBalancePassive;
                accountToUpdate.UploadedFileId = account.UploadedFileId;
                _context.Accounts.Update(accountToUpdate); // Обновление аккаунта в базе данных
                await _context.SaveChangesAsync(); // Сохранение изменений
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
                    ErrorMessage = $"Не удалось найти аккаунт для обновления с номером accountNumber = {accountNumber}"
                };
            }
        }

        // Метод для получения всех аккаунтов, связанных с определенным файлом
        public async Task<ResponseData<List<Account>>> GetAccountsFromFileAsync(string fileId)
        {
            var uploadedFile = await _context.UploadedFiles.FirstOrDefaultAsync(f => f.Id == fileId); // Поиск файла по его ID
            if (uploadedFile != null)
            {
                var accounts = await _context.Accounts
                    .Where(a => a.UploadedFileId == fileId)
                    .ToListAsync(); // Получение всех аккаунтов, связанных с этим файлом
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
                    ErrorMessage = $"Не удалось найти файл с ID = {fileId}"
                };
            }
        }

        // Метод для создания записи о загруженном файле
        public async Task<ResponseData<UploadedFile>> CreateUploadedFile(UploadedFile file)
        {
            try
            {
                await _context.UploadedFiles.AddAsync(file); // Добавление записи о файле в базу данных
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

        // Метод для получения списка всех загруженных файлов
        public async Task<ResponseData<List<UploadedFile>>> GetUploadedFilesListAsync()
        {
            try
            {
                var files = await _context.UploadedFiles.ToListAsync(); // Получение списка всех загруженных файлов
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
