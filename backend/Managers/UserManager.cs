using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using backend.Models.Database;
using backend.Models.RequestModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.Managers
{
    public class UserManager
    {
        private readonly AppDbContext db;
        
        public UserManager(AppDbContext Db)
        {
            db = Db;
        }

        public List<User> GetUsers(int limit, int offset)
        {
            return db.User.FromSqlRaw($"exec sp_GetUser @Limit = {limit}, @Skip = {offset}").ToList();
        }

        public User GetUserById(string userId)
        {
            return db.User.FromSqlRaw($"exec sp_GetUser @UserId = '{userId}'").ToList().FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return db.User.FromSqlRaw($"exec sp_GetUser @Username = '{username}'").ToList().FirstOrDefault();
        }

        public User CreateUser(CreateUserRequestModel req)
        {
            var storedProcedure = new StringBuilder();
            storedProcedure.Append("exec sp_CreateUser ");
            storedProcedure.Append($"@Username = '{req.Username}'");
            storedProcedure.Append($", @Firstname = '{req.Firstname}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Middlename) ? "" : $", @Middlename = '{req.Middlename}'");
            storedProcedure.Append($", @Lastname = '{req.Lastname}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Contact) ? "" : $", @Contact = '{req.Contact}'");

            var user = db.User.FromSqlRaw(storedProcedure.ToString()).ToList().FirstOrDefault();
            return user;
        }

        public User UpdateUser(UpdateUserRequestModel req)
        {
            var storedProcedure = new StringBuilder();
            storedProcedure.Append("exec sp_UpdateUser ");
            storedProcedure.Append($"@UserId = '{req.UserId}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Username) ? "" : $", @Username = '{req.Username}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Firstname) ? "" : $", @Firstname = '{req.Firstname}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Middlename) ? "" : $", @Middlename = '{req.Middlename}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Lastname) ? "" : $", @Lastname = '{req.Lastname}'");
            storedProcedure.Append(string.IsNullOrEmpty(req.Contact) ? "" : $", @Contact = '{req.Contact}'");

            var user = db.User.FromSqlRaw(storedProcedure.ToString()).ToList().FirstOrDefault();
            return user;
        }

        public User DeleteUser(string userId)
        {
            var user = db.User.FromSqlRaw($"exec sp_DeleteUser '{userId}'").ToList().FirstOrDefault();
            return user;
        }
    }
}