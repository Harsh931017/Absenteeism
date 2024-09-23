USE [dbAbsenteeism]
GO
/****** Object:  Table [dbo].[tblBackEndErrorLog]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBackEndErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorMessage] [varchar](max) NULL,
	[StackTrace] [varchar](max) NULL,
	[MethodName] [varchar](max) NULL,
	[ClassName] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDatabaseErrorLog]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDatabaseErrorLog](
	[ErrorLogID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorNumber] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorProcedure] [varchar](max) NULL,
	[ErrorLine] [int] NULL,
	[ErrorMessage] [varchar](max) NULL,
	[ErrorDateTime] [datetime] NULL,
 CONSTRAINT [PK__tblDatab__D65247E2D605A5CD] PRIMARY KEY CLUSTERED 
(
	[ErrorLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHoliday]    Script Date: 9/20/2024 3:55:29 PM ******/
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
/****** Object:  Table [dbo].[tblNotifications]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AvailabilityId] [int] NULL,
	[UserId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsCheck] [bit] NULL,
 CONSTRAINT [PK_tblNotifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDeviceTokenMapping]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDeviceTokenMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[DeviceId] [varchar](max) NULL,
	[DeviceToken] [varchar](max) NULL,
	[DeviceType] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserDeviceTokenMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblDatabaseErrorLog] ADD  CONSTRAINT [DF__tblDataba__Error__76619304]  DEFAULT (getdate()) FOR [ErrorDateTime]
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAllocation]    Script Date: 9/20/2024 3:55:29 PM ******/
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
BEGIN TRY
Declare @Result bit=0;        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
     
	 
 if exists(select 1 from tblAllocation WITH (NOLOCK) where Id=@Id)        
 begin        
 set @Result=1        
 end        
           
 SELECT @Result   
 END TRY
 BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAvailability]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckAvailability] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN   
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblAvailability WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckCellMember]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
ALTER  PROCEDURE [dbo].[sp_CheckCellMember] --'QWERT12@34'     
(      
@Id varchar(Max)=null      
)      
AS      
BEGIN   
BEGIN TRY
Declare @Result bit=0;      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
 if exists(select 1 from tbluser WITH (NOLOCK) where Id=@Id and roleId=1)      
 begin      
 set @Result=1      
 end      
         
 SELECT @Result 
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
    -- Optional: Re-throw the error to stop execution
    
END CATCH;

END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckCellMembers]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
          
ALTER  PROCEDURE [dbo].[sp_CheckCellMembers] --'QWERT12@34'         
(          
@Id varchar(Max)=null          
)          
AS          
BEGIN 
BEGIN TRY
Declare @Result bit=0;          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
 
DECLARE @TotalCount INT
DECLARE @ExistingCount INT

-- Get the total count of values in the comma-separated list
SET @TotalCount = (SELECT COUNT(*) FROM STRING_SPLIT(@Id, ','))

-- Get the count of records in the table that match the values in the list
SET @ExistingCount = (SELECT COUNT(*) 
                      FROM tbluser WITH (NOLOCK)
                      WHERE roleId = 1
                        AND Id IN (SELECT value FROM STRING_SPLIT(@Id, ',')))


 if @TotalCount = @ExistingCount         
 begin          
 set @Result=1          
 end                   
 SELECT @Result  
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;

END 


