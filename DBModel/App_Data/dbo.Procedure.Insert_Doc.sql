CREATE PROCEDURE   [dbo].[InsertDoc] 
	@Name nvarchar(MAX),
	@OriginalFN nvarchar(MAX),
	@Date DateTime,
	@Author nvarchar(MAX),
	@Newid nvarchar(MAX) OUTPUT
AS
	Insert Into [dbo].[Documents] (Id, Name, OriginalFileName, Date, Author_id)
	Values(NEWID(), @Name, @OriginalFN, @Date, @Author)

	SELECT @Newid = SCOPE_IDENTITY()

RETURN 0
