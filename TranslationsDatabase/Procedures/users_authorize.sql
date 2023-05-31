CREATE PROCEDURE [dbo].[users_authorize]
	@Username nvarchar(50),
	@Password nvarchar(50)
AS
BEGIN
	SELECT *
	FROM dbo.[Users]
	WHERE Username = @Username AND [Password] = @Password
END
