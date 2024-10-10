using Resunet.DAL.Models;
using Resunet.ViewModels;

namespace Resunet.ViewMapper
{
    public class AuthMapper
    {
        // из RegisterViewModel будем получать модель и превращать ее в эту модель UserModel
        // и будем отправлять на уровень (DAL)
        public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
        {
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}