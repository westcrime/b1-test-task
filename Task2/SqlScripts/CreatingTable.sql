CREATE TABLE YourTableName (
    AccountNumber VARCHAR(10) PRIMARY KEY,
    Class VARCHAR(50),
    IncomingBalanceActive DECIMAL(18, 2),
    IncomingBalancePassive DECIMAL(18, 2),
    TurnoverDebit DECIMAL(18, 2),
    TurnoverCredit DECIMAL(18, 2),
    OutgoingBalanceActive DECIMAL(18, 2),
    OutgoingBalancePassive DECIMAL(18, 2)
);