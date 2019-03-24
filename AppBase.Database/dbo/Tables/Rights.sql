CREATE TABLE [dbo].[Rights] (
    [RoleName]     NVARCHAR (50) NOT NULL,
    [Function]	nvarchar(120) NOT NULL,
    [IsEnabled]  BIT NOT NULL,
    CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED ([RoleName] ASC, [Function] ASC),
    CONSTRAINT [FK_Rights_Functions] FOREIGN KEY ([Function]) REFERENCES [dbo].[Functions] ([Name]),
    CONSTRAINT [FK_Rights_Roles] FOREIGN KEY ([RoleName]) REFERENCES [dbo].[Roles] ([Name])
);

