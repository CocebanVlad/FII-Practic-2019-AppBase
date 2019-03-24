CREATE TABLE [dbo].[UserInRoles] (
    [User] nvarchar(250) NOT NULL,
    [Role] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_UserInRoles] PRIMARY KEY CLUSTERED ([User] ASC, [Role] ASC),
    CONSTRAINT [FK_UserInRoles_Roles] FOREIGN KEY ([Role]) REFERENCES [dbo].[Roles] ([Name]),
    CONSTRAINT [FK_UserInRoles_Users] FOREIGN KEY ([User]) REFERENCES [dbo].[Users] ([UserName])
);

