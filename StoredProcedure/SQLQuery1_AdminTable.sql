---------------------------------Create Databse for Book Store-------------
Create database BookStoreDB


----------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID]  AS (CONVERT([nvarchar],'A_')+CONVERT([nvarchar](100),[AdminIdentity])) PERSISTED NOT NULL,
	[AdminIdentity] [bigint] IDENTITY(1,1) NOT NULL,
	[AdminName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [bigint] NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [Admin_AdminID_PK] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Admin_AdminIdentity_U] UNIQUE NONCLUSTERED 
(
	[AdminIdentity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Admin_Email_U] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

