


CREATE     PROCEDURE [dbo].[InsertCustomerAddress]
	-- Add the parameters for the stored procedure here
	@CustomerID NVARCHAR(100),
	@Name nvarchar(50),
	@Pincode int,
	@Locality nvarchar(50),
	@PhoneNumber bigint,
	@Address nvarchar(100),
	@City nvarchar(50),
	@Landmark nvarchar(50),
	@AddressType nvarchar(50)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @new_identity table(ID int);
	DECLARE @result int = 0;

	insert into CustomerAddress(CustomerID,
	Name,Pincode,PhoneNumber,Locality,Address,City,
	Landmark, AddressType) output inserted.CustomerAddressID into @new_identity
	values(@CustomerID,
	@Name,
	@Pincode,
	@PhoneNumber,
	@Locality,
	@Address,
	@City,
	@Landmark,
	@AddressType);

	select * from CustomerAddress where CustomerAddressID = (select ID from @new_identity);

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
