
/****** Object:  Table [dbo].[tblShift]    Script Date: 9/23/2024 12:28:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShift](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[PlantId] [int] NULL,
	[FromTime] [varchar](max) NULL,
	[EndTime] [varchar](max) NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblShift] ON 
GO
INSERT [dbo].[tblShift] ([Id], [Name], [PlantId], [FromTime], [EndTime]) VALUES (1, N'A', 1, N'5:30 am', N'2:00 pm')
GO
INSERT [dbo].[tblShift] ([Id], [Name], [PlantId], [FromTime], [EndTime]) VALUES (2, N'B', 1, N'2:00 pm', N'10:30 pm')
GO
INSERT [dbo].[tblShift] ([Id], [Name], [PlantId], [FromTime], [EndTime]) VALUES (3, N'C', 1, N'10:30 pm ', N'5:00 am')
GO
SET IDENTITY_INSERT [dbo].[tblShift] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_GetShift]    Script Date: 9/23/2024 12:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[sp_GetShift] 
(
@PlantId varchar(max)=null
)
AS
BEGIN
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select ts.Id,ts.Name, tp.Name as Plant,tp.Id as PlantId,ts.FromTime as FromTime,ts.EndTime as EndTime
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
/****** Object:  StoredProcedure [dbo].[sp_GettblAllocationsCellMember]    Script Date: 9/23/2024 12:28:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROCEDURE [dbo].[sp_GettblAllocationsCellMember] --'1','2024-09-10','2024-09-10',1     
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
  and (@UserId is null or ta.CellMemberId=@UserId)  
   
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
