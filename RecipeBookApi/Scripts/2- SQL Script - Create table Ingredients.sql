USE [RecipeBookDb]
GO

/****** Object:  Table [dbo].[Ingredients]    Script Date: 10/19/2021 6:23:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ingredients](
	[IngredientId] [bigint] IDENTITY(1,1) NOT NULL,
	[IngredientName] [nvarchar](50) NOT NULL,
	[IngredientDescription] [nvarchar](100) NOT NULL,
	[IngredientAmount] [float] NOT NULL,
	[IngredientMeasurementType] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


