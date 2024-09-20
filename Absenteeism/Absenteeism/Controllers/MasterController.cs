using Absenteeism.CommonFunction;
using Absenteeism.CommonFunction.Wrappers;
using Absenteeism.Models.Domain;
using Absenteeism.Models.Input;
using Absenteeism.Models.Notification;
using Absenteeism.Models.Output;
using Absenteeism.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Absenteeism.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MasterController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMasterService _masterService;
        private readonly ICommonService _commonService;
        public MasterController(IMasterService masterService, IOptions<AppSettings> appSettings, ICommonService commonService)
        {
            _masterService = masterService;
            _appSettings = appSettings.Value;
            _commonService = commonService;
        }

        /// <summary>
        /// Submit the Register
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.Register), AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] InputUserRegister request)
        {

            try
            {
                var checkUserNumber = await _masterService.CheckUserNumber(request.UserNumber);
                if (checkUserNumber)
                {
                    return BadRequest(new ApiResponse<tblLogin>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Duplicate_UserNumber,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }


                var checkUserEmail = await _masterService.CheckUserEmail(request.Email);
                if (checkUserEmail)
                {
                    return BadRequest(new ApiResponse<tblLogin>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Duplicate_UserEmail,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkUserRole = await _masterService.CheckRole(request.RoleId);
                if (!checkUserRole)
                {
                    return BadRequest(new ApiResponse<tblLogin>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Role,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkPlant = await _masterService.CheckPlant(request.PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<tblLogin>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                CommonFunctionSet.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);

                var user = new tblUser
                {
                    Name = request.UserName,
                    Email = request.Email,
                    RoleId = request.RoleId,
                    PlantId = request.PlantId
                };

                var users = await _masterService.AddtblUser(user);

                var userLogin = new tblLogin
                {
                    UserNumber = request.UserNumber,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserId = users.Id
                };

                var AddLoginUser = await _masterService.AddLoginUser(userLogin);

                return Ok(new ApiResponse<tblLogin>
                {
                    Succeeded = true,
                    Data = AddLoginUser,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
               
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "Register");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError,new ApiResponse<tblLogin>
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
        /// Login 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(Constant.Routing.Login)]
        public async Task<IActionResult> Login([FromBody] UserRegisterDto request)
        {
            try
            {
                GetUserPassword getUserpassword = new GetUserPassword();
                var user = await _masterService.GetUser(request.UserNumber);

                if (user == null)
                {
                    return BadRequest(new ApiResponse<GetUserPassword>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Credentials,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }


                if (user.RoleId != request.RoleId)
                {
                    return BadRequest(new ApiResponse<GetUserPassword>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Incorrect_Role,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }


                //LDAP IS COMMENT
                //if (user.RoleId == 2)
                //{
                //    if (!CommonFunctionSet.IsAuthenticated(request.UserNumber, request.Password, _appSettings.ldap.ADPath))
                //    {
                //        return new ApiResponse<GetUserPassword>
                //        {
                //            Succeeded = false,
                //            Data = null,
                //            Message = Constant.Messages.Invalid_Credentials,
                //            Outputcode = Enums.HttpStatusCode.OK,
                //            ResponseType = Constant.ResponseType.Insert
                //        };
                //    }
                //}
                //else
                //{
                if (!CommonFunctionSet.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest(new ApiResponse<GetUserPassword>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Credentials,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }
                //}


                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub,_appSettings.Jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(Constant.StoreProcedureAndParameters.Id,user.Id.ToString()),
                        new Claim(Constant.StoreProcedureAndParameters.UserNumber,user.UserNumber),
                        new Claim(Constant.StoreProcedureAndParameters.RoleId,user.RoleId.ToString())
                    };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _appSettings.Jwt.Issuer,
                    _appSettings.Jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signIn
                    );

                var tokens = new JwtSecurityTokenHandler().WriteToken(token);

                getUserpassword = new GetUserPassword()
                {
                    Id = user.Id,
                    Role = user.Role,
                    RoleId = user.RoleId,
                    Token = tokens,
                    UserId = user.UserId,
                    UserNumber = user.UserNumber
                };

                return Ok(new ApiResponse<GetUserPassword>
                {
                    Succeeded = true,
                    Data = getUserpassword,
                    Message = Constant.Messages.Success,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
             
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "Login");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<GetUserPassword>
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
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetRoles), AllowAnonymous]

        public async Task<IActionResult> GetRoles(int PlantId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var roles = await _masterService.GetRoles(PlantId);
                if (roles.Count() > 0)
                {
                    return Ok(new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = roles,
                        Message = roles.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                        Outputcode = Enums.HttpStatusCode.OK,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = null,
                        Message = Constant.Messages.NoRecord,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.NotFound
                    });
                }

            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "GetRoles");
                return StatusCode(500,new ApiResponse<string>
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
        /// Get Shift (Supervisor)
        /// </summary>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetShift)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GetShift(int PlantId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }


                var shifts = await _masterService.GetShift(PlantId);

                return Ok(new ApiResponse<string>
                {
                    Succeeded = true,
                    Data = shifts,
                    Message = shifts.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "GetShift");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<string>
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
        /// Get Cell Member Based Upon PlantId(Supervisor)
        /// </summary>
        /// <param name="PlantId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetCellMember)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GetCellMember(int PlantId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }


                var cellmembers = await _masterService.GetCellMember(PlantId);

                return Ok(new ApiResponse<string>
                {
                    Succeeded = true,
                    Data = cellmembers,
                    Message = cellmembers.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
                
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "GetCellMember");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<string>
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
        /// Get Departments (Supervisor)
        /// </summary>
        /// <param name="PlantId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetDepartment)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GetDepartment(int PlantId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var cellmembers = await _masterService.GetDepartments(PlantId);

                return Ok(new ApiResponse<string>
                {
                    Succeeded = true,
                    Data = cellmembers,
                    Message = cellmembers.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
           
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "GetDepartment");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError,new ApiResponse<string>
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
        /// Get Stations By DepartmenId (Supervisor)
        /// </summary>
        /// <param name="PlantId"></param>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.GetStation)]
        //[Authorize(Roles = "Supervisior")]
        public async Task<IActionResult> GetStation(int PlantId, int DepartmentId)
        {
            try
            {
                var checkPlant = await _masterService.CheckPlant(PlantId);
                if (!checkPlant)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Plant,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var checkDepartment = await _masterService.CheckDepartment(DepartmentId);
                if (!checkDepartment)
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Department,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }

                var stations = await _masterService.GetStation(PlantId, DepartmentId);

                return Ok(new ApiResponse<string>
                {
                    Succeeded = true,
                    Data = stations,
                    Message = stations.Count() > 0 ? Constant.Messages.CommonSuccess : Constant.Messages.NoRecord,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert
                });
            }
            catch (Exception ex)
            {
          
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "GetStation");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError,new ApiResponse<string>
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
        /// Forgot Password
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        [HttpGet(Constant.Routing.ForgotPasswordSendEmail), NonAction]
        public async Task<ApiResponse<string>> ForgotPasswordSendEmail(string UserNumber)
        {
            try
            {
                if (await _masterService.GetUserEmail(UserNumber) is not { } userEmail)
                {
                    return new ApiResponse<string>
                    {
                        Data = null,
                        Message = Constant.Messages.UserNumber,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error,
                        Succeeded = false
                    };
                }

                string HtmlTemplate = GetHTMLTemplate().ToString();
                await _commonService.SendMail(userEmail.Id, userEmail.Email, null, HtmlTemplate);
                return new ApiResponse<string>
                {
                    Data = userEmail,
                    Message = Constant.Messages.CommonSuccess,
                    Outputcode = Enums.HttpStatusCode.OK,
                    ResponseType = Constant.ResponseType.Insert,
                    Succeeded = true
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Password 
        /// </summary>
        /// <param name="inputCheckToken"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.UpdatePassword), NonAction]
        public async Task<ApiResponse<string>> UpdatePassword(InputCheckToken inputCheckToken)
        {
            try
            {
                if (!string.Equals(inputCheckToken.newPassword, inputCheckToken.confirmPassword, StringComparison.OrdinalIgnoreCase))
                {
                    return new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.ConfirmAndNewPassword,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    };
                }
                var CheckToken = await _masterService.CheckTokenValidity(inputCheckToken);
                if (CheckToken != null)
                {
                    CommonFunctionSet.CreatePasswordHash(inputCheckToken.newPassword, out string passwordHash, out string passwordSalt);
                    var inputUpdatePassword = new InputUpdatePassword
                    {
                        TokenId = CheckToken.Id,
                        UserId = CheckToken.UserId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };
                    await _masterService.UpdatePassword(inputUpdatePassword);
                    return new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = null,
                        Message = Constant.Messages.ChangePassword,
                        Outputcode = Enums.HttpStatusCode.OK,
                        ResponseType = Constant.ResponseType.Insert
                    };
                }
                else
                {
                    return new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = null,
                        Message = Constant.Messages.NotValidToken,
                        Outputcode = Enums.HttpStatusCode.NotFound,
                        ResponseType = Constant.ResponseType.NotFound
                    };
                }

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
        /// Change Cell Member Password (Cell Member)
        /// </summary>
        /// <param name="inputChangePassword"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.ChangePassword)]
        //[Authorize(Roles = "Cell Member")]
        public async Task<IActionResult> ChangeCellMemberPassword(InputChangePassword inputChangePassword)
        {
            try
            {
                if (!string.Equals(inputChangePassword.NewPassword, inputChangePassword.ConfirmPassword, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.ConfirmAndNewPassword,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.Error
                    });
                }
                var checkNumber = await _masterService.GetUser(inputChangePassword.UserNumber);
                if (checkNumber == null || !CommonFunctionSet.VerifyPasswordHash(inputChangePassword.PreviousPassword, checkNumber.PasswordHash, checkNumber.PasswordSalt))
                {
                    return Ok(new ApiResponse<string>
                    {
                        Succeeded = false,
                        Data = null,
                        Message = Constant.Messages.Invalid_Credentials,
                        Outputcode = Enums.HttpStatusCode.OK,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }
                if (checkNumber != null)
                {
                    CommonFunctionSet.CreatePasswordHash(inputChangePassword.NewPassword, out string passwordHash, out string passwordSalt);
                    var inputUpdatePassword = new ChangePassword
                    {
                        UserId = checkNumber.UserId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };
                    await _masterService.ChangePasswordCellMember(inputUpdatePassword);
                    return Ok(new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = null,
                        Message = Constant.Messages.ChangePassword,
                        Outputcode = Enums.HttpStatusCode.OK,
                        ResponseType = Constant.ResponseType.Insert
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Succeeded = true,
                        Data = null,
                        Message = Constant.Messages.UserNumber,
                        Outputcode = Enums.HttpStatusCode.BadRequest,
                        ResponseType = Constant.ResponseType.NotFound
                    });
                }

            }
            catch (Exception ex)
            {
             
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "ChangeCellMemberPassword");
                return StatusCode((int)Enums.HttpStatusCode.InternalServerError, new ApiResponse<string>
                {
                    Succeeded = false,
                    Data = ex,
                    Message = Constant.Messages.InternalError,
                    Outputcode = Enums.HttpStatusCode.InternalServerError,
                    ResponseType = Constant.ResponseType.Error
                });
            }
        }

        [NonAction]
        public string GetHTMLTemplate()
        {
            var html = System.IO.File.ReadAllText(@"./assets/index.html");
            return html;
        }


        /// <summary>
        /// Send Notification(Cell Member) Currently Under Testing
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(Constant.Routing.PushNotification)]
        public async Task<IActionResult> SendNotification([FromBody] NotificationModel request)
        {
            try
            {
                bool result = await _commonService.SendNotificationAsync(request);
                if (result)

                    return Ok("Notification sent successfully");
                return StatusCode(500, "Error sending notification");
            }
            catch (Exception ex)
            {
           
                await CommonFunctionSet.ErrorLog(ex, _appSettings.ConnectionStrings.MyConnections, "MasterController", "SendNotification");
                throw;
            }
          
        }
    }

}
