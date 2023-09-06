using Movies.Client.Models;

namespace Movies.Client.Services
{
    public interface IAccountService
    {
        public Task<UserInfoViewModel> GetUserInfo();
    }
}
