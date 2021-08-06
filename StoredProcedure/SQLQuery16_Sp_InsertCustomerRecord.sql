


CREATE   PROCEDURE [dbo].[InsertCustomerRecord]
	-- Add the parameters for the stored procedure here
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@PhoneNumber bigint,
	@Email nvarchar(50),
	@Password nvarchar(MAX)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @new_identity nvarchar(100);
	DECLARE @result int = 0;
	DECLARE @CustomerID varchar;

	if((select count(Email) from Customer where Email = @Email) = 1)
		begin;
		set @result = 2;
		THROW 52000, 'Email already exist', -1;
		end

	insert into Customer(CustomerFirstName,
	CustomerLastName,
	Email,
	PhoneNumber,
	Password) output Inserted.CustomerID into @Identity
	values(@FirstName,
	@LastName,
	@Email,
	@PhoneNumber,
	@Password);

--	SELECT @new_identity = (select ID from @Identity);

	select CustomerID,
	CustomerFirstName,
	CustomerLastName,
	Email,
	PhoneNumber
	from Customer where CustomerID = (select ID from @Identity);
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
