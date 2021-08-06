



CREATE   PROCEDURE [dbo].[InserBookRecord]
	-- Add the parameters for the stored procedure here

	@AuthorName nvarchar(50),
	@BookName NVARCHAR(50),
	@BookDiscription nvarchar(max),
	@BookImage nvarchar(max),
	@BookPrice int,
	@BookQuantity int
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @BookIdentity table (ID bigint);
	DECLARE @AuthorID bigint;
	DECLARE @result int = 0;

	set @AuthorID = (select AuthorID from Author where AuthorName = @AuthorName)
	if(IsNull(@AuthorID, 0) = 0)
	begin
		insert into Author output inserted.AuthorID into @Identity values(@AuthorName);
	    set @AuthorID = (select ID from @Identity);
	end

	insert into Book(BookName, BookDiscription, BookImage, BookPrice, AuthorID, BookQuantity)
	output inserted.BookID into @BookIdentity
	values(@BookName, @BookDiscription, @BookImage, @BookPrice, @AuthorID, @BookQuantity);

	
	
	select BookID, BookName, BookDiscription, BookImage, BookPrice, Book.AuthorID, InStock,
	BookQuantity, AuthorName, IsDelete
	from Book inner join Author on Book.AuthorID = Author.AuthorID where
	IsDelete = 0 and BookID = (select ID from @BookIdentity);
	
	set @result = 1;
COMMIT TRANSACTION;	
return @result;
END TRY
BEGIN CATCH
--SELECT ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
IF(XACT_STATE()) = -1
	BEGIN
		PRINT
		'transaction is uncommitable' + ' rolling back transaction'
		ROLLBACK TRANSACTION;
		print @result;
		return @result;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		print @result;
		return @result;
	END;
END CATCH
	
END
GO
