USE [RecipeBookDb]
GO

/****** Object:  Table [dbo].[Recipes]    Script Date: 10/19/2021 6:24:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Recipes](
	[RecipeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[NumberOfPeople] [int] NOT NULL,
	[PreparationTime] [int] NOT NULL,
	[Instructions] [nvarchar](max) NOT NULL,
	[Image] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


