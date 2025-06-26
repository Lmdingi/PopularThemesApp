using PopularThemesApp.Data.Repository;
using PopularThemesApp.Models;

namespace PopularThemesApp.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool LoadUsersFromFile(string usersFilePath)
        {
            try
            {
                return _userRepository.LoadUsersFromFile(usersFilePath);
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
