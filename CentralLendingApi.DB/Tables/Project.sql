CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Platform] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[Amount] [float] NULL,
	[Rate] [float] NULL,
	[Term] [int] NULL,
	[Link] [nvarchar](max) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC));