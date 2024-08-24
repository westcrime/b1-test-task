using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task2.Models;
using Task2.Services;
using OfficeOpenXml;

namespace Task2.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ILogger<FileUploadController> _logger;
        private readonly IDatabaseService _databaseService;

        public FileUploadController(ILogger<FileUploadController> logger, IDatabaseService databaseService)
        {
            _databaseService = databaseService;
            _logger = logger;
        }
        
        // Загрузка файла
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // Возвращаем сообщение об ошибке, если файл не выбран
                return Content("Файл не выбран.");
            }

            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

            try
            {
                // Сохранение загруженного файла на сервере
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Создание записи о загруженном файле
                var uploadedFile = new UploadedFile
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = file.FileName,
                    UploadDate = DateTime.Now,
                };

                var responseFromFilesDb = await _databaseService.CreateUploadedFile(uploadedFile);
                if (responseFromFilesDb?.Success == false)
                {
                    _logger.LogError(responseFromFilesDb.ErrorMessage);
                    // Перенаправление на страницу с ошибкой при создании записи о файле
                    return RedirectToAction("Error");
                }

                _logger.LogInformation("Файл сохранился в базу данных");

                // Обработка Excel-файла
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var workbook = package.Workbook;
                    var worksheet = workbook.Worksheets[0];
                    
                    string className = string.Empty;
                    for (int row = 9; row <= worksheet.Dimension.Rows; row++)
                    {
                        if (!decimal.TryParse(worksheet.Cells[row, 1].Value?.ToString(), out decimal result))
                        {
                            className = worksheet.Cells[row, 1].Value?.ToString();
                            continue;
                        }
                        var accountData = new Account
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountNumber = worksheet.Cells[row, 1].Value?.ToString(),
                            Class = className,
                            IncomingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 3].Value),
                            IncomingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 4].Value),
                            TurnoverDebit = Convert.ToDecimal(worksheet.Cells[row, 5].Value),
                            TurnoverCredit = Convert.ToDecimal(worksheet.Cells[row, 6].Value),
                            OutgoingBalanceActive = Convert.ToDecimal(worksheet.Cells[row, 7].Value),
                            OutgoingBalancePassive = Convert.ToDecimal(worksheet.Cells[row, 8].Value),
                            UploadedFileId = uploadedFile.Id
                        };
                        _logger.Log(LogLevel.Critical, $"<-------{accountData.AccountNumber}, {accountData.Class}, {accountData.IncomingBalanceActive}, {accountData.IncomingBalancePassive}, " + 
                        $"{accountData.TurnoverDebit}, {accountData.TurnoverCredit}, {accountData.OutgoingBalanceActive}, {accountData.OutgoingBalancePassive}, {accountData.UploadedFileId}------->");

                        var response = await _databaseService.CreateAccountAsync(accountData);
                        if (response?.Success == false)
                        {
                            _logger.LogError(response.ErrorMessage);
                            // Перенаправление на страницу с ошибкой при создании записи о счете
                            return RedirectToAction("Error");
                        }
                    }
                }

                // Перенаправление на главную страницу после успешной загрузки
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                // Логирование ошибки и перенаправление на страницу ошибки
                _logger.LogError(ex, "Ошибка при загрузке файла.");
                return RedirectToAction("Error");
            }
        }

        // Отображение страницы ошибки
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