GO
/****** Object:  StoredProcedure [dbo].[sp_CheckCellMembersAlreadyAllocated]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
          
ALTER  PROCEDURE [dbo].[sp_CheckCellMembersAlreadyAllocated] --'QWERT12@34'         
(          
@Id varchar(Max)=null,
@CreatedDate varchar(Max)=null
)          
AS          
BEGIN   
BEGIN TRY
Declare @Result bit=0;          
 -- SET NOCOUNT ON added to prevent extra result sets from          
 -- interfering with SELECT statements.          
 SET NOCOUNT ON;          
 
DECLARE @ExistingCount INT

-- Get the count of records in the table that match the values in the list
SET @ExistingCount = (SELECT COUNT(*) 
                      FROM tblAllocation WITH (NOLOCK)
                      WHERE  CellMemberId IN (SELECT value FROM STRING_SPLIT(@Id, ','))
					  AND (@CreatedDate is null or Convert(date,CreatedDate)= convert(date,@CreatedDate)))
					  


 if @ExistingCount=0         
 begin          
 set @Result=1          
 end                   
 SELECT @Result 
 
  
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_CheckDefect]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckDefect] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN  
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblDefect WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result   

   END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckDepartment]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckDepartment] --'QWERT12@34'   
(    
@DepartmentId varchar(Max)=null    
)    
AS    
BEGIN  
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblDepartment WITH (NOLOCK) where Id=@DepartmentId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result  
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckPlant]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
        
ALTER  PROCEDURE [dbo].[sp_CheckPlant] --'QWERT12@34'       
(        
@Id varchar(Max)=null        
)        
AS        
BEGIN 
BEGIN TRY
Declare @Result bit=0;        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
        
 if exists(select 1 from tblPlant WITH (NOLOCK) where Id=@Id)        
 begin        
 set @Result=1        
 end        
           
 SELECT @Result 
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckRole]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_CheckRole] --'QWERT12@34' 
(  
@RoleId varchar(Max)=null  
)  
AS  
BEGIN  
BEGIN TRY
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblRoles WITH (NOLOCK) where Id=@RoleId)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 

	 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckShift]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckShift] --'QWERT12@34'   
(    
@ShiftId varchar(Max)=null    
)    
AS    
BEGIN  
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblShift WITH (NOLOCK) where Id=@ShiftId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result 
 
 	 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStation]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckStation] --'QWERT12@34'   
(    
@StationId varchar(Max)=null    
)    
AS    
BEGIN  
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblStation WITH (NOLOCK) where Id=@StationId)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result  
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckStatus]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckStatus] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN    
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblStatus WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result  
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckTokenValidity]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_CheckTokenValidity]   
(  
@UserId varchar(max)=null,  
@Token varchar(max)=null  
)  
AS  
BEGIN  
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 select * from tblForgotPassword WITH (NOLOCK)  
 where UserId=@UserId and Token=@Token and IsUsed=0 AND CreatedDate >= DATEADD(MINUTE, -15, GETDATE())
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
    
END CATCH;
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUser]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
ALTER  PROCEDURE [dbo].[sp_CheckUser] --'QWERT12@34'     
(      
@Id varchar(Max)=null      
)      
AS      
BEGIN    
BEGIN TRY
Declare @Result bit=0;      
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
 if exists(select 1 from tblUser WITH (NOLOCK) where Id=@Id)      
 begin      
 set @Result=1      
 end      
         
 SELECT @Result    
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUserEmail]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_CheckUserEmail] --'QWERT12@34' 
(  
@UserEmail varchar(Max)=null  
)  
AS  
BEGIN  
BEGIN TRY
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblUser WITH (NOLOCK) where Email=@UserEmail)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckUserNumber]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_CheckUserNumber] --'QWERT12@34' 
(  
@UserNumber varchar(Max)=null  
)  
AS  
BEGIN  
BEGIN TRY
Declare @Result bit=0;  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
 if exists(select 1 from tblLogin WITH (NOLOCK) where UserNumber=@UserNumber)  
 begin  
 set @Result=1  
 end  
     
	SELECT @Result 
	 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckWorkHistory]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CheckWorkHistory] --'QWERT12@34'   
(    
@Id varchar(Max)=null    
)    
AS    
BEGIN  
BEGIN TRY
Declare @Result bit=0;    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
 if exists(select 1 from tblWorkingHistory WITH (NOLOCK) where Id=@Id)    
 begin    
 set @Result=1    
 end    
       
 SELECT @Result 
 
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateDefect]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_CreateDefect]
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
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblDefect(Date,DefectName,Occurrance,ShiftId,PlantId,StationId) 
	OUTPUT INSERTED.*
	values(@Date,@DefectName,@Occurrance,@ShiftId,@PlantId,@StationId)
	 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateNotification]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_CreateNotification]
(
@AvailabilityId varchar(max)=null,
@UserId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Insert into tblNotifications(AvailabilityId,UserId,CreatedDate,IsCheck) 
	OUTPUT INSERTED.* 
	values(@AvailabilityId,@UserId,GETDate(),0)

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblAllocation]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_CreatetblAllocation]  
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
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  

DECLARE @Tomorrow DATE = DATEADD(DAY, 1, @CreatedDate);
DECLARE @NextBusinessDate DATE;
  
    insert into tblAllocation(CellMemberId,DepartmentId,StationId,ShiftId,CreatedBy,CreatedDate,PlantId)   
 OUTPUT INSERTED.*  
 values(@CellMemberId,@DepartmentId,@StationId,@ShiftId,@CreatedBy,@CreatedDate,@PlantId) 
 
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

 Insert into tblAvailability(Date,DepartmentId,StationId,ShiftId,PlantId,UserId)
values (@NextBusinessDate,@DepartmentId,@StationId,@ShiftId,@PlantId,@CellMemberId)

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 


GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblAllocation_v2]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_CreatetblAllocation_v2]    
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
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
  
DECLARE @Tomorrow DATE = DATEADD(DAY, 1, @CreatedDate);  
DECLARE @NextBusinessDate DATE;  

-- Split @CellMemberId into a table
    DECLARE @CellMemberIdTable TABLE (CellMemberId VARCHAR(MAX));

  INSERT INTO @CellMemberIdTable (CellMemberId)
    SELECT TRIM(value) 
    FROM STRING_SPLIT(@CellMemberId, ',');
    
    insert into tblAllocation(CellMemberId,DepartmentId,StationId,ShiftId,CreatedBy,CreatedDate,PlantId)     
 OUTPUT INSERTED.*    
 SELECT CellMemberId, @DepartmentId, @StationId, @ShiftId, @CreatedBy, @CreatedDate, @PlantId
    FROM @CellMemberIdTable;  
   
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
  
 Insert into tblAvailability(Date,DepartmentId,StationId,ShiftId,PlantId,UserId)  
SELECT @NextBusinessDate, @DepartmentId, @StationId, @ShiftId, @PlantId, CellMemberId
    FROM @CellMemberIdTable;  

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;	

END   
  
  
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblAvailability]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_CreatetblAvailability]
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
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblAvailability(Date,DepartmentId,StationId,ShiftId,StatusId,PlantId,UserId) 
	OUTPUT INSERTED.*
	values(@Date,@DepartmentId,@StationId,@ShiftId,@StatusId,@PlantId,@UserId)

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreatetblCellMemberStatus]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_CreatetblCellMemberStatus]
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
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblCellMemberStatus(Date,EmployeeId,StationId,ShiftId,StatusId,PlantId) 
	OUTPUT INSERTED.*
	values(@Date,@EmployeeId,@StationId,@ShiftId,@StatusId,@PlantId)

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateWorkHistory]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_CreateWorkHistory] 
(
@Date varchar(max)=null,
@DepartmentId varchar(max)=null,
@StationId varchar(max)=null,
@ShiftId varchar(max)=null,
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    insert into tblWorkingHistory(Date,DepartmentId,StationId,ShiftId,PlantId) 
	OUTPUT INSERTED.*
	values(@Date,@DepartmentId,@StationId,@ShiftId,@PlantId)

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblAllocation]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER  PROCEDURE [dbo].[sp_DeletetblAllocation]     
(    
@Id varchar(max)=null    
)    
AS    
BEGIN    
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
 Delete from tblAllocation where Id=@Id  
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
      
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblAvailability]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_DeletetblAvailability]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN  
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblAvailability where Id=@Id  

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblCellMemberStatus]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_DeletetblCellMemberStatus]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblCellMemberStatus where Id=@Id  

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletetblDefect]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER  PROCEDURE [dbo].[sp_DeletetblDefect]   
(  
@Id varchar(max)=null  
)  
AS  
BEGIN
BEGIN TRY

 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
 Delete from tblDefect where Id=@Id  

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
    
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteWorkHistory]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_DeleteWorkHistory] 
(
@Id varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Delete from tblWorkingHistory where Id=@Id

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
  
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCellMember]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[sp_GetCellMember]
(
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select tu.Id,tu.Name, tp.Name as Plant,tp.Id as PlantId,tu.Email,tu.RoleId as RoleId,tr.Name as Role
from tblUser tu  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on tu.PlantId=tp.Id
INNER JOIN tblRoles tr WITH (NOLOCK) on tu.RoleId=tr.Id
where @PlantId is null or tu.PlantId=@PlantId and tr.Id=1

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDefectById]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
alter PROCEDURE [dbo].[sp_GetDefectById]      
 (       
 @Id varchar(max)=null    
 )      
AS      
BEGIN     
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetDepartments]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetDepartments]
(
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select td.Id,td.Name, tp.Name as Plant,tp.Id as PlantId
from tblDepartment td  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on td.PlantId=tp.Id
where @PlantId is null or td.PlantId=@PlantId 

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNotificationByUserId]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_GetNotificationByUserId]  
(  
@UserId varchar(max)=null  
)  
AS  
BEGIN  
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
   select * from tblNotifications where (@UserId is null or UserId=@UserId) and IsCheck=0 

   END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRoles]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetRoles] 
(
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select tr.Id as RoleId,tr.Name as Role,tp.Id as PlantId,tp.Name as Plant from tblRoles tr  WITH (NOLOCK)
INNER JOIN tblplant tp on tr.PlantId=tp.Id
where (@PlantId is null or tr.PlantId=@PlantId)

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetShift]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetShift] 
(
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ts.Id,ts.Name, tp.Name as Plant,tp.Id as PlantId,ts.Timing as Timing
from tblShift ts  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on ts.PlantId=tp.Id
where @PlantId is null or ts.PlantId=@PlantId

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetStationByDepartmentId]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetStationByDepartmentId]
(
@PlantId varchar(max)=null,
@DepartmentId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ts.Id,ts.Name, tp.Name as Plant,tp.Id as PlantId,ts.DepartmentId,td.Name as Department
from tblStation ts  WITH (NOLOCK)
INNER JOIN tblPlant tp WITH (NOLOCK) on ts.PlantId=tp.Id
INNER JOIN tblDepartment td WITH (NOLOCK) on ts.DepartmentId=td.Id
where (@PlantId is null or ts.PlantId=@PlantId ) and (@DepartmentId is null or ts.DepartmentId=@DepartmentId )

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocationById]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_GettblAllocationById]  
 (  
 @Id varchar(max)=null  
 )  
AS  
BEGIN 
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocations]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_GettblAllocations] --'1','2024-09-10','2024-09-10',1   
 (      
 @PlantId varchar(max)=null ,
 @StartDate varchar(max)=null,
 @EndDate varchar(max)=null,
 @ShiftId varchar(max)=null,
 @UserId varchar(max)=null
 )    
AS    
BEGIN   
BEGIN TRY
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
   (
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(CreatedDate AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))
        OR (CAST(CreatedDate AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))
      )
	  and
  (@PlantId is null or tp.Id=@plantId) 
  and (@ShiftId is null or ts1.Id=@ShiftId) 
  and (@UserId is null or ta.CreatedBy=@UserId)
    
  order by ta.CreatedDate desc  

  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocationsCellMember]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_GettblAllocationsCellMember] --'1','2024-09-10','2024-09-10',1     
 (        
 @PlantId varchar(max)=null ,   
 @UserId varchar(max)=null  
 )      
AS      
BEGIN   
BEGIN TRY
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
   convert(date,ta.CreatedDate)=Convert(date,getdate()) 
   and  
  (@PlantId is null or tp.Id=@plantId)     
  and (@UserId is null or ta.CreatedBy=@UserId)  
   
    END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailability]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_GettblAvailability]  
 (  
 @StartDate varchar(max)=null,  
 @EndDate varchar(max)=null,  
 @PlantId varchar(max)=null,  
 @UserId varchar(max)=null  
 )  
AS  
BEGIN  
BEGIN TRY
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
 where (
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(Date AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))
        OR (CAST(Date AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))
      )  
  and (@PlantId is null or tp.Id=@PlantId)  
  and (@UserId is null or tu.Id=@UserId)  
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailabilityById]    Script Date: 9/20/2024 3:55:29 PM ******/
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
BEGIN TRY
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
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblCellMemberStatus]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
ALTER PROCEDURE [dbo].[sp_GettblCellMemberStatus]      
 (      
 @StartDate varchar(max)=null,      
 @EndDate varchar(max)=null,      
 @ShiftId varchar(max)=null,      
 @PlantId varchar(max)=null      
 )      
