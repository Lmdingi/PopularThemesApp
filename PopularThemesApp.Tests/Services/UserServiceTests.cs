using PopularThemesApp.Data.Repository;
using PopularThemesApp.Services;

namespace PopularThemesApp.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void LoadUsersFromFile_ShouldReadFileAndLoadData()
        {
            // Arrange
            var userRepository = new UserRepository();

            // Act
            var userService = new UserService(userRepository);
            var addedUsersTrue = userService.LoadUsersFromFile("MockData/users.txt");
            var addedUsersFalse = userService.LoadUsersFromFile("MockData/wrongUsers.txt");

            var result = userService.GetAllUsers();

            // Assert
            Assert.True(addedUsersTrue);
            Assert.False(addedUsersFalse);
            Assert.Equal(5, result.Count);
            Assert.Equal(1, result[0].Id);
            Assert.Equal("Alicia Harper", result[0].FullName);
        }   
        
        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var userRepository = new UserRepository();
            var userService = new UserService(userRepository);
            userService.LoadUsersFromFile("MockData/users.txt");

            var wrongUserRepository = new UserRepository();
            var wrongUserService = new UserService(wrongUserRepository);
            wrongUserService.LoadUsersFromFile("MockData/wrongUsers.txt");

            // Act
            var userServiceResult = userService.GetAllUsers();
            var wrongUserServiceResult = wrongUserService.GetAllUsers();

            // Assert
            Assert.Equal(5, userServiceResult.Count);
            Assert.Equal(1, userServiceResult[0].Id);
            Assert.Equal("Alicia Harper", userServiceResult[0].FullName);
            Assert.Empty(wrongUserServiceResult);
        }
    }
}
