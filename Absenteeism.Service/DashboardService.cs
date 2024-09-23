using Absenteeism.CommonFunction;
using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;
using Absenteeism.Repository.Interface;
using Absenteeism.Service.Interface;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Absenteeism.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly AppSettings _appSettings;
        public DashboardService(IDashboardRepository dashboardRepository, IOptions<AppSettings> appSettings)
        {
            _dashboardRepository = dashboardRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<GetAvailability> CreateAvailability(tblAvailability tblAvailability)
        {
            try
            {
                return await _dashboardRepository.CreateAvailability(tblAvailability);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "CreateAvailability");
                throw;
            }
        }

        public async Task DeletetblAvailability(int Id)
        {
            try
            {
                await _dashboardRepository.DeletetblAvailability(Id);
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "DeletetblAvailability");
                throw;
            }
        }

        public async Task DeletetblDefect(int Id)
        {
            try
            {
                await _dashboardRepository.DeletetblDefect(Id);
            }
            catch (Exception ex)
            {
    
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "DeletetblDefect");
                throw;
            }
        }

        public async Task DeleteWorkHistory(int Id)
        {
            try
            {
                await _dashboardRepository.DeleteWorkHistory(Id);
            }
            catch (Exception ex)
            {
             
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "DeleteWorkHistory");
                throw;
            }
        }

        public async Task<GettblCellMemberStatus> CreateCellMember(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                return await _dashboardRepository.CreatetblCellMemberStatus(inputtblCellMemberStatus);
            }
            catch (Exception ex)
            {
           
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "CreateCellMember");
                throw;
            }
        }

        public async Task<List<GetAvailability>> GettblAvailability(string StartDate, string EndDate, int PlantId, int UserId)
        {
            try
            {
                return await _dashboardRepository.GetAvailability(StartDate, EndDate, PlantId, UserId);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettblAvailability");
                throw;
            }
        }

        public async Task<List<GettblDefect>> GettblDefect(int PlantId)
        {
            try
            {
                return await _dashboardRepository.GettblDefect(PlantId);
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettblDefect");
                throw;
            }
        }

        public async Task<List<GetWorkingHistory>> GettblWorkingHistory(string StartDate, string EndDate, int PlantId, int UserId)
        {
            try
            {
                return await _dashboardRepository.GettblWorkingHistory(StartDate, EndDate, PlantId, UserId);
            }
            catch (Exception ex)
            {
            
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettblWorkingHistory");
                throw;
            }
        }

        public async Task<GettblDefect> SavetblDefect(tblDefect tblDefect)
        {
            try
            {
                return await _dashboardRepository.SavetblDefect(tblDefect);
            }
            catch (Exception ex)
            {
            
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "SavetblDefect");
                throw;
            }
        }

        public async Task<GetWorkingHistory> SavetblWorkingHistory(tblWorkingHistory tblWorkingHistory)
        {
            try
            {
                return await _dashboardRepository.CreatetblWorkingHistory(tblWorkingHistory);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "SavetblWorkingHistory");
                throw;
            }
        }

        public async Task<GetAvailability> UpdateAvailability(UpdateAvailiability inputtblAvailability)
        {
            try
            {
                return await _dashboardRepository.UpdateAvailability(inputtblAvailability);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "UpdateAvailability");
                throw;
            }
        }

        public async Task<GettblDefect> UpdatetblDefect(tblDefect tblDefect)
        {
            try
            {
                return await _dashboardRepository.UpdatetblDefect(tblDefect);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "UpdatetblDefect");
                throw;
            }
        }

        public async Task<GetWorkingHistory> UpdatetblWorkingHistory(tblWorkingHistory tblWorkingHistory)
        {
            try
            {
                return await _dashboardRepository.UpdatetblWorkingHistory(tblWorkingHistory);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "UpdatetblWorkingHistory");
                throw;
            }
        }

        public async Task DeleteCellMember(int Id)
        {
            try
            {
                await _dashboardRepository.DeleteCellMemberStatus(Id);
            }
            catch (Exception ex)
            {
             
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "DeleteCellMember");
                throw;
            }
        }

        public async Task<GettblCellMemberStatus> UpdatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                return await _dashboardRepository.UpdatetblCellMemberStatus(inputtblCellMemberStatus);
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "UpdatetblCellMemberStatus");
                throw;
            }
        }

        public async Task<List<GettblCellMemberStatus>> GettblCellMemberStatus(string StartDate, string EndDate, int ShiftId, int PlantId)
        {
            try
            {
                return await _dashboardRepository.GettblCellMemberStatus(StartDate, EndDate, ShiftId, PlantId);
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettblCellMemberStatus");
                throw;
            }
        }

        public async Task<List<GettblAllocation>> CreatetblAllocation(InputAllocation inputtblAllocation)
        {
            try
            {
                return await _dashboardRepository.CreatetblAllocation(inputtblAllocation);
            }
            catch (Exception ex)
            {
              
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "CreatetblAllocation");
                throw;
            }
        }

        public async Task<GettblAllocation> UpdatetblAllocation(tblAllocation inputtblAllocation)
        {
            try
            {
                return await _dashboardRepository.UpdatetblAllocation(inputtblAllocation);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "UpdatetblAllocation");
                throw;
            }
        }

        public async Task DeletetblAllocation(int Id)
        {
            try
            {
                await _dashboardRepository.DeleteAllocation(Id);
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "DeletetblAllocation");
                throw;
            }
        }

        public async Task<List<GettblAllocation>> GettblAllocation(int PlantId, int UserId, string StartDate, string EndDate, int? ShiftId)
        {
            try
            {
                return await _dashboardRepository.GettblAllocation(PlantId, UserId, StartDate, EndDate, ShiftId);
            }
            catch (Exception ex)
            {
             
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettblAllocation");
                throw;
            }
        }

        public async Task<List<GettblAllocation>> GettodayAllocationCellMember(int PlantId, int UserId)
        {
            try
            {
                return await _dashboardRepository.GettodayAllocationCellMember(PlantId, UserId);
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardService", "GettodayAllocationCellMember");
                throw;
            }
        }
    }
}
