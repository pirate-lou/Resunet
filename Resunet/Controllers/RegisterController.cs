using Resunet.BL.Auth;
using Microsoft.AspNetCore.Mvc;
using Resunet.ViewMapper;
using Resunet.ViewModels;

// ничего не знает о DAL уровне, его прерогатива работать только с BL уровнем 

namespace Resunet.Controllers
{
    public class RegisterController : Controller
    {
        // никогда не будем менять 
        private readonly IAuthBL authBl;

        public RegisterController(IAuthBL authBl)
        {
            this.authBl = authBl;
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Index() // отображение формы для регистрации 
        {
            return View("Index", new RegisterViewModel());
        }

        [HttpPost] 
        [Route("/register")]
        public IActionResult IndexSave(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                authBl.CreateUser(AuthMapper.MapRegisterViewModelToUserModel(model));
                // если зарегался => отправляем на домашнюю страницу
                return Redirect("/"); 
            }
            
            // файл "Index" будет искаться, по умолчанию, в папке с названием класса
            // но без "Controller", т.е. "Register"
            // и если в папке "View" указать папку "Register" как-то по-другому,
            // то придется здесь придется указывать полное имя (где его искать)
            return View("Index", model);
        }
    }
}