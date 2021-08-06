


CREATE   PROCEDURE [dbo].[GetCustomerBookRecord]
	-- Add the parameters for the stored procedure here
	@CustomerID nvarchar(100)
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

Select *
into #MyTempTable
from Cart where  CustomerID=@CustomerID;


select Book.BookID, BookName, BookDiscription, BookImage, BookPrice, Book.AuthorID, InStock,
	BookQuantity, AuthorName, cast(IsNull(CartID,0) as bit) as InCart
	from Book inner join Author on Book.AuthorID = Author.AuthorID
	left  join #MyTempTable on Book.BookID = #MyTempTable.BookID
	where IsDelete = 0
drop table #MyTempTable;

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
