using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VoteNowApp.Areas.Identity.Data;
using VoteNowApp.Models.Model;
using VoteNowApp.Models.ViewModel;

namespace VoteNowApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CandidateController : Controller
    {
        private readonly VoteDBContext voteDbContext;

        public CandidateController(VoteDBContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Candidate = await voteDbContext.Candidates.ToListAsync();
            return View(Candidate);
        }

        [HttpGet]
        public IActionResult Select()
        {
            return Add();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCandidateViewModel addCandidatesRequest)
        {
            var Candidate = new CandidateModel()
            {
                candidatename = addCandidatesRequest.candidatename,
                number = addCandidatesRequest.number,
                course = addCandidatesRequest.course,
                yearlevel = addCandidatesRequest.yearlevel,
                position = addCandidatesRequest.position,

            };

            await voteDbContext.Candidates.AddAsync(Candidate);
            await voteDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var Candidate = await voteDbContext.Candidates.FirstOrDefaultAsync(x => x.Id == id);

            if (Candidate != null)
            {
                var viewModel = new UpdateCandidateViewModel()
                {
                    Id = Candidate.Id,
                    candidatename = Candidate.candidatename,
                    number = Candidate.number,
                    course = Candidate.course,
                    yearlevel = Candidate.yearlevel,
                    position = Candidate.position,

                    Positions = voteDbContext.Positions
                    .Select(p => new SelectListItem { Value = p.title, Text = p.title })
                    .ToList()
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCandidateViewModel model)
        {
            var Candidate = await voteDbContext.Candidates.FindAsync(model.Id);

            if (Candidate != null)
            {
                Candidate.candidatename = model.candidatename;
                Candidate.number = model.number;
                Candidate.course = model.course;
                Candidate.yearlevel = model.yearlevel;
                Candidate.position = model.position;

                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCandidateViewModel model)
        {
            var Candidate = await voteDbContext.Candidates.FindAsync(model.Id);

            if (Candidate != null)
            {
                voteDbContext.Candidates.Remove(Candidate);
                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new AddCandidateViewModel
            {
                Positions = voteDbContext.Positions
                    .Select(p => new SelectListItem { Value = p.title, Text = p.title })
                    .ToList()
            };

            return View(viewModel);
        }
    }
}
