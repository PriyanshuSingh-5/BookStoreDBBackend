

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderAddress](
	[OrderAddressID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [nvarchar](100) NULL,
	[OrderID] [bigint] NULL,
	[Name] [nvarchar](50) NULL,
	[Pincode] [int] NULL,
	[Locality] [nvarchar](50) NULL,
	[Address] [nvarchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[Landmark] [nvarchar](50) NULL,
	[AddressType] [nvarchar](50) NULL,
 CONSTRAINT [OrderAddress_ID_PK] PRIMARY KEY CLUSTERED 
(
	[OrderAddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderAddress]  WITH CHECK ADD  CONSTRAINT [OrderAddress_CustomerID_FK] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderAddress] CHECK CONSTRAINT [OrderAddress_CustomerID_FK]
GO
ALTER TABLE [dbo].[OrderAddress]  WITH CHECK ADD  CONSTRAINT [OrderAddress_OrderID_FK] FOREIGN KEY([OrderID])
REFERENCES [dbo].[CustomerOrder] ([OrderID])
GO
ALTER TABLE [dbo].[OrderAddress] CHECK CONSTRAINT [OrderAddress_OrderID_FK]
GO
