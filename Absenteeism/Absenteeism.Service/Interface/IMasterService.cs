using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;

namespace Absenteeism.Service.Interface
{
    public interface IMasterService
    {
        Task<List<tblRoles>> GetRoles(int PlantId);
        Task<List<tblShift>> GetShift(int PlantId);
        Task<List<GetDepartments>> GetDepartments(int PlantId);
        Task<List<GetStations>> GetStation(int PlantId, int DepartmentId);
        Task<List<GetCellMember>> GetCellMember(int PlantId);
        Task<bool> CheckUserNumber(string userNumber);
        Task<bool> CheckUserEmail(string userEmail);
        Task<bool> CheckRole(int RoleId);
        Task<bool> CheckDepartment(int DepartmentId);
        Task<bool> CheckStation(int StationId);
        Task<bool> CheckShift(int ShiftId);
        Task<bool> CheckWorkHistory(int Id);
        Task<bool> CheckDefect(int Id);

        Task<bool> CheckStatus(int Id);
        Task<bool> CheckAvailabililty(int Id);
        Task<bool> CheckEmployee(int Id);
        Task<bool> CheckCellMember(int Id);
        Task<tblLogin> AddLoginUser(tblLogin tbllogin);
        Task<tblLogin> GetUser(string UserNumber);
        Task<GetUserEmailId> GetUserEmail(string UserNumber);
        Task<tblUser> AddtblUser(tblUser tblUser);
        Task<tblForgotPassword> CheckTokenValidity(InputCheckToken inputCheckToken);
        Task UpdatePassword(InputUpdatePassword inputUpdatePassword);
        Task ChangePasswordCellMember(ChangePassword inputUpdatePassword);
        Task<bool> CheckPlant(int Id);

        Task<bool> CheckAllocation(int Id);

        Task<List<tblNotification>> GetNotification(int UserId);
        Task UpdateNotification(int UserId);
        Task<tblNotification> InsertNotification(int UserId, int AvailabilityId);

        Task<bool> CheckCellMembers(int[] Id);
        Task<bool> CheckCellMembersAllocated(int[] Id,DateTime CreatedDate);
    }
}
