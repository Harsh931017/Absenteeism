USE [master]
GO
/****** Object:  Database [dbAbsenteeism]    Script Date: 9/9/2024 4:00:48 PM ******/
CREATE DATABASE [dbAbsenteeism]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbAbsenteeism', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbAbsenteeism.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbAbsenteeism_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER01\MSSQL\DATA\dbAbsenteeism_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [dbAbsenteeism] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbAbsenteeism].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbAbsenteeism] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbAbsenteeism] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbAbsenteeism] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbAbsenteeism] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbAbsenteeism] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET RECOVERY FULL 
GO
ALTER DATABASE [dbAbsenteeism] SET  MULTI_USER 
GO
ALTER DATABASE [dbAbsenteeism] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbAbsenteeism] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbAbsenteeism] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbAbsenteeism] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbAbsenteeism] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbAbsenteeism] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'dbAbsenteeism', N'ON'
GO
ALTER DATABASE [dbAbsenteeism] SET QUERY_STORE = ON
GO
ALTER DATABASE [dbAbsenteeism] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [dbAbsenteeism]
GO
/****** Object:  Table [dbo].[tblAllocation]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAllocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CellMemberId] [int] NULL,
	[DepartmentId] [int] NULL,
	[StationId] [int] NULL,
	[ShiftId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_tblAllocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAvailability]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAvailability](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[DepartmentId] [int] NULL,
	[StationId] [int] NULL,
	[ShiftId] [int] NULL,
	[StatusId] [int] NULL,
	[PlantId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Availability] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCellMemberStatus--NoUSE]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCellMemberStatus--NoUSE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[StationId] [int] NULL,
	[EmployeeId] [int] NULL,
	[ShiftId] [int] NULL,
	[StatusId] [int] NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_CellMemberStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDefect]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDefect](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DefectName] [varchar](max) NULL,
	[Occurrance] [int] NULL,
	[ShiftId] [int] NULL,
	[Date] [date] NULL,
	[PlantId] [int] NULL,
	[StationId] [int] NULL,
	[Time] [varchar](max) NULL,
 CONSTRAINT [PK_tblDefect] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDepartment]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDepartment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblForgotPassword]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblForgotPassword](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Token] [varchar](50) NULL,
	[IsUsed] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblForgotPassword] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLogin]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserNumber] [varchar](50) NULL,
	[PasswordHash] [varchar](200) NULL,
	[PasswordSalt] [varchar](200) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPlant]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPlant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](max) NULL,
 CONSTRAINT [PK_tblPlant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRoles]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblShift]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShift](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Timing] [varchar](max) NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStation]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DepartmentId] [int] NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStatus]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](max) NULL,
 CONSTRAINT [PK_tblStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[RoleId] [int] NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWorkingHistory--NoUSE]    Script Date: 9/9/2024 4:00:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWorkingHistory--NoUSE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[DepartmentId] [int] NULL,
	[StationId] [int] NULL,
	[ShiftId] [int] NULL,
	[PlantId] [int] NULL,
 CONSTRAINT [PK_WorkingHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblAllocation] ON 
GO
INSERT [dbo].[tblAllocation] ([Id], [CellMemberId], [DepartmentId], [StationId], [ShiftId], [CreatedBy], [CreatedDate], [PlantId]) VALUES (1, 1, 1, 1, 1, 3, CAST(N'2024-09-09T09:49:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[tblAllocation] OFF
GO
SET IDENTITY_INSERT [dbo].[tblAvailability] ON 
GO
INSERT [dbo].[tblAvailability] ([Id], [Date], [DepartmentId], [StationId], [ShiftId], [StatusId], [PlantId], [UserId]) VALUES (1, CAST(N'2024-09-09T09:38:00.000' AS DateTime), 1, 1, 1, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[tblAvailability] OFF
GO
SET IDENTITY_INSERT [dbo].[tblDefect] ON 
GO
INSERT [dbo].[tblDefect] ([Id], [DefectName], [Occurrance], [ShiftId], [Date], [PlantId], [StationId], [Time]) VALUES (1, N'DefectTest1', 43, 1, CAST(N'2024-08-01' AS Date), 1, 1, N'3:00:00 PM')
GO
INSERT [dbo].[tblDefect] ([Id], [DefectName], [Occurrance], [ShiftId], [Date], [PlantId], [StationId], [Time]) VALUES (2, N'DefectTest2', 21, 2, CAST(N'2024-08-03' AS Date), 1, 1, N'2:10:00 PM')
GO
SET IDENTITY_INSERT [dbo].[tblDefect] OFF
GO
SET IDENTITY_INSERT [dbo].[tblDepartment] ON 
GO
INSERT [dbo].[tblDepartment] ([Id], [Name], [PlantId]) VALUES (1, N'Assembly', 1)
GO
INSERT [dbo].[tblDepartment] ([Id], [Name], [PlantId]) VALUES (2, N'Assembly--2', 1)
GO
INSERT [dbo].[tblDepartment] ([Id], [Name], [PlantId]) VALUES (3, N'Assembly--3', 1)
GO
INSERT [dbo].[tblDepartment] ([Id], [Name], [PlantId]) VALUES (4, N'Assembly--4', 1)
GO
SET IDENTITY_INSERT [dbo].[tblDepartment] OFF
GO
SET IDENTITY_INSERT [dbo].[tblLogin] ON 
GO
INSERT [dbo].[tblLogin] ([Id], [UserNumber], [PasswordHash], [PasswordSalt], [UserId]) VALUES (1, N'QWERT1234', N'MHcw/898oFwrN3iuRvtCMUsKTz/9ePqPNZzuMIDm66EOlS7rLMkGTkql1plDlGkoqE1rTEGe6CdlnmwMbyqm4A==', N'oNN6o82I/FMYCVyX2Koe14i8m0caRH6X70Acs968Q8/vRZLb+7pI736nV6sj0SGDz7DQRACtO/2wl95AwS+G/FFkld5Mmp/1nWkhXz9aqru9coHBMx82vOJ4EfGm2KWgSRV4prZSjWfwxuJ5rlN2Mfz0Hunov+bBBu1DmCsxzL8=', 1)
GO
INSERT [dbo].[tblLogin] ([Id], [UserNumber], [PasswordHash], [PasswordSalt], [UserId]) VALUES (2, N'QWERT12', N'5E/vPhAW8MHqdO1nuYfYQsti8oP4zF+JouSOkbdXKP6ZLfSYdGJSzGkixB7ehYF8lG5t53BUdzl18z9mslXSMg==', N'C0TKwpzTH9UGXrL+gdAWzsDrZv3yH7mGNZi+dLKFNY7ZDcNQJlHXih9lZIfrRYF0Iyc6SrtoJHGj1iVIi1DL+D6eUqjvzk4V2CO26rDqqwAQduUXFlTE39yIPY44BBJA0fpjkL7PCWpcPCnJg3zI6MX6TDoV8RrzI8qc4SH+bds=', 2)
GO
INSERT [dbo].[tblLogin] ([Id], [UserNumber], [PasswordHash], [PasswordSalt], [UserId]) VALUES (3, N'QWERT123', N'xk8LG+xldkBVu0GgeU+xp0D7AJ4k2kZffs2fJiGEDaoCbdrh/uzFii1bcLEqb102fZMwEazAzMcOrh1bwpgiUQ==', N'czP4hyWv0Umua692iky7fywlMlhZ8jqUceq6kwOyWvzX4iOfSji+Z4gBL9LMwo/7SOU5IQmYuHix8VbLNdLLDsvbnsZZr8uOvYGnPjCpBASt0yjESal81yZTZaHZOh1zc6oV6NRj+3Dw0sUj5TXND1DhXBNekJopc5f9gn9HOpw=', 3)
GO
SET IDENTITY_INSERT [dbo].[tblLogin] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPlant] ON 
GO
INSERT [dbo].[tblPlant] ([Id], [Code], [Name]) VALUES (1, N'FS01', N'Plant 1')
GO
INSERT [dbo].[tblPlant] ([Id], [Code], [Name]) VALUES (2, N'FS02', N'Plant 2')
GO
INSERT [dbo].[tblPlant] ([Id], [Code], [Name]) VALUES (3, N'FS05', N'Plant 3')
GO
SET IDENTITY_INSERT [dbo].[tblPlant] OFF
GO
SET IDENTITY_INSERT [dbo].[tblRoles] ON 
GO
INSERT [dbo].[tblRoles] ([Id], [Name], [PlantId]) VALUES (1, N'Cell Member', 1)
GO
INSERT [dbo].[tblRoles] ([Id], [Name], [PlantId]) VALUES (2, N'Supervisior', 1)
GO
SET IDENTITY_INSERT [dbo].[tblRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[tblShift] ON 
GO
INSERT [dbo].[tblShift] ([Id], [Name], [Timing], [PlantId]) VALUES (1, N'A', N'6:00 am to 2:00 pm', 1)
GO
INSERT [dbo].[tblShift] ([Id], [Name], [Timing], [PlantId]) VALUES (2, N'B', N'2:30 pm to 10:00 pm', 1)
GO
SET IDENTITY_INSERT [dbo].[tblShift] OFF
GO
SET IDENTITY_INSERT [dbo].[tblStation] ON 
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (1, N'QP1', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (2, N'QP2', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (3, N'QP3', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (4, N'QP4', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (5, N'QP5', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (6, N'QP6', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (7, N'QP7', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (8, N'QP8', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (9, N'QP9', 1, 1)
GO
INSERT [dbo].[tblStation] ([Id], [Name], [DepartmentId], [PlantId]) VALUES (10, N'QP10', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[tblStation] OFF
GO
SET IDENTITY_INSERT [dbo].[tblStatus] ON 
GO
INSERT [dbo].[tblStatus] ([Id], [Status]) VALUES (1, N'Present')
GO
INSERT [dbo].[tblStatus] ([Id], [Status]) VALUES (2, N'Absent')
GO
SET IDENTITY_INSERT [dbo].[tblStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUser] ON 
GO
INSERT [dbo].[tblUser] ([Id], [Name], [Email], [RoleId], [PlantId]) VALUES (1, N'QWERT1234', N'harsh@gmail.com', 1, 1)
GO
INSERT [dbo].[tblUser] ([Id], [Name], [Email], [RoleId], [PlantId]) VALUES (2, N'QWERT12', N'harsh1@gmail.com', 1, 1)
GO
INSERT [dbo].[tblUser] ([Id], [Name], [Email], [RoleId], [PlantId]) VALUES (3, N'QWERT123', N'harsh2@gmail.com', 2, 1)
GO
SET IDENTITY_INSERT [dbo].[tblUser] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAllocation]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
CREATE PROCEDURE [dbo].[sp_CheckAllocation] --'QWERT12@34'       
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
/****** Object:  StoredProcedure [dbo].[sp_CheckAvailability]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckAvailability] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblAvailability WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckCellMember]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE [dbo].[sp_CheckCellMember] --'QWERT12@34'     
(      
@Id varchar(Max)=null      
)      
AS      
BEGIN      
Declare @Result bit=0;      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
 if exists(select 1 from tbluser WITH (NOLOCK) where Id=@Id and roleId=1)      
 begin      
 set @Result=1      
 end      
         
 SELECT @Result     
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckDefect]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckDefect] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblDefect WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckDepartment]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckDepartment] --'QWERT12@34'   
(    
@DepartmentId varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblDepartment WITH (NOLOCK) where Id=@DepartmentId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckPlant]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
CREATE PROCEDURE [dbo].[sp_CheckPlant] --'QWERT12@34'       
(        
@Id varchar(Max)=null        
)        
AS        
BEGIN        
Declare @Result bit=0;        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
        
 if exists(select 1 from tblPlant WITH (NOLOCK) where Id=@Id)        
 begin        
 set @Result=1        
 end        
           
 SELECT @Result       
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckRole]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_CheckRole] --'QWERT12@34' 
(  
@RoleId varchar(Max)=null  
)  
AS  
BEGIN  
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblRoles WITH (NOLOCK) where Id=@RoleId)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckShift]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckShift] --'QWERT12@34'   
(    
@ShiftId varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblShift WITH (NOLOCK) where Id=@ShiftId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStation]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckStation] --'QWERT12@34'   
(    
@StationId varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblStation WITH (NOLOCK) where Id=@StationId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStatus]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckStatus] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblStatus WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckTokenValidity]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_CheckTokenValidity]   
(  
@UserId varchar(max)=null,  
@Token varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 select * from tblForgotPassword WITH (NOLOCK)  
 where UserId=@UserId and Token=@Token and IsUsed=0 AND CreatedDate >= DATEADD(MINUTE, -15, GETDATE())   
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUser]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE [dbo].[sp_CheckUser] --'QWERT12@34'     
(      
@Id varchar(Max)=null      
)      
AS      
BEGIN      
Declare @Result bit=0;      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
 if exists(select 1 from tblUser WITH (NOLOCK) where Id=@Id)      
 begin      
 set @Result=1      
 end      
         
 SELECT @Result     
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUserEmail]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_CheckUserEmail] --'QWERT12@34' 
(  
@UserEmail varchar(Max)=null  
)  
AS  
BEGIN  
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblUser WITH (NOLOCK) where Email=@UserEmail)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUserNumber]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_CheckUserNumber] --'QWERT12@34' 
(  
@UserNumber varchar(Max)=null  
)  
AS  
BEGIN  
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblLogin WITH (NOLOCK) where UserNumber=@UserNumber)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckWorkHistory]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_CheckWorkHistory] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN    
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblWorkingHistory WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateDefect]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateDefect]
(
@Date varchar(max)=null,
@DefectName varchar(max)=null,
@Occurrance varchar(max)=null,
@ShiftId varchar(max)=null,
@PlantId varchar(max)=null,
@StationId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblDefect(Date,DefectName,Occurrance,ShiftId,PlantId,StationId) 
	OUTPUT INSERTED.*
	values(@Date,@DefectName,@Occurrance,@ShiftId,@PlantId,@StationId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblAllocation]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_CreatetblAllocation]  
(  
@CellMemberId varchar(max)=null,  
@DepartmentId varchar(max)=null,  
@StationId varchar(max)=null,  
@ShiftId varchar(max)=null,  
@CreatedBy varchar(max)=null,  
@CreatedDate varchar(max)=null,
@PlantId varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    insert into tblAllocation(CellMemberId,DepartmentId,StationId,ShiftId,CreatedBy,CreatedDate,PlantId)   
 OUTPUT INSERTED.*  
 values(@CellMemberId,@DepartmentId,@StationId,@ShiftId,@CreatedBy,@CreatedDate,@PlantId)  
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblAvailability]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreatetblAvailability]
(
@Date varchar(max)=null,
@DepartmentId varchar(max)=null,
@StationId varchar(max)=null,
@ShiftId varchar(max)=null,
@StatusId varchar(max)=null,
@PlantId varchar(max)=null,
@UserId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblAvailability(Date,DepartmentId,StationId,ShiftId,StatusId,PlantId,UserId) 
	OUTPUT INSERTED.*
	values(@Date,@DepartmentId,@StationId,@ShiftId,@StatusId,@PlantId,@UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblCellMemberStatus]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreatetblCellMemberStatus]
(
@Date varchar(max)=null,
@EmployeeId varchar(max)=null,
@StationId varchar(max)=null,
@ShiftId varchar(max)=null,
@StatusId varchar(max)=null,
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblCellMemberStatus(Date,EmployeeId,StationId,ShiftId,StatusId,PlantId) 
	OUTPUT INSERTED.*
	values(@Date,@EmployeeId,@StationId,@ShiftId,@StatusId,@PlantId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateWorkHistory]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateWorkHistory] 
(
@Date varchar(max)=null,
@DepartmentId varchar(max)=null,
@StationId varchar(max)=null,
@ShiftId varchar(max)=null,
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblWorkingHistory(Date,DepartmentId,StationId,ShiftId,PlantId) 
	OUTPUT INSERTED.*
	values(@Date,@DepartmentId,@StationId,@ShiftId,@PlantId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblAllocation]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_DeletetblAllocation]     
(    
@Id varchar(max)=null    
)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
 Delete from tblAllocation where Id=@Id    
      
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblAvailability]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_DeletetblAvailability]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblAvailability where Id=@Id  
    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblCellMemberStatus]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_DeletetblCellMemberStatus]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblCellMemberStatus where Id=@Id  
    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblDefect]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_DeletetblDefect]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblDefect where Id=@Id  
    
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteWorkHistory]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DeleteWorkHistory] 
(
@Id varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Delete from tblWorkingHistory where Id=@Id
  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCellMember]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetCellMember]
(
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select tu.Id,tu.Name, tp.Name as Plant,tp.Id as PlantId,tu.Email,tu.RoleId as RoleId,tr.Name as Role
from tblUser tu  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on tu.PlantId=tp.Id
INNER JOIN tblRoles tr WITH (NOLOCK) on tu.RoleId=tr.Id
where @PlantId is null or tu.PlantId=@PlantId and tr.Id=1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDefectById]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
CREATE PROCEDURE [dbo].[sp_GetDefectById]      
 (       
 @Id varchar(max)=null    
 )      
AS      
BEGIN      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
    select td1.Id as Id,td1.DefectName as DefectName,td1.Occurrance as Occurrance,td1.Date as Date,  
 tb.Id as ShiftId,tb.Name as Shift,tp.Id as PlantId,tp.Name as Plant,td1.StationId as StationId,
 ts.Name as Station
 from tblDefect td1 WITH (NOLOCK)     
 INNER JOIN tblshift tb WITH (NOLOCK) on tb.Id=td1.ShiftId  
 INNER JOIN tblPlant tp  WITH (NOLOCK) on tp.Id=td1.PlantId  
 INNER JOIN tblStation ts WITH (NOLOCK) on ts.Id=td1.StationId
 where (@Id is null or td1.Id=@Id)    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDepartments]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetDepartments]
(
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select td.Id,td.Name, tp.Name as Plant,tp.Id as PlantId
from tblDepartment td  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on td.PlantId=tp.Id
where @PlantId is null or td.PlantId=@PlantId 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRoles]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetRoles] 
(
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select tr.Id as RoleId,tr.Name as Role,tp.Id as PlantId,tp.Name as Plant from tblRoles tr  WITH (NOLOCK)
INNER JOIN tblplant tp on tr.PlantId=tp.Id
where (@PlantId is null or tr.PlantId=@PlantId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetShift]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetShift] 
(
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ts.Id,ts.Name, tp.Name as Plant,tp.Id as PlantId,ts.Timing as Timing
from tblShift ts  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on ts.PlantId=tp.Id
where @PlantId is null or ts.PlantId=@PlantId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStationByDepartmentId]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetStationByDepartmentId]
(
@PlantId varchar(max)=null,
@DepartmentId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ts.Id,ts.Name, tp.Name as Plant,tp.Id as PlantId,ts.DepartmentId,td.Name as Department
from tblStation ts  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on ts.PlantId=tp.Id
INNER JOIN tblDepartment td WITH (NOLOCK) on ts.DepartmentId=td.Id
where (@PlantId is null or ts.PlantId=@PlantId ) and (@DepartmentId is null or ts.DepartmentId=@DepartmentId )
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocationById]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_GettblAllocationById]  
 (  
 @Id varchar(max)=null  
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select  ta.Id,ta.CellMemberId,tu.Name as CellMember, ta.DepartmentId,td.Name as Department,
	ta.StationId,ts.Name as Station,ta.ShiftId,ts1.Name as Shift,ta.CreatedBy,ta.CreatedDate,ta.PlantId,
	tp.Name as Plant,tu1.Name as CreatedByName
 from tblAllocation ta WITH (NOLOCK)  
 INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=ta.StationId  
 INNER JOIN tblUser tu  WITH (NOLOCK) on ta.CellMemberId=tu.Id  
 INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=ta.ShiftId  
 INNER JOIN tblPlant tp  WITH (NOLOCK) on tp.Id=ta.PlantId  
 INNER JOIN tblDepartment td WITH(NOLOCK) on ta.DepartmentId=td.Id
 INNER JOIN tblUser tu1 WITH(NOLOCK) ON ta.CreatedBy=tu1.Id
 where (@Id is null or ta.Id=@Id)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocations]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GettblAllocations]  
 (    
 @PlantId varchar(max)=null  
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select ta.Id,ta.CellMemberId,tu.Name as CellMember,ta.DepartmentId,td.Name as Department,
	ta.StationId,ts.Name as Station,ta.SHiftId,ts1.Name as Shift,ta.CreatedBy,tu1.Name as CreatedByName,
	ta.CreatedDate,ta.PlantId,tp.Name as Plant
 from tblAllocation ta WITH (NOLOCK)  
INNER JOIN tblUser tu WITH (NOLOCK) on ta.CellMemberId=tu.Id 
INNER JOIN tblDepartment td WITH (NOLOCK) ON ta.DepartmentId=td.Id
INNER JOIN tblStation ts WITH (NOLOCK) on ta.StationId=ts.Id
INNER JOIN tblShift ts1 WITH (NOLOCK) on ta.ShiftId=ts1.Id
INNER JOIN tblUser tu1 WITH(NOLOCK) ON ta.CreatedBy=tu1.Id
INNER JOIN tblPlant tp WITH (NOLOCK) ON ta.PlantId=tp.Id
 where  
  (@PlantId is null or tp.Id=@plantId)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailability]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GettblAvailability]
	(
	@StartDate varchar(max)=null,
	@EndDate varchar(max)=null,
	@PlantId varchar(max)=null,
	@UserId varchar(max)=null
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select ta.Id,ta.Date,ta.DepartmentId as DepartmentId,td.Name as Department,
	ta.ShiftId as ShiftId,ts1.Name as Shift,ta.StatusId as StatusId,ts2.Status as Status,
	ta.StationId as StationId,ts.Name as Station,TP.Id as PlantId,tp.Name as Plant,
	ta.UserId,tu.Name as Employee
	from tblAvailability ta WITH (NOLOCK)
	INNER JOIN tblDepartment td WITH (NOLOCK) on td.Id=ta.DepartmentId
	INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=ta.StationId
	INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=ta.ShiftId
	INNER JOIN tblStatus ts2 WITH(NOLOCK) ON ts2.Id=ta.StatusId
	INNER JOIN tblPlant tp WITH(NOLOCK) on tp.Id=ta.PlantId
	INNER JOIN tblUser tu WITH(NOLOCK) on ta.UserId=tu.Id
	where ((@StartDate is null and @EndDate is null) or (CAST(Date AS DATE) between CAST(@StartDate as date) and Cast(@EndDate as date)))
	 and (@PlantId is null or tp.Id=@PlantId)
	 and (@UserId is null or tu.Id=@UserId)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailabilityById]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GettblAvailabilityById]
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
	INNER JOIN tblStatus ts2 WITH(NOLOCK) ON ts2.Id=ts2.Id
	INNER JOIN tblPlant tp WITH(NOLOCK) ON tp.Id=ta.PlantId
	INNER JOIN tblUser tu WITH(NOLOCK) ON ta.UserId=tu.Id
	where (@Id is null or ta.Id=@Id)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblCellMemberStatus]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_GettblCellMemberStatus]  
 (  
 @StartDate varchar(max)=null,  
 @EndDate varchar(max)=null,  
 @ShiftId varchar(max)=null,  
 @PlantId varchar(max)=null  
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select tcms.Id,tcms.Date,tcms.StationId,ts.Name as Station,  
 tcms.UserId as EmployeeId,tu.Name as Employee,tcms.ShiftId,ts1.Name as Shift,  
 tcms.StatusId,ts2.Status,tp.Id as PlantId,tp.Name as Plant  
 from tblAvailability tcms WITH (NOLOCK)  
 INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=tcms.StationId  
 INNER JOIN tblUser tu  WITH (NOLOCK) on tcms.UserId=tu.Id  
 INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=tcms.ShiftId  
 INNER JOIN tblStatus ts2  WITH (NOLOCK) on ts2.Id=tcms.StatusId  
 INNER JOIN tblPlant tp  WITH (NOLOCK) on tp.Id=tcms.PlantId  
 where ((@StartDate is null and @EndDate is null) or (CAST(Date AS DATE) between CAST(@StartDate as date) and Cast(@EndDate as date)))  
 and (@ShiftId is null or ShiftId=@ShiftId)  
 and (@PlantId is null or tp.Id=@plantId)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblCellMemberStatusById]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GettblCellMemberStatusById]
	(
	@Id varchar(max)=null
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select tcms.Id,tcms.Date,tcms.StationId,ts.Name as Station,
	tcms.EmployeeId,tu.Name as Employee,tcms.ShiftId,ts1.Name as Shift,
	tcms.StatusId,ts2.Status,tp.Id as PlantId,tp.Name as Plant
	from tblCellMemberStatus tcms WITH (NOLOCK)
	INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=tcms.StationId
	INNER JOIN tblUser tu  WITH (NOLOCK) on tcms.EmployeeId=tu.Id
	INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=tcms.ShiftId
	INNER JOIN tblStatus ts2  WITH (NOLOCK) on ts2.Id=tcms.StatusId
	INNER JOIN tblPlant tp  WITH (NOLOCK) on tp.Id=tcms.PlantId
	where (@Id is null or tcms.Id=@Id)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblDefect]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_GettblDefect]    
(    
@PlantId varchar(max)=null    
)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
    select top(10) td.Id,td.DefectName,td.Occurrance,td.ShiftId,ts.Name as Shift,td.Date,td.Time,    
 td.PlantId as PlantId,tp.Name as Plant,td.StationId,ts1.Name as Station   
 from tblDefect td WITH (NOLOCK)    
 inner join tblShift ts  WITH (NOLOCK) on ts.Id=td.ShiftId    
 INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=td.PlantId   
 INNER JOIN tblStation ts1 WITH(NOLOCK) ON ts1.Id=td.StationId  
 where (@PlantId is null or tp.Id=@PlantId)   
 order by td.Occurrance desc  
     
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Getuser]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_Getuser] 
(
@UserNumber varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 select tl.*,tr.Id as RoleId,tr.Name as Role from tbllogin tl WITH (NOLOCK)
 INNER JOIN tblUser tu on tu.Id=tl.UserId
 INNER JOIN tblRoles tr on tr.Id=tu.RoleId
 where UserNumber=@UserNumber
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserEmail]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetUserEmail]
(
@UserNumber varchar(max)=null
)
AS
BEGIN

	SET NOCOUNT ON;

select tu.Email,tu.Id from tbllogin tl WITH (NOLOCK)
inner join tblUser tu WITH (NOLOCK) on tl.UserId=tu.Id
where UserNumber=@UserNumber
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistory]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_GetWorkingHistory]  
 (  
 @StartDate varchar(max)=null,  
 @EndDate varchar(max)=null,
 @PlantId varchar(max)=null
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select twh.Id,twh.Date,twh.DepartmentId as DepartmentId,td.Name Department,ts.Id as StationId,ts.Name Station,
	tst.Id as ShiftId,tst.Name Shift,tp.Id as PlantId,tp.Name as Plant
	from tblWorkingHistory twh WITH (NOLOCK) 
	Inner join tblDepartment td WITH (NOLOCK) on td.Id=twh.DepartmentId
	Inner join tblStation ts WITH (NOLOCK) on ts.Id=twh.StationId
	Inner join tblshift tst WITH (NOLOCK) on tst.Id=twh.ShiftId
	INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=twh.PlantId
 where ((@StartDate is null and @EndDate is null) or (CAST(Date AS DATE) between CAST(@StartDate as date) and Cast(@EndDate as date))) 
	and (@PlantId is null or tp.Id=@PlantId)
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistory_v2]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_GetWorkingHistory_v2]  
 (  
 @StartDate varchar(max)=null,  
 @EndDate varchar(max)=null,
 @PlantId varchar(max)=null,
 @UserId varchar(max)=null
 )  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    select twh.Id,twh.CreatedDate as Date,twh.DepartmentId as DepartmentId,td.Name Department,ts.Id as StationId,ts.Name Station,
	tst.Id as ShiftId,tst.Name Shift,tp.Id as PlantId,tp.Name as Plant
	from tblAllocation twh WITH (NOLOCK) 
	Inner join tblDepartment td WITH (NOLOCK) on td.Id=twh.DepartmentId
	Inner join tblStation ts WITH (NOLOCK) on ts.Id=twh.StationId
	Inner join tblshift tst WITH (NOLOCK) on tst.Id=twh.ShiftId
	INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=twh.PlantId
 where ((@StartDate is null and @EndDate is null) or (CAST(CreatedDate AS DATE) between CAST(@StartDate as date) and Cast(@EndDate as date))) 
	and (@PlantId is null or tp.Id=@PlantId)
	and (@UserId is null or twh.CellMemberId=@UserId)
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistoryById]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_GetWorkingHistoryById]    
 (     
 @Id varchar(max)=null  
 )    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
    select twh.Id,twh.Date,twh.DepartmentId as DepartmentId,td.Name Department,ts.Id as StationId,ts.Name Station,  
 tst.Id as ShiftId,tst.Name Shift,tp.Id as PlantId,tp.Name as Plant  
 from tblWorkingHistory twh WITH (NOLOCK)   
 Inner join tblDepartment td WITH (NOLOCK) on td.Id=twh.DepartmentId  
 Inner join tblStation ts WITH (NOLOCK) on ts.Id=twh.StationId  
 Inner join tblshift tst WITH (NOLOCK) on tst.Id=twh.ShiftId  
 INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=twh.PlantId  
 where (@Id is null or twh.Id=@Id)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertLogin]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertLogin]
(
@Name varchar(max)=null,
@Email varchar(max)=null,
@RoleId varchar(max)=null,
@PlantId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  Insert into tblUser(Name,Email,RoleId,PlantId)  
  OUTPUT INSERTED.*  
  values(@Name,@Email,@RoleId,@PlantId) 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertLoginUser]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_InsertLoginUser]   
(  
@UserNumber varchar(max)=null,  
@PasswordHash varchar(max)=null,  
@PasswordSalt varchar(max)=null,  
@UserId varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
  Insert into tblLogin(UserNumber,PasswordHash,PasswordSalt,UserId)  
  OUTPUT INSERTED.*  
  values(@UserNumber,@PasswordHash,@PasswordSalt,@UserId)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveRandomPassword]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveRandomPassword]
(
@UserId varchar(max)=null,
@Token varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

insert into tblForgotPassword(UserId,Token,IsUsed,CreatedDate) values(@UserId,@Token,0,GETDATE())

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAllocation]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_UpdateAllocation]   
(    
@Id varchar(max)=null,    
@CellMemberId varchar(max)=null,    
@DepartmentId varchar(max)=null,    
@StationId varchar(max)=null,    
@ShiftId varchar(max)=null,  
@CreatedBy varchar(max)=null,  
@PlantId varchar(max)=null , 
@CreatedDate varchar(max)=null 
)    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
  UPDATE tblAllocation    
SET     
    CellMemberId = @CellMemberId,    
    DepartmentId = @DepartmentId,    
    StationId = @StationId,    
    ShiftId = @ShiftId,  
 CreatedBy=@CreatedBy,  
 PlantId = @PlantId ,
 CreatedDate=@CreatedDate
WHERE     
    Id = @Id;    
    
 select * from tblAllocation WITH (NOLOCK) where Id=@Id    
     
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAvailability]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_UpdateAvailability] 
(  
@Id varchar(max)=null,  
@Date varchar(max)=null,  
@DepartmentId varchar(max)=null,  
@StationId varchar(max)=null,  
@ShiftId varchar(max)=null,
@StatusId varchar(max)=null,
@PlantId varchar(max)=null,
@UserId varchar(max)=null
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  UPDATE tblAvailability  
SET   
    Date = @Date,  
    DepartmentId = @DepartmentId,  
    StationId = @StationId,  
    ShiftId = @ShiftId,
	StatusId=@StatusId,
	PlantId = @PlantId,
	UserId = @UserId
WHERE   
    Id = @Id;  
  
 select * from tblAvailability WITH (NOLOCK) where Id=@Id  
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateCellMemberStatus]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_UpdateCellMemberStatus]   
(  
@Id varchar(max)=null,  
@Date varchar(max)=null,  
@StationId varchar(max)=null,  
@EmployeeId varchar(max)=null,  
@ShiftId varchar(max)=null,
@StatusId varchar(max)=null,
@PlantId varchar(max)=null
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  UPDATE tblCellMemberStatus  
SET   
    Date = @Date,  
    StationId = @StationId,  
    EmployeeId = @EmployeeId,  
    ShiftId = @ShiftId ,
	StatusId=@StatusId,
	PlantId=@PlantId
WHERE   
    Id = @Id;  
  
 select * from tblCellMemberStatus WITH (NOLOCK) where Id=@Id  
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateDefect]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_UpdateDefect]   
(  
@Id varchar(max)=null,  
@Date varchar(max)=null,  
@Occurrance varchar(max)=null,  
@DefectName varchar(max)=null,  
@ShiftId varchar(max)=null,
@PlantId varchar(max)=null,
@StationId varchar(max)=null
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  UPDATE tblDefect  
SET   
    Date = @Date,  
    DefectName = @DefectName,  
    Occurrance = @Occurrance,  
    ShiftId = @ShiftId,
	PlantId = @PlantId,
	StationId = @StationId
WHERE   
    Id = @Id;  
  
 select * from tblDefect WITH (NOLOCK) where Id=@Id  
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePassword]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdatePassword]
(
@PasswordSalt varchar(max)=null,
@PasswordHash varchar(max)=null,
@UserId varchar(max)=null,
@TokenId varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update tblLogin set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt WHERE UserId=@UserId

	update tblForgotPassword set isUsed=1 where Id=@TokenId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePasswordCellUser]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_UpdatePasswordCellUser]
(
@PasswordHash varchar(max)=null,
@UserId varchar(max)=null,
@PasswordSalt varchar(max)=null
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update tblLogin set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt WHERE UserId=@UserId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateWorkHistory]    Script Date: 9/9/2024 4:00:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[sp_UpdateWorkHistory]   
(  
@Id varchar(max)=null,  
@Date varchar(max)=null,  
@DepartmentId varchar(max)=null,  
@StationId varchar(max)=null,  
@ShiftId varchar(max)=null,  
@PlantId varchar(max)=null  
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  UPDATE tblWorkingHistory  
SET   
    Date = @Date,  
    DepartmentId = @DepartmentId,  
    StationId = @StationId,  
    ShiftId = @ShiftId ,  
 PlantId=@PlantId  
WHERE   
    Id = @Id;  
  
select twh.Id,twh.Date,twh.DepartmentId as DepartmentId,td.Name Department,ts.Id as StationId,ts.Name Station,  
 tst.Id as ShiftId,tst.Name Shift,tp.Id as PlantId,tp.Name as Plant  
 from tblWorkingHistory twh WITH (NOLOCK)   
 Inner join tblDepartment td WITH (NOLOCK) on td.Id=twh.DepartmentId  
 Inner join tblStation ts WITH (NOLOCK) on ts.Id=twh.StationId  
 Inner join tblshift tst WITH (NOLOCK) on tst.Id=twh.ShiftId  
 INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=twh.PlantId  
 where (@Id is null or twh.Id=@Id)  
   
END  
GO
USE [master]
GO
ALTER DATABASE [dbAbsenteeism] SET  READ_WRITE 
GO
