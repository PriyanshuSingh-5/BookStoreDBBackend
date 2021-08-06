

CREATE   PROCEDURE [dbo].[DeleteCustomerAddress]
	-- Add the parameters for the stored procedure here
	@CustomerID NVARCHAR(100),
	@AddressID bigint

AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	DECLARE @result int = 0;

	if((select count(*) from CustomerAddress 
	where CustomerID = @CustomerID and CustomerAddressID = @AddressID) = 1)
	begin
		delete from CustomerAddress where CustomerID = @CustomerID and CustomerAddressID = @AddressID; 
		set @result = 1;
		
	end
	else 
	begin
		set @result = 2;
		throw 50010, 'AddressID dont exist',-1;
		
	end
	
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
