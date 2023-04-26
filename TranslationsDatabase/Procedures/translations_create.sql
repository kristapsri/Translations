CREATE PROCEDURE [dbo].[translations_create]
	@TranslationGroup NVARCHAR(450),
	@TranslationKey NVARCHAR(450),
	@Text TEXT,
	@Project NVARCHAR(450),
	@LanguageId INT,
	@CreatedAt DATETIME
AS
BEGIN
	INSERT INTO [dbo].[Translations] (TranslationGroup, TranslationKey, Text, Project, LanguageId, CreatedAt)
	VALUES (
		@TranslationGroup,
		@TranslationKey,
		@Text,
		@Project,
		@LanguageId,
		GETDATE()
	)
END