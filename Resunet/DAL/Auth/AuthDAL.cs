using System;
using Dapper;
using Drapper;
using Npgsql;
using Resunet.DAL.Models;


namespace Resunet.DAL.Auth
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> CreateUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<UserModel>(@" 
                    select UserId, Email, Password, Salt, Status 
                    from AppUser 
                    where Email = @email", new { email = email }) ?? new UserModel();
            }
        }

        public async Task<UserModel> CreateUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                connection.Open();

                // если запись не найдена в бд => возвращается пустой объект 
                return await connection.QueryFirstOrDefaultAsync<UserModel>(@" 
                    select UserId, Email, Password, Salt, Status 
                    from AppUser 
                    where UserId = @id", new { id = id }) ?? new UserModel();
            }
        }

        public async Task<int> CreateUser(UserModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                connection.Open();
                string sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status)"; 
                
                return await connection.ExecuteAsync(sql,model);
            }
        }
    }
}