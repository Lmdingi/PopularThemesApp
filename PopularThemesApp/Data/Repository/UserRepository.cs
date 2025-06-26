using PopularThemesApp.Models;

namespace PopularThemesApp.Data.Repository
{
    public class UserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void LoadUsersFromFile(string usersFilePath)
        {
            if (!string.IsNullOrWhiteSpace(usersFilePath))
            {
                var usersFileData = File.ReadAllLines(usersFilePath);

                if (usersFileData != null)
                {
                    foreach (var line in usersFileData)
                    {
                        var userInformation = line.Split("\t");

                        if (userInformation.Length != 2 || !int.TryParse(userInformation[0], out int id))
                        {
                            continue;
                        }

                        _users.Add(new User
                        {
                            Id = id,
                            FullName = userInformation[1],
                        });
                    }
                }
            }
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User? GetUserById(int userId)
        {

            var user = _users.FirstOrDefault(v => v.Id == userId);

            if(user == null)
            {
                return null;
            }

            return user;
        }
    }
}
