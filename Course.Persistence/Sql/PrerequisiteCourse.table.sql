USE [Course]
GO

/****** Object:  Table [dbo].[PrerequisiteCourse]    Script Date: 11/9/2015 10:34:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PrerequisiteCourse](
	[Id_Course] [int] NOT NULL,
	[Id_Prereq_Course] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Course] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PrerequisiteCourse]  WITH CHECK ADD FOREIGN KEY([Id_Course])
REFERENCES [dbo].[Course] ([CourseID])
GO

ALTER TABLE [dbo].[PrerequisiteCourse]  WITH CHECK ADD FOREIGN KEY([Id_Prereq_Course])
REFERENCES [dbo].[Course] ([CourseID])
GO

