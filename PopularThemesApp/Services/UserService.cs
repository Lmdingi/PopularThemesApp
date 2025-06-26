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

        public void LoadUsersFromFile(string usersFilePath)
        {
             _userRepository.LoadUsersFromFile(usersFilePath);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
