CREATE TABLE [dbo].[RecipeIngredient]
(
    [RecipeId] INT NOT NULL, 
    [IngredientId] INT NOT NULL, 
    CONSTRAINT [PK_RecipeIngredient] PRIMARY KEY ([RecipeId], [IngredientId])
)
