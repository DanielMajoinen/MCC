CREATE PROCEDURE [dbo].[uspAccount_Fetch]
	@Id INT
AS

SELECT
	[Id],
	[Balance]
FROM [dbo].[Account]
WHERE [Id] = @Id