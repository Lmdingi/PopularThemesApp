using PopularThemesApp.Models;

namespace PopularThemesApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Load user and vote data from text files.
            var usersFileData = File.ReadAllLines("Data/users.txt");
            var favouritesFileData = File.ReadAllLines("Data/favourites.txt");

            List<User> users = new List<User>();
            List<Vote> votes = new List<Vote>();

            // Get users from file data.
            foreach (var line in usersFileData)
            {
                var result = GetIdAndValue(line, "\t");

                if (result != null)
                {
                    var (id, fullName) = result.Value;

                    users.Add(new User
                    {
                        Id = id,
                        FullName = fullName,
                    });
                }
            }

            // Get votes from file data.
            foreach (var line in favouritesFileData)
            {
                var result = GetIdAndValue(line, " ");

                if (result != null)
                {
                    var (userId, colour) = result.Value;

                    votes.Add(new Vote
                    {
                        UserId = userId,
                        Colour = colour,
                    });
                }
            }

            // Get the most voted colour.
            string mostVotedColor = GetMostVotedColour(votes);

            // Get all users who voted for the most voted colour, and sort them by full name.
            var colourVoters = GetUsersWhoVotedForColour(mostVotedColor, votes, users);
            var sortedColourVoters = colourVoters.OrderBy(v => v.FullName).ToList();

            // Display full names of users who voted for the most voted colour.
            foreach (var voter in sortedColourVoters)
            {
                Console.WriteLine(voter.FullName);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Get all users who voted for the specified colour.
        /// </summary>
        /// <param name="colour">The colour to filter votes by.</param>
        /// <param name="votes">The list of all votes.</param>
        /// <param name="voters">The list of all users.</param>
        /// <returns>A list of users who voted for the specified colour.</returns>
        private static List<User> GetUsersWhoVotedForColour(string colour, List<Vote> votes, List<User> voters)
        {
            List<User> colourVoters = new List<User>();

            foreach (var vote in votes)
            {
                if (vote.Colour == colour)
                {
                    var voter = voters.FirstOrDefault(v => v.Id == vote.UserId);
                    if (voter != null)
                    {
                        colourVoters.Add(voter);
                    }
                }
            }

            return colourVoters;
        }

        /// <summary>
        /// Get the colour that received the most votes.
        /// </summary>
        /// <param name="votes">The list of all votes.</param>
        /// <returns>The name of the most voted colour.</returns>
        private static string GetMostVotedColour(List<Vote> votes)
        {
            var votedColours = votes.Select(v => v.Colour).Distinct().ToList();

            int count = 0;
            string mostVotedColour = "";

            foreach (var votedColour in votedColours)
            {
                int currentVoteCount = 0;

                foreach (var vote in votes)
                {
                    if (vote.Colour == votedColour)
                    {
                        currentVoteCount++;
                    }
                }

                if (currentVoteCount > count)
                {
                    count = currentVoteCount;
                    mostVotedColour = votedColour;
                }
            }

            return mostVotedColour;
        }

        /// <summary>
        /// Seperates a string into an integer id and a string value using the specified delimiter.
        /// </summary>
        /// <param name="line">The input with the string containing both id and value.</param>
        /// <param name="delimiter">The character used to split the line.</param>
        /// <returns>A tuple containing the id and value, or null if spliting fails.</returns>
        public static (int id, string value)? GetIdAndValue(string line, string delimiter)
        {
            var info = line.Split(delimiter);

            if (info.Length != 2 || !int.TryParse(info[0], out int id))
            {
                return null;
            }

            return (id, info[1]);
        }
    }
}
