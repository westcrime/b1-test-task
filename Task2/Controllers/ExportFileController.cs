using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc;
using Task2.Services;

public class ExportFileController : Controller
{
    private readonly IDatabaseService _databaseService;
    private readonly ILogger<ExportFileController> _logger;

    public ExportFileController(ILogger<ExportFileController> logger, IDatabaseService databaseService)
    {
        _databaseService = databaseService;
        _logger = logger; 
    }

    [HttpGet]
    public async Task<IActionResult> ExportToExcel(string fileId)
    {
        // Получение аккаунтов из файла
        var response = await _databaseService.GetAccountsFromFileAsync(fileId);
        if (response?.Success == false)
        {
            _logger.LogError(response.ErrorMessage);
            // Перенаправление на страницу с ошибкой при создании записи о файле
            return RedirectToAction("Error");
        }

        var accounts = response.Data;

        // Заполнение эксель файла данными
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Accounts");
            worksheet.Cells[1, 1].Value = "Б/сч";
            worksheet.Cells[1, 2].Value = "Класс";
            worksheet.Cells[1, 3].Value = "Входящее Актив";
            worksheet.Cells[1, 4].Value = "Входящее Пассив";
            worksheet.Cells[1, 5].Value = "Дебет";
            worksheet.Cells[1, 6].Value = "Кредит";
            worksheet.Cells[1, 7].Value = "Исходящее Актив";
            worksheet.Cells[1, 8].Value = "Исходящее Пассив";

            for (int i = 0; i < accounts.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = accounts[i].AccountNumber;
                worksheet.Cells[i + 2, 2].Value = accounts[i].Class;
                worksheet.Cells[i + 2, 3].Value = accounts[i].IncomingBalanceActive;
                worksheet.Cells[i + 2, 4].Value = accounts[i].IncomingBalancePassive;
                worksheet.Cells[i + 2, 5].Value = accounts[i].TurnoverDebit;
                worksheet.Cells[i + 2, 6].Value = accounts[i].TurnoverCredit;
                worksheet.Cells[i + 2, 7].Value = accounts[i].OutgoingBalanceActive;
                worksheet.Cells[i + 2, 8].Value = accounts[i].OutgoingBalancePassive;
            }

            var bytes = package.GetAsByteArray();
            // Передача готового файла
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "accounts.xlsx");
        }
    }
}
