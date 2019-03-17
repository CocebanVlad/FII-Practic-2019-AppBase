CREATE TABLE [dbo].[Function] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (120) NOT NULL,
    CONSTRAINT [PK_Functions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

