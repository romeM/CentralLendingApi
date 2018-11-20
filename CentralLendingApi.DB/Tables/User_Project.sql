CREATE TABLE [dbo].[User_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] int NOT NULL,
	[Project_Id] int NOT NULL,
	[ProjectAmount] [int] NOT NULL,
	[ProjectStartDate] [date]  NOT NULL,
    CONSTRAINT [PK_User_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Project_User] FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_User_Project_Project] FOREIGN KEY ([Project_Id]) REFERENCES [Project] ([Id]) ON DELETE CASCADE);