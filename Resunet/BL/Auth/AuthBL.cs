using System.ComponentModel.DataAnnotations;
using System.Data;
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
            Encrypt encrypt,
            IHttpContextAccessor httpContextAccessor)
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

        // log in and remember user in system  
        private void Login(int id)
        {
            // if session has userid and we know his id - it means, that user authorised 
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

        public async Task<ValidationResult?> ValidateEmail(string email)
        {
            var user = await authDal.GetUser(email);
            if (user.UserId != null)
                return new ValidationResult("Email уже существует");
            return null;
        }
    }
}