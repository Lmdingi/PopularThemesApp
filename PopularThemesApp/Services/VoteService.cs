using PopularThemesApp.Data.Repository;
using PopularThemesApp.Models;

namespace PopularThemesApp.Services
{
    public class VoteService
    {
        private readonly VoteRepository _voteRepository;
        private readonly UserRepository _userRepository;

        public VoteService(VoteRepository voteRepository, UserRepository userRepository)
        {
            _voteRepository = voteRepository;
            _userRepository = userRepository;
        }

        public void LoadVotesFromFile(string votesFilePath)
        {
            _voteRepository.LoadVotesFromFile(votesFilePath);
        }

        public string GetMostVotedColour()
        {
            int numberOfVotes = 0;
            string mostVotedColour = string.Empty;

            var allVotes = _voteRepository.GetAllVotes();
            var votedColours = allVotes.Select(v => v.Colour).Distinct().ToList();

            foreach (var votedColour in votedColours)
            {
                int currentVoteCount = 0;

                foreach (var vote in allVotes)
                {
                    if (vote.Colour == votedColour)
                    {
                        currentVoteCount++;
                    }
                }

                if (currentVoteCount > numberOfVotes)
                {
                    numberOfVotes = currentVoteCount;
                    mostVotedColour = votedColour;
                }
            }

            return mostVotedColour;
        }


        public List<User> GetUsersWhoVotedForColour(string mostVotedColor)
        {
            List<User> colourVoters = new List<User>();
            var allVotes = _voteRepository.GetAllVotes();

            foreach (var vote in allVotes)
            {
                if (vote.Colour == mostVotedColor)
                {
                    var voter = _userRepository.GetUserById(vote.UserId);
                    if (voter != null)
                    {
                        colourVoters.Add(voter);
                    }
                }
            }

            return colourVoters;
        }
    }
}
