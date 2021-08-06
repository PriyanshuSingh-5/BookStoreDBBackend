

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [nvarchar](100) NULL,
	[BookID] [bigint] NULL,
	[BookCount] [int] NULL,
	[TotalCost] [int] NULL,
 CONSTRAINT [CartID_PK] PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

------------------
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [Cart_BookCount_D]  DEFAULT ((1)) FOR [BookCount]
GO
-----------------------------------
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [Cart_CustomerID_FK] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
ON DELETE CASCADE
GO
---------------------------------------------

ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [Cart_CustomerID_FK]
GO
----------------------------------

ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [CustomerCart_BookID_FK] FOREIGN KEY([BookID])
REFERENCES [dbo].[Book] ([BookID])
ON DELETE CASCADE
GO
-----------------------------------------
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [CustomerCart_BookID_FK]
GO
-------------------------------------

ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [Cart_BookCount_C] CHECK  (([BookCount]>=(1)))
GO

--------------------------------
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [Cart_BookCount_C]
GO

-----------------------------------

select *from Cart;

