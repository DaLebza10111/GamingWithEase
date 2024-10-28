-- Database
CREATE DATABASE ClientTransactionDB;
GO

-- Use the database
USE ClientTransactionDB;
GO

-- Client table
CREATE TABLE Client (
    ClientID SMALLINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Surname NVARCHAR(50) NOT NULL,
    ClientBalance DECIMAL(18, 2) NOT NULL
);
GO

-- TransactionsType table
CREATE TABLE TransactionsType (
    transactionTypeID SMALLINT IDENTITY(1,1) PRIMARY KEY,
    transactionType_name NVARCHAR(50) NOT NULL
);
GO

-- Transactions table
CREATE TABLE Transactions (
    TransactionID SMALLINT IDENTITY(1,1) PRIMARY KEY,
    Amount DECIMAL(18, 2) NOT NULL,
    TransactionTypeID SMALLINT NOT NULL,
    ClientID SMALLINT NOT NULL,
    Comment NVARCHAR(100) NULL,
    FOREIGN KEY (TransactionTypeID) REFERENCES transactionsType(transactionTypeID),
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID)
);
GO

-- Create stored procedure to get transactions by client ID
CREATE PROCEDURE GetTransactionsByClientId
    @ClientID SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * 
    FROM [Transactions] 
    WHERE ClientID = @ClientID;
END
GO

-- Create stored procedure to update transaction comment
CREATE PROCEDURE UpdateTransactionComment
    @TransactionID SMALLINT,
    @Comment NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [Transactions]
    SET Comment = @Comment 
    WHERE TransactionID = @TransactionID;
END
GO

-- Create stored procedure to add a transaction
CREATE PROCEDURE AddTransaction
    @Amount DECIMAL(18, 2),
    @TransactionTypeID SMALLINT,
    @ClientID SMALLINT,
    @Comment NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Transactions] (Amount, TransactionTypeID, ClientID, Comment)
    VALUES (@Amount, @TransactionTypeID, @ClientID, @Comment);

    UPDATE [Client] 
    SET ClientBalance = ClientBalance + @Amount 
    WHERE ClientID = @ClientID;
END
GO

-- Update client balance
CREATE PROCEDURE UpdateClientBalance
    @ClientID SMALLINT,
    @Amount DECIMAL(18, 2),
    @IsCredit BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF @IsCredit = 1
    BEGIN
        UPDATE [Client] 
        SET ClientBalance = ClientBalance + @Amount 
        WHERE ClientID = @ClientID;
    END
    ELSE
    BEGIN
        UPDATE [Client] 
        SET ClientBalance = ClientBalance - @Amount 
        WHERE ClientID = @ClientID;
    END
END
GO