AS      
BEGIN    
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
    select tcms.Id,tcms.Date,tcms.StationId,ts.Name as Station,      
 tcms.UserId as EmployeeId,tu.Name as Employee,tcms.ShiftId,ts1.Name as Shift,      
 tcms.StatusId,ts2.Status,tp.Id as PlantId,tp.Name as Plant,
 CASE 
        WHEN ta.CellMemberId IS NOT NULL THEN 1 
        ELSE 0 
    END AS IsAllocated
 from tblAvailability tcms WITH (NOLOCK)      
 INNER JOIN tblStation ts  WITH (NOLOCK) on ts.Id=tcms.StationId      
 INNER JOIN tblUser tu  WITH (NOLOCK) on tcms.UserId=tu.Id      
 INNER JOIN tblShift ts1  WITH (NOLOCK) on ts1.Id=tcms.ShiftId      
 INNER JOIN tblStatus ts2  WITH (NOLOCK) on ts2.Id=tcms.StatusId      
 INNER JOIN tblPlant tp  WITH (NOLOCK) on tp.Id=tcms.PlantId
 LEFT JOIN tblAllocation ta WITH (NOLOCK) on ta.CellMemberId=tcms.UserId and convert(date,tcms.Date)=Convert(date,ta.CreatedDate)
 where (  
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(Date AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))  
        OR (CAST(Date AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))  
      )  
	
 and (@ShiftId is null or tcms.ShiftId=@ShiftId)      
 and (@PlantId is null or tp.Id=@plantId)  
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblCellMemberStatusById]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GettblCellMemberStatusById]
	(
	@Id varchar(max)=null
	)
