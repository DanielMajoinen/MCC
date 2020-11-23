CREATE PROCEDURE [dbo].[uspLedger_FetchByAccount]
	@AccountId INT
AS

SELECT
	L.[Id],
	L.[Date],
	L.[Amount],
	S.[Name] AS 'Status',
	L.[ClosedReason]
FROM [dbo].[Account] A
LEFT JOIN [dbo].[Ledger] L
	ON A.[ID] = L.[AccountId]
JOIN [dbo].[Status] S
	ON L.[StatusId] = S.[Id]
WHERE A.[Id] = @AccountId
ORDER BY L.[Date] DESC