using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoteNowApp.Areas.Identity.Data;
using VoteNowApp.Models.ViewModel;
using VoteNowApp.Models.Model;
using Microsoft.AspNetCore.Authorization;

namespace VoteNowApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly VoteDBContext voteDbContext;

        public PositionController(VoteDBContext voteDbContext)
        {
            this.voteDbContext = voteDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Position = await voteDbContext.Positions.ToListAsync();
            return View(Position);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPositionViewModel addPositionsRequest)
        {
            var Position = new PositionModel()
            {
                title = addPositionsRequest.title,
            };

            await voteDbContext.Positions.AddAsync(Position);
            await voteDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var Position = await voteDbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);

            if (Position != null)
            {
                var viewModel = new UpdatePositionViewModel()
                {
                    Id = Position.Id,
                    title = Position.title,
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdatePositionViewModel model)
        {
            var Position = await voteDbContext.Positions.FindAsync(model.Id);

            if (Position != null)
            {
                Position.title = model.title;

                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePositionViewModel model)
        {
            var Position = await voteDbContext.Positions.FindAsync(model.Id);

            if (Position != null)
            {
                voteDbContext.Positions.Remove(Position);
                await voteDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
