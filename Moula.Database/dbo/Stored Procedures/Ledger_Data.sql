CREATE PROCEDURE [dbo].[Ledger_Data]
AS

SET IDENTITY_INSERT [dbo].[Ledger] ON

MERGE INTO [dbo].[Ledger] AS [Target]
USING (VALUES
	(1, 1, '2020-01-01 00:00', 30.00, 2, NULL),
	(2, 1, '2020-01-04 13:25', 100.00, 3, 'Fraud'),
	(3, 1, '2020-02-03 04:13', 67.00, 2, NULL),
	(4, 2, '2020-02-19 06:57', 31.00, 2, NULL),
	(5, 2, '2020-03-14 18:45', 110.00, 3, 'Fraud'),
	(6, 3, '2020-04-23 23:02', 22.00, 1, NULL)
) AS [Source] ([Id], [AccountId], [Date], [Amount], [StatusId], [ClosedReason])
ON TARGET.[Id] = SOURCE.[Id]

WHEN MATCHED AND 
(
	TARGET.[AccountId] <> SOURCE.[AccountId]
	OR TARGET.[Date] <> SOURCE.[Date]
	OR TARGET.[Amount] <> SOURCE.[Amount]
	OR TARGET.[StatusId] <> SOURCE.[StatusId]
	OR TARGET.[ClosedReason] <> SOURCE.[ClosedReason]
)
THEN UPDATE SET [AccountId] = SOURCE.[AccountId],
	[Date] = SOURCE.[Date],
	[Amount] = SOURCE.[Amount],
	[StatusId] = SOURCE.[StatusId],
	[ClosedReason] = SOURCE.[ClosedReason]

WHEN NOT MATCHED BY TARGET
THEN INSERT ([Id], [AccountId], [Date], [Amount], [StatusId], [ClosedReason])
VALUES ([Id], [AccountId], [Date], [Amount], [StatusId], [ClosedReason])

WHEN NOT MATCHED BY SOURCE THEN
DELETE;

SET IDENTITY_INSERT [dbo].[Ledger] OFF