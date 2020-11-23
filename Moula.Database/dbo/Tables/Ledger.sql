CREATE TABLE [dbo].[Ledger]
(
	[Id]			BIGINT IDENTITY (1,1)	NOT NULL,
	[AccountId]		INT						NOT NULL,
	[Date]			DATETIME2				NOT NULL CONSTRAINT [DF_Ledger_Date] DEFAULT GETUTCDATE(),
	[Amount]		DECIMAL (18,3)			NOT NULL,
	[StatusId]		INT						NOT NULL,
	[ClosedReason]	NVARCHAR(250)			NULL, -- NVARCHAR to support UTF-8 charset
	CONSTRAINT [PK_Ledger] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Ledger_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Id]),
	CONSTRAINT [FK_Ledger_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_Ledger_AccountId_Date] ON [dbo].[Ledger] ([AccountId], [Date] DESC)
GO