CREATE TABLE Review (
    CustomerIdentity bigint,
    BookId bigint ,
	ReviewId INT PRIMARY KEY IDENTITY(1,1),
	Review int default 0,
	Feedback varchar(30)
    FOREIGN KEY (BookID) REFERENCES Book(BookID),
	FOREIGN KEY (CustomerIdentity) REFERENCES Customer(CustomerIdentity)

);