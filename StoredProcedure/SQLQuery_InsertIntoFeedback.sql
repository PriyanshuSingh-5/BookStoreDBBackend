create Proc sp_ReviewBackToBook
@CustomerIdentity bigint,
@BookID bigint,
@Review int,
@Feedback varchar(30)
AS
BEGIN
Insert into Review (CustomerIdentity ,BookID,Review,Feedback) values (@CustomerIdentity,@BookID,@Review,@Feedback)
	
	END