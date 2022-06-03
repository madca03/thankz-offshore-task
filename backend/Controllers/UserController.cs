using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using backend.Managers;
using backend.Models.Database;
using backend.Models.RequestModels;
using backend.Models.ResponseModels;
using backend.References;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : BaseAPIController
    {
        private readonly UserManager userManager;
        
        public UserController(UserManager UserManager)
        {
            userManager = UserManager;
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] GetUsersRequestModel req)
        {
            var res = new GetUsersResponseModel
            {
                Users = userManager.GetUsers(req.Limit ?? 0, req.Offset ?? 0)
            };

            if (req.CSV == true)
            {
                using var stream = new MemoryStream();
                using var writer = new StreamWriter(stream);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(res.Users);
                writer.Flush();

                var bytes = stream.ToArray();
                var epochTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                return File(bytes, "text/csv", $"users_{epochTime}.csv");
            }
            
            return HandleGenericSuccess(res);
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUser([FromRoute] string userId)
        {
            var user = userManager.GetUserById(userId);
            var res = new GetUserResponseModel
            {
                User = user
            };

            return HandleGenericSuccess(res);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequestModel req)
        {
            try
            {
                var user = userManager.CreateUser(req);
                var res = new GetUserResponseModel
                {
                    User = user
                };

                return HandleGenericSuccess(res);
            }
            catch (SqlException ex)
            {
                if (ex.Number == StoredProcedureStatusCodes.USERNAME_EXISTS)
                {
                    return HandleGenericFailure(apiStatus: APIStatusCodes.UsernameExists);
                }
                
                throw;
            }
        }

        [HttpPost]
        [Route("addfromfile")]
        public IActionResult CreateUserFromFile([FromForm] CreateUserFromFileRequestModel req)
        {
            CsvReader csv = null;

            if (req.FileType == FileTypes.CSV)
            {
                var reader = new StreamReader(req.UserFile.OpenReadStream());
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };
                csv = new CsvReader(reader, csvConfig);
            }
            else if (req.FileType == FileTypes.XLSX)
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };

                var excelParser = new ExcelParser(req.UserFile.OpenReadStream(), null, csvConfig);
                csv = new CsvReader(excelParser);
            }
            
            var records = csv.GetRecords<User>();
            
            var res = new GetUsersResponseModel
            {
                Users = new List<User>()
            };
            
            foreach (var rec in records)
            {
                if (string.IsNullOrEmpty(rec.Username)) continue;
                if (string.IsNullOrEmpty(rec.Firstname)) continue;
                if (string.IsNullOrEmpty(rec.Lastname)) continue;

                var newUserReq = new CreateUserRequestModel
                {
                    Username = rec.Username,
                    Firstname = rec.Firstname,
                    Middlename = rec.Middlename,
                    Lastname = rec.Lastname,
                    Contact = rec.Contact
                };

                try
                {
                    var newUser = userManager.CreateUser(newUserReq);
                    res.Users.Add(newUser);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == StoredProcedureStatusCodes.USERNAME_EXISTS) continue;
                    throw;
                }
            }

            return HandleGenericSuccess(res);
        }
        
        [HttpPut]
        [Route("{userId}")]
        public IActionResult UpdateUser(string userId, [FromBody] UpdateUserRequestModel req)
        {
            req.UserId = userId;
            
            try
            {
                var user = userManager.UpdateUser(req);
                var res = new GetUserResponseModel
                {
                    User = user
                };

                return HandleGenericSuccess(res);
            }
            catch (SqlException ex)
            {
                if (ex.Number == StoredProcedureStatusCodes.USER_NOT_FOUND)
                {
                    return HandleGenericFailure(apiStatus: APIStatusCodes.UserNotFound);
                }
                
                if (ex.Number == StoredProcedureStatusCodes.USERNAME_EXISTS)
                {
                    return HandleGenericFailure(apiStatus: APIStatusCodes.UsernameExists);
                }

                throw;
            }
        }

        [HttpDelete]
        [Route("{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                var user = userManager.DeleteUser(userId);
                var res = new GetUserResponseModel
                {
                    User = user
                };

                return HandleGenericSuccess(res);
            }
            catch (SqlException ex)
            {
                if (ex.Number == StoredProcedureStatusCodes.USER_NOT_FOUND)
                {
                    return HandleGenericFailure(apiStatus: APIStatusCodes.UserNotFound);
                }

                throw;
            }
        }
    }
}