AS
BEGIN
BEGIN TRY
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

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblDefect]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
ALTER PROCEDURE [dbo].[sp_GettblDefect]      
(      
@PlantId varchar(max)=null      
)      
AS      
BEGIN   
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
    select top(10) td.Id,td.DefectName,td.Occurrance,td.ShiftId,ts.Name as Shift,td.Date,td.Time,      
 td.PlantId as PlantId,tp.Name as Plant,td.StationId,ts1.Name as Station     
 from tblDefect td WITH (NOLOCK)      
 inner join tblShift ts  WITH (NOLOCK) on ts.Id=td.ShiftId      
 INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=td.PlantId     
 INNER JOIN tblStation ts1 WITH(NOLOCK) ON ts1.Id=td.StationId    
 where (@PlantId is null or tp.Id=@PlantId) and convert(date,td.Date)=Convert(date,DATEADD(DAY, -1, GETDATE()))   
 order by td.Occurrance desc   
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
       
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Getuser]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_Getuser] 
(
@UserNumber varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 select tl.*,tr.Id as RoleId,tr.Name as Role from tbllogin tl WITH (NOLOCK)
 INNER JOIN tblUser tu on tu.Id=tl.UserId
 INNER JOIN tblRoles tr on tr.Id=tu.RoleId
 where UserNumber=@UserNumber

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserEmail]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_GetUserEmail]
(
@UserNumber varchar(max)=null
)
AS
BEGIN
BEGIN TRY

	SET NOCOUNT ON;

