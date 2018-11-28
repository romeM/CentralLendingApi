CREATE TABLE [dbo].[Person_MonthlyStatistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[Person_Id] int NOT NULL,
	[PMT] [decimal](10, 2) NOT NULL,
	[PPMT] [decimal](10, 2) NOT NULL,
	[IPMT] [decimal](10, 2) NOT NULL,
    CONSTRAINT [PK_Person_MonthlyStatistics] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Person_MonthlyStatistics_Person] FOREIGN KEY ([Person_Id]) REFERENCES [Person] ([Id]) ON DELETE CASCADE);
