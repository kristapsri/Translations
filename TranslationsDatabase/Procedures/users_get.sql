CREATE PROCEDURE [dbo].[users_get]
	@Id int
AS
BEGIN
	SELECT * 
	FROM dbo.[Users]
	WHERE Id = @Id
END
