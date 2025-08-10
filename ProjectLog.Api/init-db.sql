IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ProjectLog')
BEGIN
    CREATE DATABASE ProjectLog;
END

USE ProjectLog;

DROP TABLE IF EXISTS Projects;
CREATE TABLE Projects (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    CreatedAt DATETIME NOT NULL
);

INSERT INTO Projects (Name, Description, CreatedAt) VALUES ('Sample Project', 'This is a sample project description.', GETDATE());