using Task2.Models;

namespace Task2.Services
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Account>>> GetAccountListAsync();

        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<UploadedFile>>> GetUploadedFilesListAsync();

        /// <summary>
        /// Поиск объекта по accountNumber
        /// </summary>
        /// <param name="accountNumber">Идентификатор объекта</param>
        /// <returns></returns>
        public Task<ResponseData<Account>> GetAccountByaccountNumberAsync(string accountNumber);

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="accountNumber">accountNumber изменяемомго объекта</param>
        /// <param name="account">объект с новыми параметрами</param>
        /// <returns></returns>
        public Task<ResponseData<Account>> UpdateAccountAsync(string accountNumber, Account account);

        /// <summary>
        /// Удаление объекта
        /// </summary>  
        /// <param name="accountNumber">accountNumber удаляемомго объекта</param>
        /// <returns></returns>
        public Task<ResponseData<bool>> DeleteAccountAsync(string accountNumber);

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="account">Новый объект</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<Account>> CreateAccountAsync(Account account);

        /// <summary>
        /// Получение списка объектов из определенного файла
        /// </summary>
        /// <param name="fileId">Айди файла</param>
        /// <returns>Список аккаунтов</returns>
        public Task<ResponseData<List<Account>>> GetAccountsFromFileAsync(string fileId);

        /// <summary>
        /// Добавление информации о файле в бд
        /// </summary>
        /// <param name="file">файл</param>
        /// <returns></returns>
        public Task<ResponseData<UploadedFile>> CreateUploadedFile(UploadedFile file);
    }
}