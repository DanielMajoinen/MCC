﻿CREATE TABLE [dbo].[Status]
(
	[Id]	INT IDENTITY (1,1)	NOT NULL,
	[Name]	VARCHAR(30)			NOT NULL
	CONSTRAINT [PK_Status] PRIMARY KEY ([Id] ASC)
)
