CREATE PROCEDURE [dbo].[translations_detail]
	@Id INT
AS
BEGIN
	SELECT item.*
	FROM dbo.[Translations] AS item
	WHERE item.Id = @Id
END
