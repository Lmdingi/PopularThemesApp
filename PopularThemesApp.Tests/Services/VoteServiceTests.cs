using PopularThemesApp.Data.Repository;
using PopularThemesApp.Services;

namespace PopularThemesApp.Tests.Services
{
    public class VoteServiceTests
    {
        [Fact]
        public void LoadVotesFromFile_ShouldReadFileAndLoadData()
        {
            // Arrange
            var voteRepository = new VoteRepository();
            var userRepository = new UserRepository();

            // Act
            var voteService = new VoteService(voteRepository, userRepository);
            var addedVotesTrue = voteService.LoadVotesFromFile("MockData/favourites.txt");
            var addedVotesFalse = voteService.LoadVotesFromFile("MockData/wrongFavourites.txt");

            // Assert
            Assert.True(addedVotesTrue);
            Assert.False(addedVotesFalse);
        }

        [Fact]
        public void GetMostVotedColour_ShouldReturnNameOfTheColour()
        {
            //Arrange
            var userRepository = new UserRepository();

            var voteRepository = new VoteRepository();      
            var voteService = new VoteService(voteRepository, userRepository);
            voteService.LoadVotesFromFile("MockData/favourites.txt");

            var wrongVoteRepository = new VoteRepository();
            var wrongVoteService = new VoteService(wrongVoteRepository, userRepository);
            wrongVoteService.LoadVotesFromFile("MockData/wrongFavourites.txt");

            // Act
            var mostVotedColourResult = voteService.GetMostVotedColour();
            var mostVotedColourWrongResult = wrongVoteService.GetMostVotedColour();

            // Assert
            Assert.Equal("Maroon", mostVotedColourResult);
            Assert.Equal(string.Empty, mostVotedColourWrongResult);
        }

        [Fact]
        public void GetUsersWhoVotedForColour_ShouldReturnNamesOfVotersWhoForTheProvidedColour()
        {
            //Arrange
            var userRepository = new UserRepository();
            var voteRepository = new VoteRepository();

            var userService = new UserService(userRepository);
            var voteService = new VoteService(voteRepository, userRepository);

            voteService.LoadVotesFromFile("MockData/favourites.txt");
            var addedUsersTrue = userService.LoadUsersFromFile("MockData/users.txt");

            // Act
            var votesResult = voteService.GetUsersWhoVotedForColour("Maroon");
            var wrongVotesResult = voteService.GetUsersWhoVotedForColour("wrong colour");

            // Assert
            Assert.Equal(3, votesResult.Count);

            Assert.Equal(1, votesResult[0].Id);
            Assert.Equal(4, votesResult[1].Id);
            Assert.Equal(5, votesResult[2].Id);

            Assert.Equal("Alicia Harper", votesResult[0].FullName);
            Assert.Equal("Andrew Beasley", votesResult[1].FullName);
            Assert.Equal("Timothy Holmes", votesResult[2].FullName);

            Assert.Empty(wrongVotesResult);
        }
    }
}