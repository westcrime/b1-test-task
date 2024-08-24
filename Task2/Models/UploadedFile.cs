namespace Task2.Models;

public class UploadedFile
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public DateTime UploadDate { get; set; }
    public ICollection<Account> AccountData { get; set; }
}