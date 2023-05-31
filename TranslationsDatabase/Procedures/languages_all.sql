CREATE PROCEDURE [dbo].[languages_all]
	@Offset INT,
	@Limit INT
AS
BEGIN
	SELECT * FROM [dbo].[Languages]
	ORDER BY CreatedAt DESC
	OFFSET @Offset ROWS
	FETCH NEXT @Limit ROWS ONLY
END