select tu.Email,tu.Id from tbllogin tl WITH (NOLOCK)
inner join tblUser tu WITH (NOLOCK) on tl.UserId=tu.Id
where UserNumber=@UserNumber

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistory]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_GetWorkingHistory]  
 (  
 @StartDate varchar(max)=null,  
 @EndDate varchar(max)=null,
 @PlantId varchar(max)=null
 )  
AS  
BEGIN  
BEGIN TRY
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

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistory_v2]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
      
ALTER PROCEDURE [dbo].[sp_GetWorkingHistory_v2]      
 (      
 @StartDate varchar(max)=null,      
 @EndDate varchar(max)=null,    
 @PlantId varchar(max)=null,    
 @UserId varchar(max)=null    
 )      
AS      
BEGIN    
BEGIN TRY  
 -- SET NOCOUNT ON added to prevent extra result sets from      
 -- interfering with SELECT statements.      
 SET NOCOUNT ON;      
      
    select twh.Id, FORMAT(CAST(twh.CreatedDate AS DATETIME), 'dd-MM-yyyy')  as Date,twh.DepartmentId as DepartmentId,td.Name Department,ts.Id as StationId,ts.Name Station,    
 tst.Id as ShiftId,tst.Name Shift,tp.Id as PlantId,tp.Name as Plant    
 from tblAllocation twh WITH (NOLOCK)     
 Inner join tblDepartment td WITH (NOLOCK) on td.Id=twh.DepartmentId    
 Inner join tblStation ts WITH (NOLOCK) on ts.Id=twh.StationId    
 Inner join tblshift tst WITH (NOLOCK) on tst.Id=twh.ShiftId    
 INNER JOIN tblPlant tp WITH (NOLOCK) on tp.Id=twh.PlantId    
 where (  
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(CreatedDate AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))  
        OR (CAST(CreatedDate AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))  
      )  
 and (@PlantId is null or tp.Id=@PlantId)    
 and (@UserId is null or twh.CellMemberId=@UserId)    
  
 END TRY  
