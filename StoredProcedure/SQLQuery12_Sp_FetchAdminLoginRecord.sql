

CREATE   PROCEDURE [dbo].[FetchAdminLoginRecord]
	-- Add the parameters for the stored procedure here
	@AdminID nvarchar(100),
	@Password nvarchar(MAX)
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @result int = 0;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if((select count(AdminID) from Admin where AdminID = @AdminID) = 0)
		begin;
		set @result = 2;
		THROW 52000, 'AdminID is invalid', -1;
		end
	if((select count(AdminID) from Admin where AdminID = @AdminID and Password = @Password) = 0)
	begin;
		set @result = 3;
		THROW 52000, 'wrong password', -1;
	end
	else
	begin
	select AdminID,
	AdminName,
	Email,
	PhoneNumber
	from Admin where AdminID = @AdminID;
		set @result = 1;
	end
	
COMMIT TRANSACTION
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
