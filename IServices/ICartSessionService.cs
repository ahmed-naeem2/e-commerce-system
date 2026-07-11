
namespace e_commerce_system.IServices
{
    public interface ICartSessionService
    {
        string GetOrCreateSessionId();
        void ClearSessionId();
        

    }