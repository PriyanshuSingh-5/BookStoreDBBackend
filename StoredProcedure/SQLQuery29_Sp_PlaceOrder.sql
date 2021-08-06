


CREATE   PROCEDURE [dbo].[PlaceOrder] 
	-- Add the parameters for the stored procedure here
	@CustomerID varchar(100),
	@AddressID bigint
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @TotalCost int = 0;
	DECLARE @OrderID bigint = 0;
	DECLARE @result int = 0;
	DECLARE @BookQuantity int = 0;
	
	if((select count(*) from cart where CustomerID = @CustomerID) = 0)
	begin
	set @result = 2;
	THROW 50009, 'cart is empty', -1;
	end

	set @TotalCost = (select sum(TotalCost) from Cart where Cart.CustomerID = @CustomerID);

	if(@TotalCost > 0)
	begin
		insert into CustomerOrder(CustomerID, TotalCost) output Inserted.OrderID into @Identity
		values(@CustomerID, @TotalCost);

		set @OrderID = (select ID from @Identity);

		insert into OrderBook(OrderID, CustomerID, BookQuantity, BookID) 
		select @OrderID, @CustomerID, BookQuantity, Cart.BookID from 
		Cart inner join Book on Cart.BookID = Book.BookID
		where Cart.CustomerID = @CustomerID;

		insert into OrderAddress(CustomerID, OrderID, Name, Pincode, Locality, Address,
		City, Landmark, AddressType) select @CustomerID, @OrderID,Name, Pincode, 
		Locality, Address, City, Landmark, AddressType  from CustomerAddress
		where CustomerAddressID = @AddressID;

		UPDATE Book SET BookQuantity = BookQuantity - 1 WHERE BookID in
		(select BookID from Cart where CustomerID = @CustomerID)

		delete from cart where CustomerID = @CustomerID;
		
	end

	select CustomerOrder.OrderID, Email, OrderDate, CustomerOrder.CustomerID, TotalCost, 
	OrderAddressID, Name, Pincode, Locality, Address, PhoneNumber,
		City, Landmark, AddressType
	from CustomerOrder inner join Customer
	on Customer.CustomerID = CustomerOrder.CustomerID
	inner join OrderAddress on OrderAddress.OrderID = CustomerOrder.OrderID
	where CustomerOrder.CustomerID = @CustomerID and CustomerOrder.OrderID = @OrderID;

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
