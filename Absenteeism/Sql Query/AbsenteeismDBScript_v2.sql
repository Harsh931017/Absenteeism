
/****** Object:  Table [dbo].[tblHoliday]    Script Date: 9/10/2024 3:42:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHoliday](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[Name] [varchar](max) NULL,
	[Disable] [int] NULL,
 CONSTRAINT [PK_tblHoliday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblHoliday] ON 
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (1, CAST(N'2024-01-17' AS Date), N'Guru Gobind Singh Jayanti', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (2, CAST(N'2024-01-26' AS Date), N'Republic Day', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (3, CAST(N'2024-03-08' AS Date), N'Mahashivratri', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (4, CAST(N'2024-03-25' AS Date), N'Holi', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (5, CAST(N'2024-04-11' AS Date), N'Id-Ul-Fitar', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (6, CAST(N'2024-08-15' AS Date), N'Independence Day', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (7, CAST(N'2024-10-02' AS Date), N'Mahatma Gandhi Jaynanti', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (8, CAST(N'2024-10-31' AS Date), N'Diwali', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (9, CAST(N'2024-11-01' AS Date), N'Govardhan Pooja', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (10, CAST(N'2024-11-15' AS Date), N'Guru Nanak Jyanti', 0)
GO
INSERT [dbo].[tblHoliday] ([Id], [Date], [Name], [Disable]) VALUES (11, CAST(N'2024-12-25' AS Date), N'Christmas', 0)
GO
SET IDENTITY_INSERT [dbo].[tblHoliday] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAllocation]    Script Date: 9/10/2024 3:42:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
ALTER PROCEDURE [dbo].[sp_CheckAllocation] --'QWERT12@34'       
(        
@Id varchar(Max)=null        
)        
AS        
BEGIN        
Declare @Result bit=0;        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
        
 if exists(select 1 from tblAllocation WITH (NOLOCK) where Id=@Id)        
 begin        
 set @Result=1        
 end        
           
 SELECT @Result       
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailabilityById]    Script Date: 9/10/2024 3:42:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_GettblAvailabilityById]  
 (  
 @Id varchar(max)=null  
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select ta.Id,ta.Date,ta.DepartmentId as DepartmentId,td.Name as Department,  
 ta.ShiftId as ShiftId,ts1.Name as Shift,ta.StatusId as StatusId,ts2.Status as Status,  
 ta.StationId as StationId,ts.Name as Station,ta.PlantId as PlantId,tp.Name as Plant,  
 ta.UserId as UserId,tu.Name as Employee  
 from tblAvailability ta WITH (NOLOCK)  
 INNER JOIN tblDepartment td WITH (NOLOCK) on td.Id=ta.DepartmentId  
 INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=ta.StationId  
 INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=ta.ShiftId  
 INNER JOIN tblStatus ts2 WITH(NOLOCK) ON ta.StatusId=ts2.Id  
 INNER JOIN tblPlant tp WITH(NOLOCK) ON tp.Id=ta.PlantId  
 INNER JOIN tblUser tu WITH(NOLOCK) ON ta.UserId=tu.Id  
 where (@Id is null or ta.Id=@Id)  
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAvailability_v2]    Script Date: 9/10/2024 3:42:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_UpdateAvailability_v2] --1,1
(  
@UserId varchar(max)=null,
@StatusId varchar(max)=null
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 DECLARE @Tomorrow DATE = DATEADD(DAY, 1, Getdate());
DECLARE @NextBusinessDate DATE;


 WHILE EXISTS (
    SELECT 1
    FROM tblHoliday
    WHERE Date = @Tomorrow
) OR DATENAME(WEEKDAY, @Tomorrow) = 'Sunday'
BEGIN
    -- If it's a Sunday or a holiday, increment the day
    SET @Tomorrow = DATEADD(DAY, 1, @Tomorrow);
END

-- Set the next business day
SET @NextBusinessDate = @Tomorrow;


  UPDATE tblAvailability  
SET   
	StatusId=@StatusId
WHERE   
    UserId = @UserId and DATE=@NextBusinessDate
  
 select Top(1) * from tblAvailability WITH (NOLOCK) WHERE   
    UserId = 1 and DATE=@NextBusinessDate
   
END 
GO
