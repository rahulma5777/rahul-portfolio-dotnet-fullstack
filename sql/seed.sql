-- Inserts sample data into Users and TaskItems tables

INSERT INTO Users (Name, Email)
VALUES
    ('Alice Johnson', 'alice@example.com'),
    ('Bob Smith', 'bob@example.com'),
    ('Charlie Brown', 'charlie@example.com');

INSERT INTO TaskItems (Title, IsCompleted, UserId)
VALUES
    ('Set up the project structure', 1, 1),
    ('Implement RESTful API', 0, 1),
    ('Design database schema', 0, 2),
    ('Write README documentation', 1, 3);