using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Services;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        // Отображение главной страницы
        public async Task<IActionResult> Index()
        {
            try
            {
                // Загрузка списков загруженных файлов
                var response = await _databaseService.GetUploadedFilesListAsync();
                if (response.Success == false)
                {
                    _logger.LogError(response.ErrorMessage);
                    // Отображение страницы с ошибкой при получении списка файлов
                    return View("Error");
                }
                return View(response.Data);
            }
            catch (Exception ex)
            {
                // Логирование ошибки и отображение страницы ошибки
                _logger.LogError(ex, "Ошибка при отображении списка файлов.");
                return View("Error");
            }
        }

        // Отображение данных из файла по ID
        public async Task<IActionResult> ViewFile(string id)
        {
            try
            {
                // Получение данных из файла по ID
                var response = await _databaseService.GetAccountsFromFileAsync(id);
                if (response.Success == false)
                {
                    // Логирование ошибки и отображение страницы с ошибкой
                    _logger.LogError(response.ErrorMessage);
                    return View("Error");
                }
                var accountViewModel = new AccountViewModel()
                {
                    UploadedFileId = id,
                    Accounts = response.Data
                };
                return View(accountViewModel);
            }
            catch (Exception ex)
            {
                // Логирование исключения и отображение страницы с ошибкой
                _logger.LogError(ex, "Ошибка при получении данных из файла.");
                return View("Error");
            }
        }

        // Отображение страницы с политикой конфиденциальности
        public IActionResult Privacy()
        {
            return View();
        }

        // Отображение страницы ошибки
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
