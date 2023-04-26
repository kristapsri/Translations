CREATE TABLE [dbo].[Translations]
(
	[TranslationGroup] NVARCHAR(450) NOT NULL,
    [TranslationKey] NVARCHAR(450) NOT NULL,
    [Text] TEXT NOT NULL,
    [Project] NVARCHAR(450) NOT NULL,
    [LanguageId] INT NOT NULL,
    [CreatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    [UpdatedAt] DATETIME NULL
    CONSTRAINT PK_TranslationId PRIMARY KEY (TranslationGroup, TranslationKey),
	FOREIGN KEY (LanguageId) REFERENCES [Languages](Id),
)
