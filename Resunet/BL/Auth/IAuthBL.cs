using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public interface IAuthBL
    {
        Task<int> CreateUser(UserModel user);
    }
}