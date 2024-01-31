namespace VoteNowApp.Models.Model
{
    public class VoteModel
    {
        public Guid VoteId { get; set; }
        public Guid CandidateId { get; set; }
        public string VoterId { get; set; }
    }
}
