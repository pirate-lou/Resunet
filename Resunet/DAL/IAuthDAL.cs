using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IAuthDAL
    {
        // поиск по email и id
        Task<UserModel> GetUser(string email);
        Task<UserModel> GetUser(int id);
        
        // возвращает id созданного пользователя 
        Task<int> CreateUser(UserModel model);
    }
}