CREATE PROCEDURE [dbo].[languages_update]
	@Id int,
	@Locale NVARCHAR(6),
	@Name NVARCHAR(60)
AS
BEGIN
	UPDATE [dbo].[Languages]
	SET
		Locale = @Locale,
		Name = @Name,
		UpdatedAt = GETDATE()
	WHERE Id = @Id
END