

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderBook](
	[OrderBookID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NULL,
	[BookID] [bigint] NULL,
	[BookQuantity] [int] NULL,
	[CustomerID] [nvarchar](100) NULL,
	[PhoneNumner] [bigint] NULL,
 CONSTRAINT [OrderBook_ID_PK] PRIMARY KEY CLUSTERED 
(
	[OrderBookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderBook]  WITH CHECK ADD  CONSTRAINT [OrderBook_BookID_FK] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([BookID])
GO
ALTER TABLE [dbo].[OrderBook] CHECK CONSTRAINT [OrderBook_BookID_FK]
GO
ALTER TABLE [dbo].[OrderBook]  WITH CHECK ADD  CONSTRAINT [OrderBook_CustomerID_FK] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderBook] CHECK CONSTRAINT [OrderBook_CustomerID_FK]
GO
ALTER TABLE [dbo].[OrderBook]  WITH CHECK ADD  CONSTRAINT [OrderBook_OrderID_FK] FOREIGN KEY([OrderID])
REFERENCES [dbo].[CustomerOrder] ([OrderID])
GO
ALTER TABLE [dbo].[OrderBook] CHECK CONSTRAINT [OrderBook_OrderID_FK]
GO
