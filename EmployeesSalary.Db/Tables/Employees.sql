CREATE TABLE [dbo].[Employees]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [DateCreated] DATETIME NOT NULL CONSTRAINT [DF_Employees_DateCreated] DEFAULT (sysdatetime()),
    [Salary] INT NOT NULL,
    [FirstName] NVARCHAR(50) NOT NULL,
    [LastName] NVARCHAR(50) NOT NULL,
    [PhoneNumber] NVARCHAR(20) NOT NULL,
    [ImportedFileId] INT NULL ,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employees_ImportedFiles] FOREIGN KEY ([ImportedFileId]) REFERENCES [dbo].[ImportedFiles] ([Id])
)
