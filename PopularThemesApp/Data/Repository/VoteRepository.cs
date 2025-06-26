using PopularThemesApp.Models;

namespace PopularThemesApp.Data.Repository
{
    public class VoteRepository
    {
        private readonly List<Vote> _votes = new List<Vote>();

        public void LoadVotesFromFile(string votesFilePath)
        {
            if (!string.IsNullOrWhiteSpace(votesFilePath))
            {
                var votesFileData = File.ReadAllLines(votesFilePath);

                if (votesFileData != null)
                {
                    foreach (var line in votesFileData)
                    {
                        var voteInformation = line.Split(" ");

                        if (voteInformation.Length != 2 || !int.TryParse(voteInformation[0], out int userId))
                        {
                            continue;
                        }

                        _votes.Add(new Vote
                        {
                            UserId = userId,
                            Colour = voteInformation[1],
                        });
                    }
                }
            }
        }

        internal List<Vote> GetAllVotes()
        {
            return _votes;
        }
    }
}
