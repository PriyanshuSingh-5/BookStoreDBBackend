

CREATE     PROCEDURE [dbo].[DeleteBookRecord] 
	-- Add the parameters for the stored procedure here
	@BookID varchar(100)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @result int = 0;

	if((select count(*) from Book where BookID = @BookID) = 0)
	begin;
		set @result = 2;
		throw 5000,'Book dont exist',-1;
	end

	if((select IsDelete from Book where BookID = @BookID) = 0)
	begin
	update Book set IsDelete = 1, BookQuantity = 0 where BookID = @BookID;
	set @result = 1;
	end
	else
	begin
		set @result = 3;
		throw 5001,'Book already deleted',-1;
	end

COMMIT TRANSACTION;	
print @result;
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
