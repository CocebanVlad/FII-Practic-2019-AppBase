CREATE TABLE [dbo].[Rights] (
    [RoleId]     INT NOT NULL,
    [FunctionId] INT NOT NULL,
    [IsEnabled]  BIT NOT NULL,
    CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED ([RoleId] ASC, [FunctionId] ASC),
    CONSTRAINT [FK_Rights_Functions] FOREIGN KEY ([FunctionId]) REFERENCES [dbo].[Functions] ([Id]),
    CONSTRAINT [FK_Rights_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
);

