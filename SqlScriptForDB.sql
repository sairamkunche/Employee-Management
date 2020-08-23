USE [EmployeeManagement]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 23-08-2020 19:17:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Skill] [varchar](50) NULL,
	[YearsExperience] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Skill], [YearsExperience]) VALUES (1, N'sairam', N'34343434', N'c#', 0)
INSERT [dbo].[Employees] ([EmployeeID], [EmployeeName], [PhoneNumber], [Skill], [YearsExperience]) VALUES (3, N'venkat', N'9489808790', N'java', 3)
SET IDENTITY_INSERT [dbo].[Employees] OFF
