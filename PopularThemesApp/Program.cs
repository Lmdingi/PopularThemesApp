using PopularThemesApp.Data.Repository;
using PopularThemesApp.Services;

namespace PopularThemesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Get users and votes data from text files
            var userRepository = new UserRepository();
            var voteRepository = new VoteRepository();

            var userService = new UserService(userRepository);
            var voteService = new VoteService(voteRepository, userRepository);

            userService.LoadUsersFromFile("Data/users.txt");
            voteService.LoadVotesFromFile("Data/favourites.txt");

            var users = userService.GetAllUsers();

            // Display the most voted colour
            var mostVotedColor = voteService.GetMostVotedColour();
            Console.WriteLine("====================================");
            Console.WriteLine($"Most voted colour: {mostVotedColor}");
            Console.WriteLine("====================================");

            // Get users who voted for it and sort them by full name.
            var colourVoters = voteService.GetUsersWhoVotedForColour(mostVotedColor);
            var sortedColourVoters = colourVoters.OrderBy(v => v.FullName).ToList();

            // Display full names of users who voted for the most voted colour.
            Console.WriteLine("Users who voted for the colour:");
            foreach (var voter in sortedColourVoters)
            {
                Console.WriteLine($"\t{voter.FullName}");
            }

            Console.ReadKey();
        }
    }
}
