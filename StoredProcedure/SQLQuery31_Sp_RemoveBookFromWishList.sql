



CREATE     PROCEDURE [dbo].[RemoveBookFromWishList]
	-- Add the parameters for the stored procedure here
	@CustomerID nvarchar(100),
	@BookID bigint
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

	if((select count(BookID) from Book where BookID = @BookID and IsDelete = 0) = 1)
	begin
		if((select count(*) from WishList where CustomerID = @CustomerID and BookID = @BookID) = 1)
		delete from WishList where BookID = @BookID and CustomerID = @CustomerID;
		else
		begin
			set @result = 3;
			throw 50005,'book is not in wish list',-1
		end
	end
	else
	begin
		set @result = 2;
		throw 50005,'book dont exist',-1
	end
	select WishList.CustomerID, WishList.WishListID, WishList.BookID, Book.BookPrice, Book.BookName from 
	WishList inner join Book on WishList.BookID = Book.BookID
	where WishList.CustomerID = @CustomerID;

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
