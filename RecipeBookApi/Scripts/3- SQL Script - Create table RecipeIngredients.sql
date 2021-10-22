USE [RecipeBookDb]
GO

/****** Object:  Table [dbo].[RecipeIngredients]    Script Date: 10/19/2021 6:23:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RecipeIngredients](
	[RecipeId] [bigint] NOT NULL,
	[IngredientId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC,
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([IngredientId])
GO

ALTER TABLE [dbo].[RecipeIngredients]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipes] ([RecipeId])
GO


