-- Stored procedures for NetFullStack project

-- Returns all tasks for a given user
IF OBJECT_ID('dbo.usp_GetTasksByUser', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetTasksByUser;
GO

CREATE PROCEDURE dbo.usp_GetTasksByUser
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, IsCompleted, UserId
    FROM TaskItems
    WHERE UserId = @UserId;
END;
GO

-- Returns the total number of users
IF OBJECT_ID('dbo.usp_GetUserCount', 'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_GetUserCount;
GO

CREATE PROCEDURE dbo.usp_GetUserCount
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS UserCount
    FROM Users;
END;
GO