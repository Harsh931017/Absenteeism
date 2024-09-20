using Absenteeism.CommonFunction;
using Absenteeism.CommonFunction.Wrappers;
using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Output;
using Absenteeism.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AbsenteeismAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ICommonService _commonService;
        private readonly IDashboardService _dashboardService;
        private readonly IMasterService _masterService;
        public DashboardController(IOptions<AppSettings> appSettings, IDashboardService dashboardService, IMasterService masterService)
        {
            _appSettings = appSettings.Value;
            _dashboardService = dashboardService;
            _masterService = masterService;
        }

        #region Work History

        /// <summary>
        /// Create Work History
        /// </summary>
        /// <param name="inputCheckToken"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.CreateWorkHistory), NonAction]
        public async Task<ApiResponse<GetWorkingHistory>> CreateWorkHistory(tblWorkingHistory inputCheckToken)
        {
            try
            {
                var checkUserDepartment = await _masterService.CheckDepartment(inputCheckToken.DepartmentId);
                if (!checkUserDepartment)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStation = await _masterService.CheckStation(inputCheckToken.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputCheckToken.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkPlant = await _masterService.CheckPlant(inputCheckToken.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var tblWorkingHistory = await _dashboardService.SavetblWorkingHistory(inputCheckToken);
                return new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = true,
                    Data = tblWorkingHistory,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "CreateWorkHistory");
                return new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }


        /// <summary>
        /// Delete Work History
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.DeleteWorkHistory), NonAction]
        public async Task<ApiResponse<tblWorkingHistory>> DeleteWorkHistory(int Id)
        {
            try
            {
                var checkWorkHistory = await _masterService.CheckWorkHistory(Id);
                if (!checkWorkHistory)
                {
                    return new ApiResponse<tblWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_WorkHistory,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                await _dashboardService.DeleteWorkHistory(Id);
                return new ApiResponse<tblWorkingHistory>
                {
                    Succeeded = true,
                    Data = null,
                    Message = Constant.Messages.DeleteWorkHistory,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "DeleteWorkHistory");
                return new ApiResponse<tblWorkingHistory>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }


        /// <summary>
        /// Update Work History
        /// </summary>
        /// <param name="inputCheckToken"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdateWorkHistory), NonAction]
        public async Task<ApiResponse<GetWorkingHistory>> UpdateWorkHistory(tblWorkingHistory inputCheckToken)
        {
            try
            {
                var checkWorkHistory = await _masterService.CheckWorkHistory(inputCheckToken.Id);
                if (!checkWorkHistory)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_WorkHistory,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkPlant = await _masterService.CheckPlant(inputCheckToken.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserDepartment = await _masterService.CheckDepartment(inputCheckToken.DepartmentId);
                if (!checkUserDepartment)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStation = await _masterService.CheckStation(inputCheckToken.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputCheckToken.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var updateWorkingHistory = await _dashboardService.UpdatetblWorkingHistory(inputCheckToken);
                return new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = true,
                    Data = updateWorkingHistory,
                    Message = Constant.Messages.UpdateWorkHistory,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "UpdateWorkHistory");
                return new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Get Work History (CellMember)
        /// </summary>
        /// <param name="StartDate">(yyyy-MM-dd)</param>
        /// <param name="EndDate">(yyyy-MM-dd)</param>
        /// <param name="PlantId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetWorkHistory)]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> GetWorkHistory(string? StartDate, string? EndDate, int PlantId, int UserId)
        {
            try
            {

                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkCellMember = await _masterService.CheckCellMember(UserId);
                if (!checkCellMember)
                {
                    return BadRequest(new ApiResponse<GetWorkingHistory>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }
                var getWorkingHistory = await _dashboardService.GettblWorkingHistory(StartDate, EndDate, PlantId, UserId);
                return Ok(new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = true,
                    Data = getWorkingHistory,
                    Message = getWorkingHistory.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GetWorkHistory");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GetWorkingHistory>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }
        #endregion

        #region Defect

        /// <summary>
        /// Create Defect
        /// </summary>
        /// <param name="inputtblDefect"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.CreateDefect), NonAction]
        public async Task<IActionResult> CreateDefect(tblDefect inputtblDefect)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(inputtblDefect.PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkStation = await _masterService.CheckStation(inputtblDefect.StationId);
                if (!checkStation)
                {
                    return BadRequest(new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkShift = await _masterService.CheckShift(inputtblDefect.ShiftId);
                if (!checkShift)
                {
                    return BadRequest(new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var savetblDefect = await _dashboardService.SavetblDefect(inputtblDefect);
                return Ok(new ApiResponse<GettblDefect>
                {
                    Succeeded = true,
                    Data = savetblDefect,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "CreateDefect");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GettblDefect>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }

        /// <summary>
        /// Delete tbl Defect
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.DeletetblDefect), NonAction]
        public async Task<ApiResponse<tblDefect>> DeletetblDefect(int Id)
        {
            try
            {
                var checkDefect = await _masterService.CheckDefect(Id);
                if (!checkDefect)
                {
                    return new ApiResponse<tblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Defect,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                await _dashboardService.DeletetblDefect(Id);
                return new ApiResponse<tblDefect>
                {
                    Succeeded = true,
                    Data = null,
                    Message = Constant.Messages.DeleteDefect,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "DeletetblDefect");
                return new ApiResponse<tblDefect>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Update TblDefect
        /// </summary>
        /// <param name="inputtblDefect"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdatetblDefect), NonAction]
        public async Task<ApiResponse<GettblDefect>> UpdatetblDefect(tblDefect inputtblDefect)
        {
            try
            {
                var checkDefect = await _masterService.CheckDefect(inputtblDefect.Id);
                if (!checkDefect)
                {
                    return new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Defect,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkStation = await _masterService.CheckStation(inputtblDefect.StationId);
                if (!checkStation)
                {
                    return new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }
                var checkUserShift = await _masterService.CheckShift(inputtblDefect.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkPlant = await _masterService.CheckPlant(inputtblDefect.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var updateDefect = await _dashboardService.UpdatetblDefect(inputtblDefect);
                return new ApiResponse<GettblDefect>
                {
                    Succeeded = true,
                    Data = updateDefect,
                    Message = Constant.Messages.UpdateDefect,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "UpdatetblDefect");
                return new ApiResponse<GettblDefect>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Get tblDefect (Cell Member)
        /// </summary>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GettblDefect)]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> GettblDefect(int PlantId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GettblDefect>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var gettblDefects = await _dashboardService.GettblDefect(PlantId);
                return Ok(new ApiResponse<GettblDefect>
                {
                    Succeeded = true,
                    Data = gettblDefects,
                    Message = gettblDefects.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GettblDefect");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GettblDefect>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }
        #endregion

        #region Availability


        /// <summary>
        /// Create tblAvailability
        /// </summary>
        /// <param name="inputtblAvailability"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.CreateAvailability), NonAction]
        public async Task<ApiResponse<GetAvailability>> CreateAvailability(tblAvailability inputtblAvailability)
        {
            try
            {
                var checkCellMember = await _masterService.CheckCellMember(inputtblAvailability.UserId);
                if (!checkCellMember)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkPlant = await _masterService.CheckPlant(inputtblAvailability.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserDepartment = await _masterService.CheckDepartment(inputtblAvailability.DepartmentId);
                if (!checkUserDepartment)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStation = await _masterService.CheckStation(inputtblAvailability.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputtblAvailability.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStatus = await _masterService.CheckStatus(inputtblAvailability.StatusId);
                if (!checkUserStatus)
                {
                    return new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Status,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }


                var tblAvailability = await _dashboardService.CreateAvailability(inputtblAvailability);
                return new ApiResponse<GetAvailability>
                {
                    Succeeded = true,
                    Data = tblAvailability,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "CreateAvailability");
                return new ApiResponse<GetAvailability>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Delete tblAvailability
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.DeletetblAvailability), NonAction]
        public async Task<ApiResponse<tblAvailability>> DeletetblAvailability(int Id)
        {
            try
            {
                var checkAvailability = await _masterService.CheckAvailabililty(Id);
                if (!checkAvailability)
                {
                    return new ApiResponse<tblAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Availabililty,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                await _dashboardService.DeletetblAvailability(Id);
                return new ApiResponse<tblAvailability>
                {
                    Succeeded = true,
                    Data = null,
                    Message = Constant.Messages.DeletetblAvailability,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "DeletetblAvailability");
                return new ApiResponse<tblAvailability>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Update tblAvailability(Cell Member)
        /// </summary>
        /// <param name="inputtblAvailability">IsToday for today then 1 otherwise for tomorrow 0</param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdatetblAvailability)]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> UpdatetblAvailability(UpdateAvailiability inputtblAvailability)
        {
            try
            {
                var checkCellMember = await _masterService.CheckCellMember(inputtblAvailability.userId);
                if (!checkCellMember)
                {
                    return BadRequest(new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }



                //var checkAvailability = await _masterService.CheckAvailabililty(inputtblAvailability.Id);
                //if (!checkAvailability)
                //{
                //    return new ApiResponse<GetAvailability>
                //    {
                //        Succeeded = false,
                //        Data = null,
                //        Message = Constant.Messages.Invalid_Availabililty,
                //        Outputcode = Enums.HttpStatusCode.BadRequest,
                //        ResponseType = Constant.ResponseType.Error
                //    };
                //}


                //var checkUserDepartment = await _masterService.CheckDepartment(inputtblAvailability.DepartmentId);
                //if (!checkUserDepartment)
                //{
                //    return new ApiResponse<GetAvailability>
                //    {
                //        Succeeded = false,
                //        Data = null,
                //        Message = Constant.Messages.Invalid_Department,
                //        Outputcode = Enums.HttpStatusCode.BadRequest,
                //        ResponseType = Constant.ResponseType.Error
                //    };
                //}

                //var checkUserStation = await _masterService.CheckStation(inputtblAvailability.StationId);
                //if (!checkUserStation)
                //{
                //    return new ApiResponse<GetAvailability>
                //    {
                //        Succeeded = false,
                //        Data = null,
                //        Message = Constant.Messages.Invalid_Station,
                //        Outputcode = Enums.HttpStatusCode.BadRequest,
                //        ResponseType = Constant.ResponseType.Error
                //    };
                //}

                //var checkUserShift = await _masterService.CheckShift(inputtblAvailability.ShiftId);
                //if (!checkUserShift)
                //{
                //    return new ApiResponse<GetAvailability>
                //    {
                //        Succeeded = false,
                //        Data = null,
                //        Message = Constant.Messages.Invalid_Shift,
                //        Outputcode = Enums.HttpStatusCode.BadRequest,
                //        ResponseType = Constant.ResponseType.Error
                //    };
                //}

                var checkUserStatus = await _masterService.CheckStatus(inputtblAvailability.statusId);
                if (!checkUserStatus)
                {
                    return BadRequest(new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Status,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }


                var updatetblAvailability = await _dashboardService.UpdateAvailability(inputtblAvailability);
                return Ok(new ApiResponse<GetAvailability>
                {
                    Succeeded = true,
                    Data = updatetblAvailability,
                    Message = Constant.Messages.UpdatetblAvailability,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "UpdatetblAvailability");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GetAvailability>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }

        /// <summary>
        /// Get tblAvailability
        /// </summary>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GettblAvailability),NonAction]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> GettblAvailability(string? StartDate, string? EndDate, int PlantId, int UserId)
        {
            try
            {
                var checkCellMember = await _masterService.CheckCellMember(UserId);
                if (!checkCellMember)
                {
                    return BadRequest(new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GetAvailability>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var getAvailability = await _dashboardService.GettblAvailability(StartDate, EndDate, PlantId, UserId);
                return Ok(new ApiResponse<GetAvailability>
                {
                    Succeeded = true,
                    Data = getAvailability,
                    Message = getAvailability.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GettblAvailability");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GetAvailability>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }
        #endregion

        #region Cell Member


        /// <summary>
        /// Create Cell Member
        /// </summary>
        /// <param name="inputtblCellMemberStatus"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.CreateCellMember), NonAction]
        public async Task<ApiResponse<GettblCellMemberStatus>> CreateCellMember(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(inputtblCellMemberStatus.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }


                var checkUserStation = await _masterService.CheckStation(inputtblCellMemberStatus.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputtblCellMemberStatus.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStatus = await _masterService.CheckStatus(inputtblCellMemberStatus.StatusId);
                if (!checkUserStatus)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Status,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkEmployee = await _masterService.CheckEmployee(inputtblCellMemberStatus.EmployeeId);
                if (!checkEmployee)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Employee,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var insertedcellMember = await _dashboardService.CreateCellMember(inputtblCellMemberStatus);
                return new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = true,
                    Data = insertedcellMember,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Delete Cell Member
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.DeleteCellMember), NonAction]
        public async Task<ApiResponse<tblCellMemberStatus>> DeleteCellMember(int Id)
        {
            try
            {
                var checkCellMember = await _masterService.CheckCellMember(Id);
                if (!checkCellMember)
                {
                    return new ApiResponse<tblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                await _dashboardService.DeleteCellMember(Id);
                return new ApiResponse<tblCellMemberStatus>
                {
                    Succeeded = true,
                    Data = null,
                    Message = Constant.Messages.DeletetblCellMemberStatus,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<tblCellMemberStatus>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Update tblCellMemberStatus 
        /// </summary>
        /// <param name="inputtblCellMemberStatus"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdatetblCellMemberStatus), NonAction]
        public async Task<ApiResponse<GettblCellMemberStatus>> UpdatetblCellMemberStatus(tblCellMemberStatus inputtblCellMemberStatus)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(inputtblCellMemberStatus.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkCellMember = await _masterService.CheckCellMember(inputtblCellMemberStatus.Id);
                if (!checkCellMember)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStation = await _masterService.CheckStation(inputtblCellMemberStatus.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputtblCellMemberStatus.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserStatus = await _masterService.CheckStatus(inputtblCellMemberStatus.StatusId);
                if (!checkUserStatus)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Status,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkEmployee = await _masterService.CheckEmployee(inputtblCellMemberStatus.EmployeeId);
                if (!checkEmployee)
                {
                    return new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Employee,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var updatetblAvailability = await _dashboardService.UpdatetblCellMemberStatus(inputtblCellMemberStatus);
                return new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = true,
                    Data = updatetblAvailability,
                    Message = Constant.Messages.UpdatetblCellMemberStatus,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Get tblCellMemberStatus (Cell Member)
        /// </summary>
        /// <param name="StartDate">(yyyy-MM-dd)</param>
        /// <param name="EndDate">(yyyy-MM-dd)</param>
        /// <param name="ShiftId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.tblCellMemberStatus)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GettblCellMemberStatus(string? StartDate, string? EndDate, int ShiftId, int PlantId)
        {
            try
            {
                if (ShiftId != 0)
                {

                    var checkUserShift = await _masterService.CheckShift(ShiftId);
                    if (!checkUserShift)
                    {
                        return BadRequest(new ApiResponse<GettblCellMemberStatus>
                        {
                            Succeeded = false,
                            Data = null,
                            Message = Constant.Messages.Invalid_Shift,
                            Outputcode = Enums.HttpStatusCode.BadRequest,
                            ResponseType = Constant.ResponseType.Error
                        });
                    }
                }


                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GettblCellMemberStatus>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var gettblCellMemberStatuses = await _dashboardService.GettblCellMemberStatus(StartDate, EndDate, ShiftId, PlantId);
                return Ok(new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = true,
                    Data = gettblCellMemberStatuses,
                    Message = gettblCellMemberStatuses.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GettblCellMemberStatus");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GettblCellMemberStatus>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }
        #endregion

        #region Allocation
        /// <summary>
        /// Create the Allocation(Supervisor)
        /// </summary>
        /// <param name="inputtblAllocation"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.CreateAllocation)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> CreateAllocation(InputAllocation inputtblAllocation)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(inputtblAllocation.PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<List<GettblAllocation>>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }


                var checkUserStation = await _masterService.CheckStation(inputtblAllocation.StationId);
                if (!checkUserStation)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkUserShift = await _masterService.CheckShift(inputtblAllocation.ShiftId);
                if (!checkUserShift)
                {
                    return BadRequest(new ApiResponse<List<GettblAllocation>>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkdepartment = await _masterService.CheckDepartment(inputtblAllocation.DepartmentId);
                if (!checkdepartment)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                //var checkCellMember = await _masterService.CheckCellMember(inputtblAllocation.CellMemberId);
                //if (!checkCellMember)
                //{
                //    return BadRequest(new ApiResponse<GettblAllocation>
                //    {
                //        Succeeded = false,
                //        Data = null,
                //        Message = Constant.Messages.Invalid_CellMember,
                //        Outputcode = Enums.HttpStatusCode.BadRequest,
                //        ResponseType = Constant.ResponseType.Error
                //    });
                //}

                var checkCellMembers = await _masterService.CheckCellMembers(inputtblAllocation.CellMemberIds);
                if (!checkCellMembers)
                {
                    return BadRequest(new ApiResponse<List<GettblAllocation>>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkCellMembersallocated = await _masterService.CheckCellMembersAllocated(inputtblAllocation.CellMemberIds, inputtblAllocation.CreatedDate);
                if (checkCellMembersallocated)
                {
                    return BadRequest(new ApiResponse<List<GettblAllocation>>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMemberAllocated,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkEmployee = await _masterService.CheckEmployee(inputtblAllocation.CreatedBy);
                if (!checkEmployee)
                {
                    return BadRequest(new ApiResponse<List<GettblAllocation>>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Employee,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var insertedtblAllocation = await _dashboardService.CreatetblAllocation(inputtblAllocation);
                return Ok(new ApiResponse<List<GettblAllocation>>
                {
                    Succeeded = true,
                    Data = insertedtblAllocation,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "CreateAllocation");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<List<GettblAllocation>>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }

        /// <summary>
        /// Delete allocation
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.DeleteAllocation), NonAction]
        public async Task<ApiResponse<string>> DeleteAllocation(int Id)
        {
            try
            {
                var checkallocation = await _masterService.CheckAllocation(Id);
                if (!checkallocation)
                {
                    return new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Allocation,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                await _dashboardService.DeletetblAllocation(Id);
                return new ApiResponse<string>
                {
                    Succeeded = true,
                    Data = null,
                    Message = Constant.Messages.DeletetblAllocation,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Update the allocation
        /// </summary>
        /// <param name="inputtblAllocation"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdatetblAllocation), NonAction]
        public async Task<ApiResponse<GettblAllocation>> UpdatetblAllocation(tblAllocation inputtblAllocation)
        {
            try
            {
                var checkAllocation = await _masterService.CheckAllocation(inputtblAllocation.Id);
                if (!checkAllocation)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Allocation,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }
                var checkPlant = await _masterService.CheckPlant(inputtblAllocation.PlantId);
                if (!checkPlant)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }


                var checkUserStation = await _masterService.CheckStation(inputtblAllocation.StationId);
                if (!checkUserStation)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Station,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkUserShift = await _masterService.CheckShift(inputtblAllocation.ShiftId);
                if (!checkUserShift)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Shift,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkdepartment = await _masterService.CheckDepartment(inputtblAllocation.DepartmentId);
                if (!checkdepartment)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkCellMember = await _masterService.CheckCellMember(inputtblAllocation.CellMemberId);
                if (!checkCellMember)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }

                var checkEmployee = await _masterService.CheckEmployee(inputtblAllocation.CreatedBy);
                if (!checkEmployee)
                {
                    return new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Employee,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }


                var updatetblAllocation = await _dashboardService.UpdatetblAllocation(inputtblAllocation);
                return new ApiResponse<GettblAllocation>
                {
                    Succeeded = true,
                    Data = updatetblAllocation,
                    Message = Constant.Messages.UpdatetblCellMemberStatus,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Delete
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<GettblAllocation>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                };
            }
        }

        /// <summary>
        /// Get tblAllocation and Dashboard for (Supervisor)
        /// </summary>
        /// <param name="PlantId"></param>
        /// <param name="UserId">Supervisor Id</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ShiftId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GettblAllocation)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GettblAllocation(int PlantId, int UserId, string? StartDate, string? EndDate, int? ShiftId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var userId = await _masterService.CheckEmployee(UserId);
                if (!userId)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Employee,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                if (ShiftId > 0)
                {
                    var checkUserShift = await _masterService.CheckShift((int)ShiftId);
                    if (!checkUserShift)
                    {
                        return BadRequest(new ApiResponse<GettblAllocation>
                        {
                            Succeeded = false,
                            Data = null,
                            Message = Constant.Messages.Invalid_Shift,
                            Outputcode = Enums.HttpStatusCode.BadRequest,
                            ResponseType = Constant.ResponseType.Error
                        });
                    }
                }

                var gettblallocation = await _dashboardService.GettblAllocation(PlantId, UserId, StartDate, EndDate, ShiftId);
                return Ok(new ApiResponse<GettblAllocation>
                {
                    Succeeded = true,
                    Data = gettblallocation,
                    Message = gettblallocation.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GettblAllocation");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GettblAllocation>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }


        /// <summary>
        /// Get Today Allocation for (Cell Member)
        /// </summary>
        /// <param name="PlantId"></param>
        /// <param name="UserId">Cell Member Id</param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GettodayAllocationForCellMember)]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> GettodayAllocationForCellMember(int PlantId, int UserId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkCellMember = await _masterService.CheckCellMember(UserId);
                if (!checkCellMember)
                {
                    return BadRequest(new ApiResponse<GettblAllocation>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_CellMember,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }



                var gettblallocation = await _dashboardService.GettodayAllocationCellMember(PlantId, UserId);
                return Ok(new ApiResponse<GettblAllocation>
                {
                    Succeeded = true,
                    Data = gettblallocation,
                    Message = gettblallocation.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {

                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "DashboardController", "GettodayAllocationForCellMember");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GettblAllocation>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }
        #endregion
    }
}
