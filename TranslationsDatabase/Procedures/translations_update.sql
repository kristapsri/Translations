CREATE PROCEDURE [dbo].[translations_update]
	@Id int,
	@TranslationGroup NVARCHAR(450),
	@TranslationKey NVARCHAR(450),
	@Text TEXT,
	@Project NVARCHAR(450),
	@LanguageId INT
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
	WHERE Id = @Id
END