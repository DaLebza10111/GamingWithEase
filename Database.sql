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
