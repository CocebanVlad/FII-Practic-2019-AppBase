CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (250) NOT NULL,
    [Email]     NVARCHAR (250) NOT NULL,
    [FirstName] NVARCHAR (60)  NULL,
    [LastName]  NVARCHAR (60)  NULL,
    [BirthDate] DATETIME       NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

