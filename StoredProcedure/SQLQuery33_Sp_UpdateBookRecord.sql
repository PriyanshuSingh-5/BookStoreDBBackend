



CREATE     PROCEDURE [dbo].[UpdateBookRecord]
	-- Add the parameters for the stored procedure here
	@BookID bigint,
	@BookQuantity int,
	@AuthorName nvarchar(50),
	@BookName NVARCHAR(50),
	@BookDiscription nvarchar(max),
	@BookImage nvarchar(max),
	@BookPrice int
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @AuthorID bigint;
	DECLARE @result int = 0;
		
	if((select count(*) from Book where BookID = @BookID) = 0)
	begin
		set @result = 2; 
		throw 5000,'Book dont exist',-1;
	end
	set @AuthorID = (select AuthorID from Author where AuthorName = @AuthorName)
	if(IsNull(@AuthorID, 0) = 0)
	begin
		insert into Author output inserted.AuthorID into @Identity values(@AuthorName);
	    set @AuthorID = (select ID from @Identity);
	end

	update Book set BookName = @BookName, BookDiscription = @BookDiscription,
	BookQuantity = @BookQuantity,
	BookImage = @BookImage, BookPrice = @BookPrice, AuthorID = @AuthorID
	where BookID = @BookID;

	select BookID, BookName, BookDiscription, BookImage, BookPrice, Book.AuthorID, InStock,
	BookQuantity, AuthorName, IsDelete
	from Book inner join Author on Book.AuthorID = Author.AuthorID where
	IsDelete = 0 and BookID = @BookID;

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
