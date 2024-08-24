namespace Task2.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Account
{
    [Key]
    [Column(TypeName = "VARCHAR(100)")]
    public string Id { get; set; }
    [Column(TypeName = "VARCHAR(10)")]
    public string AccountNumber { get; set; }

    [Column(TypeName = "VARCHAR(50)")]
    public string Class { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? IncomingBalanceActive { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? IncomingBalancePassive { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? TurnoverDebit { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? TurnoverCredit { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? OutgoingBalanceActive { get; set; }

    [Column(TypeName = "DECIMAL(18, 2)")]
    public decimal? OutgoingBalancePassive { get; set; }

    public string UploadedFileId { get; set; }
    public UploadedFile UploadedFile { get; set; }
}
