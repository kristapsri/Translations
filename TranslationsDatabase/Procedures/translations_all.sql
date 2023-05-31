CREATE PROCEDURE [dbo].[translations_all]
	@Offset INT,
	@Limit INT
AS
BEGIN
	SELECT * FROM [dbo].[Translations]
	ORDER BY CreatedAt DESC
	OFFSET @Offset ROWS
	FETCH NEXT @Limit ROWS ONLY
END