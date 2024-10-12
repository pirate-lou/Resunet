namespace Resunet.BL.Auth
{
    public class CurrentUser : ICurrentUser
    {
        // httpContextAccessor - для доступа к сессии 
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool IsLoggedIn()
        {
            // проверяем, есть ли там userid
            // если пользователь сохраняется в сессии, значит он ужу авторизован  
            return httpContextAccessor.HttpContext?.Session.GetInt32(AuthConstants.AUTH_SESSION_PARAM_NAME) != null;
        }
    }
}