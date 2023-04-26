CREATE PROCEDURE [dbo].[languages_create]
	@Locale NVARCHAR(6),
	@Name NVARCHAR(60),
	@CreatedAt DATETIME
AS
BEGIN
	INSERT INTO [dbo].[Languages] (Locale, Name, CreatedAt)
	VALUES (
		@Locale,
		@Name,
		GETDATE()
	)
END