CREATE TABLE [dbo].[ImportedFiles]
(
    [Id] INT IDENTITY (1, 1) NOT NULL,
    [DateCreated] DATETIME NOT NULL CONSTRAINT [DF_ImportedFiles_DateCreated] DEFAULT (sysdatetime()),
    [FileName] NVARCHAR(500) NOT NULL,
    [Status] INT NOT NULL,
    CONSTRAINT [PK_ImportedFiles] PRIMARY KEY CLUSTERED ([Id] ASC)
)
