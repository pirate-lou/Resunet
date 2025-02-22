using System;
using Dapper;
using Npgsql;
using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                // OpenAsync - не будет держать ресурсы потока
                // сильно на производительность не повлияет 
                // только в те моменты, когда в pull чего-то не хватает и нужно подтянуть что-то
                // открыть доп соединение, но это будет редко
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<UserModel>(@" 
                    select UserId, Email, Password, Salt, Status 
                    from AppUser 
                    where Email = @email", new { email = email }) ?? new UserModel();
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnString))
            {
                await connection.OpenAsync();

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
                // после регистрации должен быть авторизован, но с await не авторизован
                await connection.OpenAsync(); 
                // connection.Open();

                string sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status);
                    SELECT currval(--вернуть id последней записи для postgre--
                    pg_get_serial_sequence('AppUser', 'userid'))";
                return await connection.QuerySingleAsync<int>(sql, model);
            }
        }
    }
}