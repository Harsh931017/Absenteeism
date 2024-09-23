
/****** Object:  StoredProcedure [dbo].[sp_GettblAvailability]    Script Date: 9/12/2024 11:18:07 AM ******/
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
 where (
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(Date AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))
        OR (CAST(Date AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))
      )  
  and (@PlantId is null or tp.Id=@PlantId)  
  and (@UserId is null or tu.Id=@UserId)  
END  
GO
/****** Object:  StoredProcedure [dbo].[sp_GettblCellMemberStatus]    Script Date: 9/12/2024 11:18:07 AM ******/
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
 where (
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(Date AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))
        OR (CAST(Date AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))
      )   
 and (@ShiftId is null or ShiftId=@ShiftId)    
 and (@PlantId is null or tp.Id=@plantId)    
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_GetWorkingHistory_v2]    Script Date: 9/12/2024 11:18:07 AM ******/
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
 where (
        (@StartDate IS NULL AND @EndDate IS NULL AND CAST(CreatedDate AS DATE) BETWEEN CAST(DATEADD(DAY, -30, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE))
        OR (CAST(CreatedDate AS DATE) BETWEEN CAST(@StartDate AS DATE) AND CAST(@EndDate AS DATE))
      )
 and (@PlantId is null or tp.Id=@PlantId)  
 and (@UserId is null or twh.CellMemberId=@UserId)  
END 
GO
