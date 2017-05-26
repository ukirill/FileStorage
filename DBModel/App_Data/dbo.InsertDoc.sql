CREATE PROCEDURE   [dbo].[InsertDoc] 
	@Name nvarchar(MAX),
	@OriginalFN nvarchar(MAX),
	@Date DateTime,
	@Author nvarchar(MAX),
	@Newid uniqueidentifier OUTPUT
	AS
	SET @Newid = NEWID()
	Insert Into [dbo].[Documents] (Id, Name, OriginalFileName, Date, Author_id)
	Values(@Newid, @Name, @OriginalFN, @Date, @Author)

	SELECT @Newid

RETURN 0