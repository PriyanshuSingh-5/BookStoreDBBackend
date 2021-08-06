


CREATE   PROCEDURE [dbo].[CheckCustomerRecord]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		DECLARE @result int = 0;

    -- Insert statements for procedure here
	if((select count(Email) from Customer where Email = @Email) = 1)
		begin;
		set @result = 1;
		end
return @result;

END
GO
