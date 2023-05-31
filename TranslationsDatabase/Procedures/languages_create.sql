CREATE PROCEDURE [dbo].[languages_create]
	@Locale NVARCHAR(6),
	@Name NVARCHAR(60)
AS
BEGIN
	INSERT INTO [dbo].[Languages] (Locale, Name)
	OUTPUT INSERTED.*
	VALUES (
		@Locale,
		@Name
	)
END