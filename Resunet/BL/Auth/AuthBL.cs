using System;
using Resunet.DAL;
using Resunet.DAL.Models;

namespace Resunet.BL.Auth
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDAL authDal;
        private readonly IEncrypt encrypt;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthBL(IAuthDAL authDal,
            IEncrypt encrypt,
            IHttpContextAccessor httpContextAccessor) // получить доступ к контексту http  
        {
            this.authDal = authDal;
            this.encrypt = encrypt;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateUser(UserModel user)
        {
            user.Salt = Guid.NewGuid().ToString();
            user.Password = encrypt.HashPassword(user.Password, user.Salt);
            int id = await authDal.CreateUser(user);
            Login(id);
            return id;
        }

        // авторизовались и запоминаем user в системе
        private void Login(int id)
        {
            // если в сессии есть userid и мы знаем его id - значит user авторизован 
            httpContextAccessor.HttpContext?.Session.SetInt32(
                AuthConstants.AUTH_SESSION_PARAM_NAME, id);
        }
        
        public async Task<int> AunthenticateUser(
            string email, string password, bool rememberMe)
        {
            var user = await authDal.GetUser(email);
            if (user.Password == encrypt.HashPassword(password, user.Salt))
            {
                Login(user.UserId);
                return user.UserId;
            }

            return 0;
        }
    }
}