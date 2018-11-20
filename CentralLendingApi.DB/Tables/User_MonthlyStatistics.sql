CREATE TABLE [dbo].[User_MonthlyStatistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[User_Id] int NOT NULL,
	[PMT] [decimal](10, 2) NOT NULL,
	[PPMT] [decimal](10, 2) NOT NULL,
	[IPMT] [decimal](10, 2) NOT NULL,
    CONSTRAINT [PK_User_MonthlyStatistics] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_MonthlyStatistics_User] FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]) ON DELETE CASCADE);
