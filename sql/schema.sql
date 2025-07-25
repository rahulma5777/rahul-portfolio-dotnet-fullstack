-- SQL Server schema for NetFullStack project
-- Creates Users and TaskItems tables with a one‑to‑many relationship

CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NULL
);

CREATE TABLE TaskItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT (0),
    UserId INT NOT NULL,
    CONSTRAINT FK_TaskItems_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE CASCADE
);