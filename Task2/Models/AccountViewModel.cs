namespace Task2.Models;

public class AccountViewModel
{
    public string UploadedFileId { get; set; }
    public IEnumerable<Account> Accounts { get; set; }
}
