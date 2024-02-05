namespace VoteNowApp.Models.ViewModel
{
    public class UpdateVoterViewModel
    {
        public Guid Id { get; set; }
        public string votername { get; set; } = string.Empty;
        public int number { get; set; }
        public string course { get; set; } = string.Empty;
        public string yearlevel { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}
