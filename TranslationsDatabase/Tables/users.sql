﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Username] NVARCHAR(50) NOT NULL UNIQUE,
	[Password] NVARCHAR(50) NOT NULL
)
