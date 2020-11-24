CREATE PROCEDURE [dbo].[Account_Data]
AS

SET IDENTITY_INSERT [dbo].[Account] ON

MERGE INTO [dbo].[Account] AS [Target]
USING (VALUES
	(1, 100.00),
	(2, 3250.00),
	(3, 0.00)
) AS [Source] ([Id], [Balance])
ON TARGET.[Id] = SOURCE.[Id]

WHEN MATCHED AND (TARGET.[Balance] <> SOURCE.[Balance])
THEN UPDATE SET [Balance] = SOURCE.[Balance]

WHEN NOT MATCHED BY TARGET
THEN INSERT ([Id], [Balance]) VALUES ([Id], [Balance])

WHEN NOT MATCHED BY SOURCE THEN
DELETE;

SET IDENTITY_INSERT [dbo].[Account] OFF