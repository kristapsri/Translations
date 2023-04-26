CREATE PROCEDURE [dbo].[translations_update]
	@TranslationGroup NVARCHAR(450),
	@TranslationKey NVARCHAR(450),
	@Text TEXT,
	@Project NVARCHAR(450),
	@LanguageId INT,
	@UpdatedAt DATETIME
AS
BEGIN
	UPDATE [dbo].[Translations]
	SET
		TranslationGroup = @TranslationGroup,
		TranslationKey = @TranslationKey,
		Text = @Text,
		Project = @Project,
		LanguageId = @LanguageId,
		UpdatedAt = GETDATE()
	WHERE TranslationGroup = @TranslationGroup 
	AND TranslationKey = @TranslationKey
	AND Project = @Project
END