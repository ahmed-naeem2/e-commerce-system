

using e_commerce_system.IServices;

namespace e_commerce_system.Services
{
    public class CartSessionService : ICartSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionKey = "CartSessionId";

        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetOrCreateSessionId()
        {
            var context = _httpContextAccessor.HttpContext!;
            if(context.Request.Cookies.TryGetValue(SessionKey, out var sessionId))
            
                return sessionId;
            
            sessionId=Guid.NewGuid().ToString();

            context.Response.Cookies.Append(SessionKey, sessionId, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(30), // Set the cookie expiration as needed
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
                SameSite = SameSiteMode.Lax
            });

           

            return sessionId;
        }
        
        public void ClearSessionId()
        {
            var context = _httpContextAccessor.HttpContext!;
            context.Response.Cookies.Delete(SessionKey);
        }
    }
}