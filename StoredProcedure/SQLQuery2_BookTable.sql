
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Book(
	[BookID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookName] [nvarchar](50) NOT NULL,
	[BookDiscription] [nvarchar](max) NOT NULL,
	[BookImage] [nvarchar](max) NULL,
	[BookPrice] [int] NOT NULL,
	[AuthorID] [bigint] NULL,
	[BookQuantity] [int] NULL,
	[InStock]  AS (CONVERT([bit],case when [BookQuantity]=(0) then (0) else (1) end)),
	[IsDelete] [bit] NULL,
 CONSTRAINT [Book_BookID_PK] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

---------------
ALTER TABLE [dbo].[Book] ADD  DEFAULT ((0)) FOR [BookQuantity]
GO
--------------------
ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [Book_IsDelete_D]  DEFAULT ((0)) FOR [IsDelete]
GO
-------------------
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [Book_AuthorID_FK] FOREIGN KEY([AuthorID])
REFERENCES [dbo].[Author] ([AuthorID])
ON DELETE CASCADE
GO
--------------------------
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [Book_AuthorID_FK]
GO
----------------------
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [Book_BookQuantity_C] CHECK  (([BookQuantity]>=(0)))
GO
--------------------------
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [Book_BookQuantity_C]
GO
-----------------------

select *from Book;
