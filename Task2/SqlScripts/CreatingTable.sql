CREATE TABLE UploadedFiles (
    Id VARCHAR(100) PRIMARY KEY,
    FileName NVARCHAR(255),
    UploadDate DATETIME
);

CREATE TABLE AccountData (
    AccountNumber VARCHAR(10) PRIMARY KEY,
    Class VARCHAR(20),
    IncomingBalanceActive DECIMAL(18, 2),
    IncomingBalancePassive DECIMAL(18, 2),
    TurnoverDebit DECIMAL(18, 2),
    TurnoverCredit DECIMAL(18, 2),
    OutgoingBalanceActive DECIMAL(18, 2),
    OutgoingBalancePassive DECIMAL(18, 2),
    UploadedFileId INT,
    FOREIGN KEY (UploadedFileId) REFERENCES UploadedFiles(Id)
);