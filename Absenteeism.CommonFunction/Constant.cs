namespace Absenteeism.CommonFunction
{
    public class Constant
    {
        public static class Messages
        {
            public const string Success = "User login successfully";
            public const string CommonSuccess = "Success";
            public const string Failed = "Something went wrong";
            public const string InternalError = "Internal Server Error";
            public const string NoRecord = "No Record Exist";
            public const string Invalid_Credentials = "Invalid Credentials";
            public const string Incorrect_Role = "Incorrect Role selected";
            public const string Duplicate_UserNumber = "User Number already exists.";
            public const string Duplicate_UserEmail = "User Email already exists.";
            public const string Invalid_Role = "Role is not exists.";
            public const string Invalid_Plant = "Plant is not exists.";
            public const string Invalid_Department = "Department is not exists.";
            public const string Invalid_WorkHistory = "Work History is not exists.";
            public const string Invalid_Station = "Station is not exists.";
            public const string Invalid_Shift = "Shift is not exists.";
            public const string Invalid_Status = "Status is not exists.";
            public const string Invalid_Employee = "Employee is not exists.";
            public const string Invalid_Defect = "Defect is not exists.";
            public const string Invalid_Availabililty = "Availabililty is not exists.";
            public const string Invalid_CellMember = "Cell Member is not exists.";
            public const string Invalid_CellMemberAllocated = "Cell Member is already allocated.";
            public const string Invalid_Allocation = "Allocation is not exists.";
            public const string UserNumber = "User Number is not exist.";
            public const string ConfirmAndNewPassword = "Confirm and new Password are not same";
            public const string NotValidToken = "Token is not valid";
            public const string ChangePassword = "Password is change successfully";
            public const string DeleteWorkHistory = "Work History is deleted successfully";
            public const string UpdateWorkHistory = "Work History is updated successfully";
            public const string UpdateDefect = "Defect is updated successfully";
            public const string DeleteDefect = "Defect is deleted successfully";
            public const string DeletetblAvailability = "tblAvailability is deleted successfully";
            public const string UpdatetblAvailability = "tblAvailability is updated successfully";
            public const string DeletetblCellMemberStatus = "CellMemberStatus is deleted successfully";
            public const string UpdatetblCellMemberStatus = "tblCellMemberStatus is updated successfully";
            public const string DeletetblAllocation = "Allocation is deleted successfully";

        }

        public static class ResponseType
        {
            public const short Insert = 1;
            public const short Update = 2;
            public const short Delete = 3;
            public const short AlreadyExists = 4;
            public const short Error = -1;
            public const short IncorrectOldPassword = 5;
            public const short NotFound = 6;
            public const short AccessDenied = -2;
        }

        public static class Routing
        {
            public const string Register = "Register";
            public const string Login = "Login";
            public const string CreateWorkHistory = "CreateWorkHistory";
            public const string DeleteWorkHistory = "DeleteWorkHistory";
            public const string UpdateWorkHistory = "UpdateWorkHistory";
            public const string GetWorkHistory = "GetWorkHistory";
            public const string CreateDefect = "CreateDefect";
            public const string DeletetblDefect = "DeletetblDefect";
            public const string UpdatetblDefect = "UpdatetblDefect";
            public const string GettblDefect = "GettblDefect";
            public const string CreateAvailability = "CreateAvailability";
            public const string DeletetblAvailability = "DeletetblAvailability";
            public const string UpdatetblAvailability = "UpdatetblAvailability";
            public const string GettblAvailability = "GettblAvailability";
            public const string CreateCellMember = "CreateCellMember";
            public const string DeleteCellMember = "DeleteCellMember";
            public const string UpdatetblCellMemberStatus = "UpdatetblCellMemberStatus";
            public const string tblCellMemberStatus = "GettblCellMemberStatus";
            public const string GettblAllocation = "GettblAllocation";
            public const string GettodayAllocationForCellMember = "GettodayAllocationForCellMember";
            public const string GetRoles = "GetRoles";
            public const string GetShift = "GetShift";
            public const string GetCellMember = "GetCellMember";
            public const string GetDepartment = "GetDepartment";
            public const string GetStation = "GetStation";
            public const string ForgotPasswordSendEmail = "ForgotPasswordSendEmail";
            public const string UpdatePassword = "UpdatePassword";
            public const string ChangePassword = "ChangeCellMemberPassword";
            public const string CreateAllocation = "CreateAllocation";
            public const string UpdatetblAllocation = "UpdatetblAllocation";
            public const string DeleteAllocation = "DeleteAllocation";
            public const string PushNotification = "PushNotification";
        }

        public static class StoreProcedureAndParameters
        {
            public const string sp_GetRoles = "sp_GetRoles";
            public const string sp_GetShift = "sp_GetShift";
            public const string sp_GetCellMember = "sp_GetCellMember";
            public const string sp_GetDepartments = "sp_GetDepartments";
            public const string sp_GetStationByDepartmentId = "sp_GetStationByDepartmentId";
            public const string sp_CheckuserNumber = "sp_CheckUserNumber";
            public const string sp_InsertLoginUser = "sp_InsertLoginUser";
            public const string sp_GetuserLogin = "sp_Getuser";
            public const string sp_InsertLogin = "sp_InsertLogin";
            public const string sp_GetUserEmail = "sp_GetUserEmail";
            public const string sp_SaveRandomPassword = "sp_SaveRandomPassword";
            public const string sp_UpdatePasswordCellUser = "sp_UpdatePasswordCellUser";
            public const string sp_CheckTokenValidity = "sp_CheckTokenValidity";
            public const string sp_UpdatePassword = "sp_UpdatePassword";
            public const string sp_CreateWorkHistory = "sp_CreateWorkHistory";
            public const string sp_DeleteWorkHistory = "sp_DeleteWorkHistory";
            public const string sp_UpdateWorkHistory = "sp_UpdateWorkHistory";
            public const string sp_GetWorkingHistory = "sp_GetWorkingHistory";
            public const string sp_GetWorkingHistory_v2 = "sp_GetWorkingHistory_v2";
            public const string sp_GetWorkingHistoryById = "sp_GetWorkingHistoryById";
            public const string sp_CreateDefect = "sp_CreateDefect";
            public const string sp_DeletetblDefect = "sp_DeletetblDefect";
            public const string sp_UpdateDefect = "sp_UpdateDefect";
            public const string sp_GettblDefect = "sp_GettblDefect";
            public const string sp_GetDefectById = "sp_GetDefectById";
            public const string sp_CreatetblAvailability = "sp_CreatetblAvailability";
            public const string sp_GettblAvailabilityById = "sp_GettblAvailabilityById";
            public const string sp_DeletetblAvailability = "sp_DeletetblAvailability";
            public const string sp_UpdateAvailability = "sp_UpdateAvailability";
            public const string sp_UpdateAvailability_v2 = "sp_UpdateAvailability_v2";
            public const string sp_GettblAvailability = "sp_GettblAvailability";
            public const string sp_CreatetblCellMemberStatus = "sp_CreatetblCellMemberStatus";
            public const string sp_CreatetblAllocation = "sp_CreatetblAllocation";
            public const string sp_CreatetblAllocation_v2 = "sp_CreatetblAllocation_v2";
            public const string sp_UpdateAllocation = "sp_UpdateAllocation";
            public const string sp_DeletetblCellMemberStatus = "sp_DeletetblCellMemberStatus";
            public const string sp_UpdateCellMemberStatus = "sp_UpdateCellMemberStatus";
            public const string sp_GettblCellMemberStatus = "sp_GettblCellMemberStatus";
            public const string sp_GettblAllocations = "sp_GettblAllocations";
            public const string sp_GettblAllocationsCellMember = "sp_GettblAllocationsCellMember";
            public const string sp_GettblCellMemberStatusById = "sp_GettblCellMemberStatusById";
            public const string sp_GettblAllocationById = "sp_GettblAllocationById";
            public const string sp_CheckUserEmail = "sp_CheckUserEmail";
            public const string sp_CheckRole = "sp_CheckRole";
            public const string sp_CheckDepartment = "sp_CheckDepartment";
            public const string sp_CheckStation = "sp_CheckStation";
            public const string sp_CheckShift = "sp_CheckShift";
            public const string sp_CheckWorkHistory = "sp_CheckWorkHistory";
            public const string sp_CheckDefect = "sp_CheckDefect";
            public const string sp_CheckStatus = "sp_CheckStatus";
            public const string sp_CheckAvailability = "sp_CheckAvailability";
            public const string sp_CheckUser = "sp_CheckUser";
            public const string sp_CheckCellMember = "sp_CheckCellMember";
            public const string sp_CheckCellMembers = "sp_CheckCellMembers";
            public const string sp_InsertErrorLog = "sp_InsertErrorLog";
            public const string sp_CheckCellMembersAlreadyAllocated = "sp_CheckCellMembersAlreadyAllocated";
            public const string sp_CheckAllocation = "sp_CheckAllocation";
            public const string sp_CheckPlant = "sp_CheckPlant";
            public const string sp_DeletetblAllocation = "sp_DeletetblAllocation";
            public const string sp_GetNotificationByUserId = "sp_GetNotificationByUserId";
            public const string sp_UpdateNotification = "sp_UpdateNotification";
            public const string sp_CreateNotification = "sp_CreateNotification";
            public const string Id = "Id";
            public const string UserNumber = "UserNumber";
            public const string RoleId = "RoleId";
            public const string ProcedureParameter_Date = "@Date";
            public const string ProcedureParameter_EmployeeId = "@EmployeeId";
            public const string ProcedureParameter_CellMemberId = "@CellMemberId";
            public const string ProcedureParameter_StationId = "@StationId";
            public const string ProcedureParameter_ShiftId = "@ShiftId";
            public const string ProcedureParameter_StatusId = "@StatusId";
            public const string ProcedureParameter_DepartmentId = "@DepartmentId";
            public const string ProcedureParameter_Id = "@Id";
            public const string ProcedureParameter_ErrorMessage = "@ErrorMessage";
            public const string ProcedureParameter_MethodName = "@MethodName";
            public const string ProcedureParameter_StackTrace = "@StackTrace";
            public const string ProcedureParameter_ClassName = "@ClassName";
            public const string ProcedureParameter_StartDate = "@StartDate";
            public const string ProcedureParameter_EndDate = "@EndDate";
            public const string ProcedureParameter_DefectName = "@DefectName";
            public const string ProcedureParameter_Occurrance = "@Occurrance";
            public const string ProcedureParameter_PasswordHash = "@PasswordHash";
            public const string ProcedureParameter_PasswordSalt = "@PasswordSalt";
            public const string ProcedureParameter_UserId = "@UserId";
            public const string ProcedureParameter_IsToday = "@isToday";
            public const string ProcedureParameter_AvailabilityId = "@AvailabilityId";
            public const string ProcedureParameter_Name = "@Name";
            public const string ProcedureParameter_Email = "@Email";
            public const string ProcedureParameter_RoleId = "@RoleId";
            public const string ProcedureParameter_PlantId = "@PlantId";
            public const string ProcedureParameter_CreatedBy = "@CreatedBy";
            public const string ProcedureParameter_CreatedDate = "@CreatedDate";
            public const string ProcedureParameter_Token = "@Token";
            public const string ProcedureParameter_TokenId = "@TokenId";
            public const string ProcedureParameter_userEmail = "@userEmail";

        }
    }
}
