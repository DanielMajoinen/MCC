CREATE PROCEDURE [dbo].[Status_Data]
AS

SET IDENTITY_INSERT [dbo].[Status] ON

MERGE INTO [dbo].[Status] AS [Target]
USING (VALUES
	(1, 'Pending'),
	(2, 'Confirmed'),
	(3, 'Closed')
) AS [Source] ([Id], [Name])
ON TARGET.[Id] = SOURCE.[Id]

WHEN MATCHED AND (TARGET.[Name] <> SOURCE.[Name])
THEN UPDATE SET [Name] = SOURCE.[Name]

WHEN NOT MATCHED BY TARGET
THEN INSERT ([Id], [Name]) VALUES ([Id], [Name])

WHEN NOT MATCHED BY SOURCE THEN
DELETE;

SET IDENTITY_INSERT [dbo].[Status] OFF