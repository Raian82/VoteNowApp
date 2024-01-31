using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoteNowApp.Areas.Identity.Data;

namespace VoteNowApp.Controllers
{
    [Authorize(Roles = "Admin,Voter")]
    public class VotingController : Controller
    {
        private readonly VoteDBContext _context;

        public VotingController(VoteDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var candidates = _context.Candidates.ToList();
            return View(candidates);
        }

        [HttpPost]
        public IActionResult Vote(Dictionary<string, Guid> selectedCandidates)
        {
            foreach (var position in selectedCandidates.Keys)
            {
                var candidateId = selectedCandidates[position];
                var candidate = _context.Candidates.Find(candidateId);
                if (candidate != null)
                {
                    candidate.Votes++;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Privacy", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Results()
        {
            var candidates = _context.Candidates.ToList();
            return View(candidates);
        }
    }
}
