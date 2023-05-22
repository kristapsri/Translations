CREATE PROCEDURE [dbo].[languages_detail]
	@Id INT
AS
BEGIN
	SELECT item.*
	FROM dbo.[Languages] AS item
	WHERE item.Id = @Id
END