BEGIN CATCH  
    -- Insert the error details into the ErrorLog table  
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)  
    VALUES (  
        ERROR_NUMBER(),  
        ERROR_SEVERITY(),  
        ERROR_STATE(),  
        ERROR_PROCEDURE(),  
        ERROR_LINE(),  
        ERROR_MESSAGE()  
    );  
      
   
END CATCH;  
END 


GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistoryById]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [dbo].[sp_GetWorkingHistoryById]    
 (     
 @Id varchar(max)=null  
 )    
AS    
BEGIN  
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertErrorLog]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertErrorLog]
(
@ErrorMessage varchar(max)=null,
@StackTrace varchar(max)=null,
@MethodName varchar(max)=null,
@ClassName varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Insert into tblBackEndErrorLog(ErrorMessage,StackTrace,MethodName,ClassName,CreatedDate) 
values (@ErrorMessage,@StackTrace,@MethodName,@ClassName,GETDAte())

END TRY
 BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertLogin]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_InsertLogin]
(
@Name varchar(max)=null,
@Email varchar(max)=null,
@RoleId varchar(max)=null,
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  Insert into tblUser(Name,Email,RoleId,PlantId)  
  OUTPUT INSERTED.*  
  values(@Name,@Email,@RoleId,@PlantId) 

  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertLoginUser]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_InsertLoginUser]   
