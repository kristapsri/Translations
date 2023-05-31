CREATE PROCEDURE [dbo].[users_create]
	@Username NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
	INSERT INTO dbo.[Users] (Username, [Password])
	VALUES (
		@Username,
		@Password
	)
END
