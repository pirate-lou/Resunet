using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        public readonly IAuthBL authDAL;

        public AuthBL(IAuthDAL authDAL)
        {
            this.authDAL = this.authDAL;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            return await authDAL.CreateUser(user);
        }
    }
}