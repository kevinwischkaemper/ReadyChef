CREATE TABLE [dbo].[Recipe]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ReadyTime] INT NOT NULL, 
    [Details] NVARCHAR(MAX) NOT NULL DEFAULT 'None available'
)
