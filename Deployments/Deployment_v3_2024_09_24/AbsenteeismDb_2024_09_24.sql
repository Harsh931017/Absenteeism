USE [dbAbsenteeism]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateNotification]    Script Date: 9/24/2024 2:47:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
ALTER PROCEDURE [dbo].[sp_CreateNotification]  
(   
@UserId varchar(max)=null  
)  
AS  
BEGIN  
BEGIN TRY  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  

 declare @AvailabilityId varchar(max)=null

 set @AvailabilityId=(select Id from tblAvailability where (@UserId is null or UserId=@UserId) and  Convert(date,Date)=Convert(date,Getdate()))

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
/****** Object:  StoredProcedure [dbo].[sp_GetNotificationByUserId]    Script Date: 9/24/2024 2:47:39 PM ******/
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
  
   select * from tblNotifications where (@UserId is null or UserId=@UserId) and IsCheck=0 and Convert(date,CreatedDate)=Convert(date,Getdate())

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
/****** Object:  StoredProcedure [dbo].[sp_UpdateNotification]    Script Date: 9/24/2024 2:47:39 PM ******/
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
   
 select Id,AvailabilityId,Userid,CreatedDate,IsCheck from tblNotifications 
 where (@UserId is null or UserId=@UserId)     
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
