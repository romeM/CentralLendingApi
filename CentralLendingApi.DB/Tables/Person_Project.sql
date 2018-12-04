CREATE TABLE [dbo].[Person_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Person_Id] int NOT NULL,
	[Project_Id] int NOT NULL,
	[Amount] DECIMAL(18, 2) NOT NULL,
	[StartDate] [date]  NOT NULL,
    CONSTRAINT [PK_Person_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Person_Project_Person] FOREIGN KEY ([Person_Id]) REFERENCES [Person] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_Person_Project_Project] FOREIGN KEY ([Project_Id]) REFERENCES [Project] ([Id]) ON DELETE CASCADE);