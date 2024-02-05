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
    public class VoterController : Controller
    {
        private readonly VoteDBContext voteDbContext;

        public VoterController(VoteDBContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Voter = await voteDbContext.Voters.ToListAsync();
            return View(Voter);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVoterViewModel addVoterRequest)
        {
            var Voter = new VoterModel()
            {
                votername = addVoterRequest.votername,
                number = addVoterRequest.number,
                course = addVoterRequest.course,
                yearlevel = addVoterRequest.yearlevel,
                password = addVoterRequest.password,

            };

            await voteDbContext.Voters.AddAsync(Voter);
            await voteDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var Voter = await voteDbContext.Voters.FirstOrDefaultAsync(x => x.Id == id);

            if (Voter != null)
            {
                var viewModel = new UpdateVoterViewModel()
                {
                    Id = Voter.Id,
                    votername = Voter.votername,
                    number = Voter.number,
                    course = Voter.course,
                    yearlevel = Voter.yearlevel,
                    password = Voter.password,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateVoterViewModel model)
        {
            var Voter = await voteDbContext.Voters.FindAsync(model.Id);

            if (Voter != null)
            {
                Voter.votername = model.votername;
                Voter.number = model.number;
                Voter.course = model.course;
                Voter.yearlevel = model.yearlevel;
                Voter.password = model.password;

                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVoterViewModel model)
        {
            var Voter = await voteDbContext.Voters.FindAsync(model.Id);

            if (Voter != null)
            {
                voteDbContext.Voters.Remove(Voter);
                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
