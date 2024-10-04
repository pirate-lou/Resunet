using Resunet.DAL.Models;

namespace Resunet.DAL
{
    public interface IAuthDAL
    {
        // поиск по email и id
        Task<UserModel> CreateUser(string email);
        Task<UserModel> CreateUser(int id);
        
        // возвращает id созданного пользователя 
        Task<int> CreateUser(UserModel model);
    }
}