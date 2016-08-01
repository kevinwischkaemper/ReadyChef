CREATE TABLE [dbo].[RecipeIngredient]
(
    [RecipeId] INT NOT NULL, 
    [IngredientId] INT NOT NULL, 
    [Amount] INT NOT NULL, 
    [AmountUnits] NVARCHAR(50) NOT NULL DEFAULT 'units', 
    CONSTRAINT [PK_RecipeIngredient] PRIMARY KEY ([RecipeId], [IngredientId])
)
