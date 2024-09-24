using Absenteeism.CommonFunction;
using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Notification;
using Absenteeism.Models.Output;
using Absenteeism.Repository.Interface;
using Absenteeism.Service.Interface;
using Azure.Core;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Absenteeism.Service
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _masterRepository;
        private readonly ICommonService _commonService;
        private readonly AppSettings _appSettings;
        public MasterService(IMasterRepository masterRepository, IOptions<AppSettings> appSettings, ICommonService commonService)
        {
            _masterRepository = masterRepository;
            _appSettings = appSettings.Value;
            _commonService = commonService;
        }

        public async Task<tblLogin> AddLoginUser(tblLogin tbllogin)
        {
            try
            {
                return await _masterRepository.AddLoginUser(tbllogin);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "AddLoginUser");
                throw;
            }
        }

        public async Task<bool> CheckUserNumber(string userNumber)
        {
            try
            {
                return await _masterRepository.CheckUserNumber(userNumber);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckUserNumber");
                throw;
            }
        }

        public async Task<List<tblRoles>> GetRoles(int PlantId)
        {
            try
            {
                return await _masterRepository.GetRoles(PlantId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetRoles");
                throw;
            }
        }

        public async Task<tblLogin> GetUser(string userNumber)
        {
            try
            {
                return await _masterRepository.GetUser(userNumber);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetUser");
                throw;
            }
        }

        public async Task<tblUser> AddtblUser(tblUser tblUser)
        {
            try
            {
                return await _masterRepository.AddtblUser(tblUser);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "AddtblUser");
                throw;
            }
        }

        public async Task<GetUserEmailId> GetUserEmail(string UserNumber)
        {
            try
            {
                return await _masterRepository.GetEmailUser(UserNumber);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetUserEmail");
                throw;
            }
        }

        public async Task<tblForgotPassword> CheckTokenValidity(InputCheckToken inputCheckToken)
        {
            try
            {
                return await _masterRepository.CheckTokenValidity(inputCheckToken);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckTokenValidity");
                throw;
            }
        }

        public async Task UpdatePassword(InputUpdatePassword inputUpdatePassword)
        {
            try
            {
                await _masterRepository.UpdatePassword(inputUpdatePassword);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "UpdatePassword");
                throw;
            }
        }

        public async Task<bool> CheckUserEmail(string userEmail)
        {
            try
            {
                return await _masterRepository.CheckUserEmail(userEmail);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckUserEmail");
                throw;
            }
        }

        public async Task<bool> CheckRole(int RoleId)
        {
            try
            {
                return await _masterRepository.CheckRole(RoleId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckRole");
                throw;
            }
        }

        public async Task<bool> CheckDepartment(int DepartmentId)
        {
            try
            {
                return await _masterRepository.CheckDepartment(DepartmentId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckDepartment");
                throw;
            }
        }

        public async Task<bool> CheckStation(int StationId)
        {
            try
            {
                return await _masterRepository.CheckStation(StationId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckStation");
                throw;
            }
        }

        public async Task<bool> CheckShift(int ShiftId)
        {
            try
            {
                return await _masterRepository.CheckShift(ShiftId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckShift");
                throw;
            }
        }

        public async Task<bool> CheckWorkHistory(int Id)
        {
            try
            {
                return await _masterRepository.CheckWorkHistory(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckWorkHistory");
                throw;
            }
        }

        public async Task<bool> CheckDefect(int Id)
        {
            try
            {
                return await _masterRepository.CheckDefect(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckDefect");
                throw;
            }
        }

        public async Task<bool> CheckStatus(int Id)
        {
            try
            {
                return await _masterRepository.CheckStatus(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckStatus");
                throw;
            }
        }

        public async Task<bool> CheckAvailabililty(int Id)
        {
            try
            {
                return await _masterRepository.CheckAvailabililty(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckAvailabililty");
                throw;
            }
        }

        public async Task<bool> CheckEmployee(int Id)
        {
            try
            {
                return await _masterRepository.CheckEmployee(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckEmployee");
                throw;
            }
        }

        public async Task<bool> CheckCellMember(int Id)
        {
            try
            {
                return await _masterRepository.CheckCellMember(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckCellMember");
                throw;
            }
        }

        public async Task<bool> CheckCellMembers(int[] Id)
        {
            try
            {
                return await _masterRepository.CheckCellMembers(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckCellMembers");
                throw;
            }
        }

        public async Task<bool> CheckPlant(int Id)
        {
            try
            {
                return await _masterRepository.CheckPlant(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckPlant");
                throw;
            }
        }

        public async Task<List<tblShift>> GetShift(int PlantId)
        {
            try
            {
                return await _masterRepository.GetShift(PlantId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetShift");
                throw;
            }
        }

        public async Task ChangePasswordCellMember(ChangePassword inputUpdatePassword)
        {
            try
            {
                await _masterRepository.UpdatePasswordCellMemberUser(inputUpdatePassword);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "ChangePasswordCellMember");
                throw;
            }
        }

        public async Task<List<GetCellMember>> GetCellMember(int PlantId)
        {
            try
            {
                return await _masterRepository.GetCellMember(PlantId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetCellMember");
                throw;
            }
        }

        public async Task<List<GetDepartments>> GetDepartments(int PlantId)
        {
            try
            {
                return await _masterRepository.GetDepartment(PlantId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetDepartments");
                throw;
            }
        }

        public async Task<List<GetStations>> GetStation(int PlantId, int DepartmentId)
        {
            try
            {
                return await _masterRepository.GetStation(PlantId, DepartmentId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetStation");
                throw;
            }
        }

        public async Task<bool> CheckAllocation(int Id)
        {
            try
            {
                return await _masterRepository.CheckAllocation(Id);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckAllocation");
                throw;
            }
        }

        public async Task<List<tblNotification>> GetNotification(int UserId)
        {
            try
            {
                return await _masterRepository.GetNotification(UserId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "GetNotification");
                throw;
            }
        }

        public async Task<List<tblNotification>> UpdateNotification(int UserId)
        {
            try
            {
                return await _masterRepository.UpdateNotification(UserId);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "UpdateNotification");
                throw;
            }
        }

        public async Task<tblNotification> InsertNotification(NotificationModel request)
        {
            try
            {
                bool result = await _commonService.SendNotificationAsync(request);
                if (result)
                {
                    return await _masterRepository.InsertNotification(request.UserId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "InsertNotification");
                throw;
            }
        }

        public async Task<bool> CheckCellMembersAllocated(int[] Id, DateTime CreatedDate)
        {
            try
            {
                return await _masterRepository.CheckCellMembersAllocated(Id, CreatedDate);
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterService", "CheckCellMembersAllocated");
                throw;
            }
        }
    }
}
