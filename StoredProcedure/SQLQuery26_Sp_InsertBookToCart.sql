


CREATE   PROCEDURE [dbo].[InsertBookToCart]
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
	DECLARE @TotalCost int = 0;

	if((select count(BookID) from Book where BookID = @BookID and IsDelete = 0) = 1)
	begin
		if((select InStock from Book where BookID = @BookID) = 1)
		begin;
			set @result = 1;
			set @TotalCost = (select BookPrice from Book where BookID = @BookID); 
			
			if((select count(*) from Cart where
			BookID = @BookID and CustomerID = @CustomerID) = 0)
			begin
			
				insert into Cart(CustomerID, BookID, TotalCost) 
				values(@CustomerID, @BookID, @TotalCost);
			end
			else
				update Cart set BookCount = BookCount +1,
				TotalCost = TotalCost + @TotalCost
				where BookID = @BookID and CustomerID = @CustomerID;
				print @TotalCost
		end
		else
		begin
			set @result = 3;
			throw 50003,'Book out of stock',-1;
		end
	end
	else
	begin
		set @result = 2;
		throw 50003,'Book dont exist',-1;
	end

	select Cart.CustomerID, Cart.CartID, Cart.BookID, Book.BookPrice, Book.BookName,
	BookCount, TotalCost from 
	Cart inner join Book on Cart.BookID = Book.BookID
	where Cart.CustomerID = @CustomerID;


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
