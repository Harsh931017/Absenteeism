using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;

namespace Absenteeism.Repository.Interface
{
    public interface IMasterRepository
    {
        Task<List<tblRoles>> GetRoles(int PlantId);
        Task<List<tblShift>> GetShift(int PlantId);
        Task<string> Login();

        Task<bool> CheckUserNumber(string userNumber);
        Task<tblLogin> AddLoginUser(tblLogin tbllogin);
        Task<tblLogin> GetUser(string userNumber);
        Task<GetUserEmailId> GetEmailUser(string userNumber);
        Task<tblUser> AddtblUser(tblUser tblUser);
        Task SaveRandomPassword(string password, int userId);
        Task<tblForgotPassword> CheckTokenValidity(InputCheckToken inputCheckToken);
        Task UpdatePassword(InputUpdatePassword inputUpdatePassword);
        Task UpdatePasswordCellMemberUser(ChangePassword inputChangePassword);

        Task<bool> CheckUserEmail(string userNumber);

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
        Task<bool> CheckPlant(int Id);
        Task<List<GetCellMember>> GetCellMember(int PlantId);
        Task<List<GetDepartments>> GetDepartment(int PlantId);
        Task<List<GetStations>> GetStation(int PlantId, int DepartmentId);
        Task<bool> CheckAllocation(int Id);
        Task<List<tblNotification>> GetNotification(int UserId);
        Task<List<tblNotification>> UpdateNotification(int UserId);
        Task<tblNotification> InsertNotification(int UserId);
        Task<bool> CheckCellMembers(int[] Id);
        Task<bool> CheckCellMembersAllocated(int[] Id, DateTime CreatedDate);
    }
}