(  
@UserNumber varchar(max)=null,  
@PasswordHash varchar(max)=null,  
@PasswordSalt varchar(max)=null,  
@UserId varchar(max)=null  
)  
AS  
BEGIN  
BEGIN TRY
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
  Insert into tblLogin(UserNumber,PasswordHash,PasswordSalt,UserId)  
  OUTPUT INSERTED.*  
  values(@UserNumber,@PasswordHash,@PasswordSalt,@UserId) 
  
  END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveRandomPassword]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_SaveRandomPassword]
(
@UserId varchar(max)=null,
@Token varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

insert into tblForgotPassword(UserId,Token,IsUsed,CreatedDate) values(@UserId,@Token,0,GETDATE())

END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
    -- Optional: Re-throw the error to stop execution
    THROW;
END CATCH;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAllocation]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
ALTER PROCEDURE [dbo].[sp_UpdateAllocation]   
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
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
     
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAvailability]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_UpdateAvailability] 
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
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateAvailability_v2]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    
CREATE PROCEDURE [dbo].[sp_UpdateAvailability_v2] --1,1  
(    
@UserId varchar(max)=null,  
@StatusId varchar(max)=null ,
@isToday varchar(max)=null
)    
AS    
BEGIN  
  
BEGIN TRY  
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
  
  if(@isToday is null or @isToday=0)
  BEGIN
  UPDATE tblAvailability    
SET     
 StatusId=@StatusId  
WHERE     
    UserId = @UserId and DATE=@NextBusinessDate  
    
 select Top(1) * from tblAvailability WITH (NOLOCK) WHERE     
    UserId = @UserId and DATE=@NextBusinessDate  
   END
   ELSE
   BEGIN
    UPDATE tblAvailability    
SET     
 StatusId=@StatusId  
WHERE     
    UserId = @UserId and DATE= Convert(date,GETDATE()) 
    
 select Top(1) * from tblAvailability WITH (NOLOCK) WHERE     
    UserId = @UserId and DATE= Convert(date,GETDATE()) 
   END

  
   END TRY  
BEGIN CATCH  
    -- Insert the error details into the ErrorLog table  
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)  
    VALUES (  
        ERROR_NUMBER(),  
        ERROR_SEVERITY(),  
        ERROR_STATE(),  
        ERROR_PROCEDURE(),  
        ERROR_LINE(),  
        ERROR_MESSAGE()  
    );  
      
    
END CATCH;  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateCellMemberStatus]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_UpdateCellMemberStatus]   
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
BEGIN TRY
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

 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateDefect]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_UpdateDefect]   
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
BEGIN TRY
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
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
  
END CATCH;
   
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateNotification]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_UpdateNotification]
(
@UserId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update tblNotifications set IsCheck=1 where (@UserId is null or UserId=@UserId) 
	and Convert(date,CreatedDate)=Convert(date,Getdate())

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePassword]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_UpdatePassword]
(
@PasswordSalt varchar(max)=null,
@PasswordHash varchar(max)=null,
@UserId varchar(max)=null,
@TokenId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update tblLogin set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt WHERE UserId=@UserId

	update tblForgotPassword set isUsed=1 where Id=@TokenId

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    

END CATCH;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePasswordCellUser]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_UpdatePasswordCellUser]
(
@PasswordHash varchar(max)=null,
@UserId varchar(max)=null,
@PasswordSalt varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	update tblLogin set PasswordHash=@PasswordHash,PasswordSalt=@PasswordSalt WHERE UserId=@UserId

	END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
   
END CATCH;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateWorkHistory]    Script Date: 9/20/2024 3:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_UpdateWorkHistory]   
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
BEGIN TRY
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
 
 END TRY
BEGIN CATCH
    -- Insert the error details into the ErrorLog table
    INSERT INTO tblDatabaseErrorLog (ErrorNumber, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorMessage)
    VALUES (
        ERROR_NUMBER(),
        ERROR_SEVERITY(),
        ERROR_STATE(),
        ERROR_PROCEDURE(),
        ERROR_LINE(),
        ERROR_MESSAGE()
    );
    
 
END CATCH;
   
END  
GO
