CREATE TABLE [dbo].[Translations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TranslationGroup] NVARCHAR(450) NOT NULL,
    [TranslationKey] NVARCHAR(450) NOT NULL,
    [Text] TEXT NOT NULL,
    [Project] NVARCHAR(450) NOT NULL,
    [LanguageId] INT NOT NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [UpdatedAt] DATETIME NULL
	FOREIGN KEY (LanguageId) REFERENCES [Languages](Id),
)
