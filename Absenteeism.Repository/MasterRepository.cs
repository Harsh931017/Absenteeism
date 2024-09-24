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
    public class MasterRepository : IMasterRepository
    {
        private readonly AppSettings _appSettings;
        public IMapper _mapper;
        public MasterRepository(IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public async Task<tblLogin> AddLoginUser(tblLogin tbllogin)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.UserNumber, tbllogin.UserNumber);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordHash, tbllogin.PasswordHash);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordSalt, tbllogin.PasswordSalt);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, tbllogin.UserId);
                    var records = (await con.QueryFirstAsync<tblLogin>(Constant.StoreProcedureAndParameters.sp_InsertLoginUser, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));

                    var getrecords = await GetUser(records.UserNumber);

                    return getrecords;
                }

            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "AddLoginUser");
                throw;
            }
        }

        public async Task<bool> CheckUserNumber(string userNumber)
        {
            try
            {
                bool checkUserNumber = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (!String.IsNullOrWhiteSpace(userNumber))
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.UserNumber, userNumber);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckuserNumber, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkUserNumber = true;
                    }
                }

                return checkUserNumber;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckUserNumber");
                throw;
            }
        }

        public async Task<List<tblRoles>> GetRoles(int PlantId)
        {
            try
            {
                List<tblRoles> getRoles = new List<tblRoles>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@PlantId", PlantId);
                    getRoles = (await con.QueryAsync<tblRoles>(Constant.StoreProcedureAndParameters.sp_GetRoles, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }

                return getRoles;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetRoles");
                throw;
            }
        }

        public async Task<tblLogin> GetUser(string UserNumber)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.UserNumber, UserNumber);
                    var getUser = (await con.QueryFirstOrDefaultAsync<tblLogin>(Constant.StoreProcedureAndParameters.sp_GetuserLogin, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    return getUser;
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetUser");
                throw;
            }
        }

        public Task<string> Login()
        {
            //try
            //{
            //    List<tblRoles> getRoles = new List<tblRoles>();
            //    using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
            //    {
            //        DynamicParameters dynamicParameters = new DynamicParameters();

            //        getRoles = (await con.QueryAsync<tblRoles>(Constant.StoreProcedureAndParameters.sp_GetRoles, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
            //    }

            //    return getRoles;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            return null;
        }

        public async Task<tblUser> AddtblUser(tblUser tblUser)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Name, tblUser.Name);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Email, tblUser.Email);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_RoleId, tblUser.RoleId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, tblUser.PlantId);
                    var getUser = (await con.QueryFirstOrDefaultAsync<tblUser>(Constant.StoreProcedureAndParameters.sp_InsertLogin, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    return getUser;
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "AddtblUser");
                throw;
            }
        }

        public async Task<GetUserEmailId> GetEmailUser(string userNumber)
        {
            try
            {

                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.UserNumber, userNumber);
                    var getUser = (await con.QueryFirstOrDefaultAsync<GetUserEmailId>(Constant.StoreProcedureAndParameters.sp_GetUserEmail, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    return getUser;
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetEmailUser");
                throw;
            }
        }

        public async Task SaveRandomPassword(string Token, int userId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, userId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Token, Token);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_SaveRandomPassword, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "SaveRandomPassword");
                throw;
            }
        }

        public async Task<tblForgotPassword> CheckTokenValidity(InputCheckToken inputCheckToken)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, inputCheckToken.UserId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Token, inputCheckToken.Token);
                    return await con.QueryFirstOrDefaultAsync<tblForgotPassword>(Constant.StoreProcedureAndParameters.sp_CheckTokenValidity, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckTokenValidity");
                throw;
            }
        }

        public async Task UpdatePassword(InputUpdatePassword inputUpdatePassword)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, inputUpdatePassword.UserId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordSalt, inputUpdatePassword.PasswordSalt);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordHash, inputUpdatePassword.PasswordHash);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_TokenId, inputUpdatePassword.TokenId);

                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_UpdatePassword, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "UpdatePassword");
                throw;
            }
        }

        public async Task<bool> CheckUserEmail(string userEmail)
        {
            try
            {
                bool checkuserEmail = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (!String.IsNullOrWhiteSpace(userEmail))
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_userEmail, userEmail);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckUserEmail, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuserEmail = true;
                    }
                }

                return checkuserEmail;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckUserEmail");
                throw;
            }
        }

        public async Task<bool> CheckRole(int RoleId)
        {
            try
            {
                bool checkuserRole = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (RoleId != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_RoleId, RoleId);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckRole, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuserRole = true;
                    }
                }

                return checkuserRole;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckRole");
                throw;
            }
        }

        public async Task<bool> CheckDepartment(int DepartmentId)
        {
            try
            {
                bool checkuser = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (DepartmentId != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, DepartmentId);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckDepartment, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuser = true;
                    }
                }

                return checkuser;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckDepartment");
                throw;
            }
        }

        public async Task<bool> CheckStation(int StationId)
        {
            try
            {
                bool checkuserStation = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (StationId != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_StationId, StationId);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckStation, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuserStation = true;
                    }
                }

                return checkuserStation;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckStation");
                throw;
            }
        }

        public async Task<bool> CheckShift(int ShiftId)
        {
            try
            {
                bool checkuserShift = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (ShiftId != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_ShiftId, ShiftId);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckShift, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuserShift = true;
                    }
                }

                return checkuserShift;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckShift");
                throw;
            }
        }

        public async Task<bool> CheckWorkHistory(int Id)
        {
            try
            {
                bool checkuserWorkHistory = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckWorkHistory, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkuserWorkHistory = true;
                    }
                }

                return checkuserWorkHistory;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckWorkHistory");
                throw;
            }
        }

        public async Task<bool> CheckDefect(int Id)
        {
            try
            {
                bool checkDefect = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckDefect, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkDefect = true;
                    }
                }

                return checkDefect;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckDefect");
                throw;
            }
        }

        public async Task<bool> CheckStatus(int Id)
        {
            try
            {
                bool checkStatus = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckStatus, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkStatus = true;
                    }
                }

                return checkStatus;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckStatus");
                throw;
            }
        }

        public async Task<bool> CheckAvailabililty(int Id)
        {
            try
            {
                bool checkAvailabililty = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckAvailability, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkAvailabililty = true;
                    }
                }

                return checkAvailabililty;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckAvailabililty");
                throw;
            }
        }

        public async Task<bool> CheckEmployee(int Id)
        {
            try
            {
                bool checkEmployee = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckUser, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkEmployee = true;
                    }
                }

                return checkEmployee;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckEmployee");
                throw;
            }
        }

        public async Task<bool> CheckCellMember(int Id)
        {
            try
            {
                bool checkCellMember = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckCellMember, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkCellMember = true;
                    }
                }

                return checkCellMember;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckCellMember");
                throw;
            }
        }

        public async Task<bool> CheckCellMembers(int[] Id)
        {
            try
            {
                bool checkCellMember = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id.Count() > 0)
                    {
                        string Ids = string.Join(",", Id);
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Ids);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckCellMembers, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkCellMember = true;
                    }
                }

                return checkCellMember;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckCellMembers");
                throw;
            }
        }

        public async Task<bool> CheckCellMembersAllocated(int[] Id, DateTime CreatedDate)
        {
            try
            {
                bool checkCellMember = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id.Count() > 0)
                    {
                        string Ids = string.Join(",", Id);
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Ids);
                    }
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_CreatedDate, CreatedDate);
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckCellMembersAlreadyAllocated, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 0)
                    {
                        checkCellMember = true;
                    }
                }

                return checkCellMember;
            }
            catch (Exception ex)
            {
                ;
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckCellMembersAllocated");
                throw;
            }
        }

        public async Task<bool> CheckAllocation(int Id)
        {
            try
            {
                bool checkCellMember = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckAllocation, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkCellMember = true;
                    }
                }

                return checkCellMember;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckAllocation");
                throw;
            }
        }

        public async Task<bool> CheckPlant(int Id)
        {
            try
            {
                bool checkCellMember = false;
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    if (Id != 0)
                    {
                        dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_Id, Id);
                    }
                    int result = (await con.QueryFirstAsync<int>(Constant.StoreProcedureAndParameters.sp_CheckPlant, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                    if (result == 1)
                    {
                        checkCellMember = true;
                    }
                }

                return checkCellMember;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "CheckPlant");
                throw;
            }
        }

        public async Task<List<tblShift>> GetShift(int PlantId)
        {
            try
            {
                List<tblShift> getShift = new List<tblShift>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    getShift = (await con.QueryAsync<tblShift>(Constant.StoreProcedureAndParameters.sp_GetShift, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }

                return getShift;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetShift");
                throw;
            }
        }


        public async Task<List<GetCellMember>> GetCellMember(int PlantId)
        {
            try
            {
                List<GetCellMember> getCellMember = new List<GetCellMember>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    getCellMember = (await con.QueryAsync<GetCellMember>(Constant.StoreProcedureAndParameters.sp_GetCellMember, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }

                return getCellMember;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetCellMember");
                throw;
            }
        }


        public async Task<List<GetDepartments>> GetDepartment(int PlantId)
        {
            try
            {
                List<GetDepartments> getDepartments = new List<GetDepartments>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    getDepartments = (await con.QueryAsync<GetDepartments>(Constant.StoreProcedureAndParameters.sp_GetDepartments, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }

                return getDepartments;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetDepartment");
                throw;
            }
        }

        public async Task<List<GetStations>> GetStation(int PlantId, int DepartmentId)
        {
            try
            {
                List<GetStations> getStations = new List<GetStations>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PlantId, PlantId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_DepartmentId, DepartmentId);
                    getStations = (await con.QueryAsync<GetStations>(Constant.StoreProcedureAndParameters.sp_GetStationByDepartmentId, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }

                return getStations;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetStation");
                throw;
            }
        }

        public async Task UpdatePasswordCellMemberUser(ChangePassword inputChangePassword)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, inputChangePassword.UserId);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordHash, inputChangePassword.PasswordHash);
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_PasswordSalt, inputChangePassword.PasswordSalt);
                    await con.ExecuteAsync(Constant.StoreProcedureAndParameters.sp_UpdatePasswordCellUser, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "UpdatePasswordCellMemberUser");
                throw;
            }
        }

        public async Task<List<tblNotification>> GetNotification(int UserId)
        {
            try
            {
                List<tblNotification> getNotification = new List<tblNotification>();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    getNotification = (await con.QueryAsync<tblNotification>(Constant.StoreProcedureAndParameters.sp_GetNotificationByUserId, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }
                return getNotification;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "GetNotification");
                throw;
            }
        }

        public async Task<List<tblNotification>> UpdateNotification(int UserId)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    return (await con.QueryAsync<tblNotification>(Constant.StoreProcedureAndParameters.sp_UpdateNotification, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure)).ToList();
                }
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "UpdateNotification");
                throw;
            }
        }

        public async Task<tblNotification> InsertNotification(int UserId)
        {
            try
            {
                tblNotification getNotification = new tblNotification();
                using (IDbConnection con = new SqlConnection(_appSettings.ConnectionStrings.MyConnections))
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add(Constant.StoreProcedureAndParameters.ProcedureParameter_UserId, UserId);
                    getNotification = (await con.QueryFirstOrDefaultAsync<tblNotification>(Constant.StoreProcedureAndParameters.sp_CreateNotification, param: dynamicParameters, commandTimeout: 3000, commandType: CommandType.StoredProcedure));
                }
                return getNotification;
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterRepository", "InsertNotification");
                throw;
            }
        }
    }
}
