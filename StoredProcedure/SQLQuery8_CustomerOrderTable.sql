

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerOrder](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NULL,
	[CustomerID] [nvarchar](100) NULL,
	[TotalCost] [int] NOT NULL,
 CONSTRAINT [CustomerOrder_OrderID_PK] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

----------------------------------
ALTER TABLE [dbo].[CustomerOrder] ADD  CONSTRAINT [CustomerOrder_OrderDate_D]  DEFAULT (getdate()) FOR [OrderDate]
GO
---------------------------------------
ALTER TABLE [dbo].[CustomerOrder]  WITH CHECK ADD  CONSTRAINT [CustomerOrder_CustomerID_FK] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON DELETE CASCADE
GO
--------------------------------
ALTER TABLE [dbo].[CustomerOrder] CHECK CONSTRAINT [CustomerOrder_CustomerID_FK]
GO

-----------------------

