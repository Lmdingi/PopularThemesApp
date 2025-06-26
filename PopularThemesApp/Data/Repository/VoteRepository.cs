using PopularThemesApp.Models;

namespace PopularThemesApp.Data.Repository
{
    public class VoteRepository
    {
        private readonly List<Vote> _votes = new List<Vote>();

        public bool LoadVotesFromFile(string votesFilePath)
        {
            bool addedVotes = false;
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

                        addedVotes = true;
                    }
                }
            }

            return addedVotes;
        }

        internal List<Vote> GetAllVotes()
        {
            return _votes;
        }
    }
}
