using Absenteeism.CommonFunction;
using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;
using Absenteeism.Repository.Interface;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Absenteeism.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppSettings _appSettings;
        public IMapper _mapper;
        public DashboardRepository(IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<GetAvailability> CreateAvailability(tblAvailability tblAvailability)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, tblAvailability.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, tblAvailability.DepartmentId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, tblAvailability.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, tblAvailability.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StatusId, tblAvailability.StatusId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblAvailability.PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, tblAvailability.UserId);
                    var saverecord = await con.QueryFirstOrDefaultAsync<tblAvailability>(Constant.StoreProcedureAndParameters.sp_CreatetblAvailability, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GetAvailabilityById(saverecord.Id);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "CreateAvailability");
                throw;
            }
        }

        public async Task<GetAvailability> GetAvailabilityById(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);

                    return await con.QueryFirstOrDefaultAsync<GetAvailability>(Constant.StoreProcedureAndParameters.sp_GettblAvailabilityById, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GetAvailabilityById");
                throw;
            }
        }

        public async Task<GettblCellMemberStatus> CreatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, inputtblCellMemberStatus.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EmployeeId, inputtblCellMemberStatus.EmployeeId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, inputtblCellMemberStatus.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, inputtblCellMemberStatus.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StatusId, inputtblCellMemberStatus.StatusId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, inputtblCellMemberStatus.PlantId);
                    var saverecord = await con.QueryFirstOrDefaultAsync<tblCellMemberStatus>(Constant.StoreProcedureAndParameters.sp_CreatetblCellMemberStatus, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GettblCellMemberStatusById(saverecord.Id);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "CreatetblCellMemberStatus");
                throw;
            }
        }


        public async Task<List<GettblAllocation>> CreatetblAllocation(InputAllocation inputtblAllocation)
        {
            try
            {
                List<GettblAllocation> gettblAllocations = new List<GettblAllocation>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    string cellMemberIds = string.Join(",", inputtblAllocation.CellMemberIds);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CellMemberId, cellMemberIds);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, inputtblAllocation.DepartmentId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, inputtblAllocation.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, inputtblAllocation.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CreatedBy, inputtblAllocation.CreatedBy);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, inputtblAllocation.PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CreatedDate, inputtblAllocation.CreatedDate);
                    var saverecord = await con.QueryAsync<tblAllocation>(Constant.StoreProcedureAndParameters.sp_CreatetblAllocation_v2, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                    foreach (var item in saverecord)
                    {
                        GettblAllocation gettblAllocation = new();
                        gettblAllocation = await GettblAllocationById(item.Id);
                        gettblAllocations.Add(gettblAllocation);
                    }
                    return gettblAllocations;
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "CreatetblAllocation");
                throw;
            }
        }

        public async Task<GetWorkingHistory> CreatetblWorkingHistory(tblWorkingHistory tblWorkingHistory)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, tblWorkingHistory.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, tblWorkingHistory.DepartmentId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, tblWorkingHistory.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, tblWorkingHistory.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblWorkingHistory.PlantId);
                    var saverecords = await con.QueryFirstOrDefaultAsync<tblWorkingHistory>(Constant.StoreProcedureAndParameters.sp_CreateWorkHistory, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                    return await GettblWorkingHistoryById(saverecords.Id);
                }
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "CreatetblWorkingHistory");
                throw;
            }
        }


        public async Task<GetWorkingHistory> GettblWorkingHistoryById(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);

                    return await con.QueryFirstOrDefaultAsync<GetWorkingHistory>(Constant.StoreProcedureAndParameters.sp_GetWorkingHistoryById, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblWorkingHistoryById");
                throw;
            }
        }

        public async Task DeleteCellMemberStatus(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_DeletetblCellMemberStatus, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "DeleteCellMemberStatus");
                throw;
            }
        }

        public async Task DeletetblAvailability(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_DeletetblAvailability, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "DeletetblAvailability");
                throw;
            }
        }

        public async Task DeletetblDefect(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_DeletetblDefect, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "DeletetblDefect");
                throw;
            }
        }

        public async Task DeleteWorkHistory(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_DeleteWorkHistory, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "DeleteWorkHistory");
                throw;
            }
        }

        public async Task<List<GetAvailability>> GetAvailability(string StartDate, string EndDate, int PlantId, int UserId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StartDate, StartDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EndDate, EndDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    return (List<GetAvailability>)await con.QueryAsync<GetAvailability>(Constant.StoreProcedureAndParameters.sp_GettblAvailability, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GetAvailability");
                throw;
            }
        }

        public async Task<List<GettblCellMemberStatus>> GettblCellMemberStatus(string StartDate, string EndDate, int ShiftId, int PlantId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StartDate, StartDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EndDate, EndDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    if (ShiftId > 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, ShiftId);
                    }
                    return (List<GettblCellMemberStatus>)await con.QueryAsync<GettblCellMemberStatus>(Constant.StoreProcedureAndParameters.sp_GettblCellMemberStatus, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblCellMemberStatus");
                throw;
            }
        }


        public async Task<GettblCellMemberStatus> GettblCellMemberStatusById(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);

                    return await con.QueryFirstOrDefaultAsync<GettblCellMemberStatus>(Constant.StoreProcedureAndParameters.sp_GettblCellMemberStatusById, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblCellMemberStatusById");
                throw;
            }
        }

        public async Task<GettblAllocation> GettblAllocationById(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);

                    return await con.QueryFirstOrDefaultAsync<GettblAllocation>(Constant.StoreProcedureAndParameters.sp_GettblAllocationById, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblAllocationById");
                throw;
            }
        }

        public async Task<List<GettblDefect>> GettblDefect(int PlantId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    return (List<GettblDefect>)await con.QueryAsync<GettblDefect>(Constant.StoreProcedureAndParameters.sp_GettblDefect, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblDefect");
                throw;
            }
        }

        public async Task<List<GetWorkingHistory>> GettblWorkingHistory(string StartDate, string EndDate, int PlantId, int UserId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StartDate, StartDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EndDate, EndDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    return (List<GetWorkingHistory>)await con.QueryAsync<GetWorkingHistory>(Constant.StoreProcedureAndParameters.sp_GetWorkingHistory_v2, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblWorkingHistory");
                throw;
            }
        }

        public async Task<GettblDefect> SavetblDefect(tblDefect tblDefect)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, tblDefect.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DefectName, tblDefect.DefectName);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Occurrance, tblDefect.Occurrance);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, tblDefect.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblDefect.PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, tblDefect.StationId);
                    var saverecords = await con.QueryFirstOrDefaultAsync<GettblDefect>(Constant.StoreProcedureAndParameters.sp_CreateDefect, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                    return await GettblDefectById(saverecords.Id);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "SavetblDefect");
                throw;
            }
        }


        public async Task<GettblDefect> GettblDefectById(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);

                    return await con.QueryFirstOrDefaultAsync<GettblDefect>(Constant.StoreProcedureAndParameters.sp_GetDefectById, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblDefectById");
                throw;
            }
        }

        public async Task<GetAvailability> UpdateAvailability(UpdateAvailiability inputtblAvailability)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StatusId, inputtblAvailability.statusId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, inputtblAvailability.userId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_IsToday, inputtblAvailability.isToday);
                    var updateavailability = await con.QueryFirstOrDefaultAsync<GetAvailability>(Constant.StoreProcedureAndParameters.sp_UpdateAvailability_v2, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GetAvailabilityById(updateavailability.Id);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "UpdateAvailability");
                throw;
            }
        }

        public async Task<GettblAllocation> UpdatetblAllocation(tblAllocation inputtblAllocation)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, inputtblAllocation.Id);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CellMemberId, inputtblAllocation.CellMemberId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, inputtblAllocation.DepartmentId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, inputtblAllocation.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, inputtblAllocation.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CreatedBy, inputtblAllocation.CreatedBy);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, inputtblAllocation.PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CreatedDate, inputtblAllocation.CreatedDate);
                    var saverecord = await con.QueryFirstOrDefaultAsync<tblAllocation>(Constant.StoreProcedureAndParameters.sp_UpdateAllocation, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GettblAllocationById(saverecord.Id);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "UpdatetblAllocation");
                throw;
            }
        }

        public async Task<GettblCellMemberStatus> UpdatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, inputtblCellMemberStatus.Id);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, inputtblCellMemberStatus.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, inputtblCellMemberStatus.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EmployeeId, inputtblCellMemberStatus.EmployeeId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, inputtblCellMemberStatus.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StatusId, inputtblCellMemberStatus.StatusId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, inputtblCellMemberStatus.PlantId);
                    var updaterecord = await con.QueryFirstOrDefaultAsync<tblCellMemberStatus>(Constant.StoreProcedureAndParameters.sp_UpdateCellMemberStatus, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GettblCellMemberStatusById(updaterecord.Id);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "UpdatetblCellMemberStatus");
                throw;
            }
        }

        public async Task<GettblDefect> UpdatetblDefect(tblDefect tblDefect)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, tblDefect.Id);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, tblDefect.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Occurrance, tblDefect.Occurrance);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DefectName, tblDefect.DefectName);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, tblDefect.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblDefect.PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, tblDefect.StationId);
                    var updatedefect = await con.QueryFirstOrDefaultAsync<tblDefect>(Constant.StoreProcedureAndParameters.sp_UpdateDefect, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);

                    return await GettblDefectById(updatedefect.Id);
                }
            }
            catch (Exception ex)
            {
          
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "UpdatetblDefect");
                throw;
            }
        }

        public async Task<GetWorkingHistory> UpdatetblWorkingHistory(tblWorkingHistory tblWorkingHistory)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, tblWorkingHistory.Id);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Date, tblWorkingHistory.Date);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, tblWorkingHistory.DepartmentId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, tblWorkingHistory.StationId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, tblWorkingHistory.ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblWorkingHistory.PlantId);
                    return await con.QueryFirstOrDefaultAsync<GetWorkingHistory>(Constant.StoreProcedureAndParameters.sp_UpdateWorkHistory, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "UpdatetblWorkingHistory");
                throw;
            }
        }

        public async Task DeleteAllocation(int Id)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_DeletetblAllocation, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "DeleteAllocation");
                throw;
            }
        }
        public async Task<List<GettblAllocation>> GettblAllocation(int PlantId, int UserId, string StartDate, string EndDate, int? ShiftId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StartDate, StartDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_EndDate, EndDate);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, ShiftId == 0 ? null : ShiftId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    return (List<GettblAllocation>)await con.QueryAsync<GettblAllocation>(Constant.StoreProcedureAndParameters.sp_GettblAllocations, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettblAllocation");
                throw;
            }
        }

        public async Task<List<GettblAllocation>> GettodayAllocationCellMember(int PlantId, int UserId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    return (List<GettblAllocation>)await con.QueryAsync<GettblAllocation>(Constant.StoreProcedureAndParameters.sp_GettblAllocationsCellMember, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardRepository", "GettodayAllocationCellMember");
                throw;
            }
        }
    }
}
