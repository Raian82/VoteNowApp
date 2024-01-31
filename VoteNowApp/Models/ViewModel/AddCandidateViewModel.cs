using Microsoft.AspNetCore.Mvc.Rendering;

namespace VoteNowApp.Models.ViewModel
{
    public class AddCandidateViewModel
    {
        public string candidatename { get; set; } = string.Empty;
        public int number { get; set; }
        public string course { get; set; } = string.Empty;
        public string yearlevel { get; set; } = string.Empty;
        public string position { get; set; } = string.Empty;
        public List<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
    }
}
