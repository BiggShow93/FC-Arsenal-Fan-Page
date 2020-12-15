namespace ArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ArsenalFanPage.Data;
    using ArsenalFanPage.Data.Common.Repositories;
    using ArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class NewsController : AdministrationController
    {
        private readonly IDeletableEntityRepository<News> dataRepository;
        private readonly ApplicationDbContext context;

        public NewsController(IDeletableEntityRepository<News> dataRepository, ApplicationDbContext context)
        {
            this.dataRepository = dataRepository;
            this.context = context;
        }

        // GET: Administration/News
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.dataRepository.AllAsNoTracking().Include(n => n.Category).Include(n => n.CreatedByUser).Include(n => n.Image);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var news = await this.dataRepository.All()
                .Include(n => n.Category)
                .Include(n => n.CreatedByUser)
                .Include(n => n.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }
  

        // GET: Administration/News/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Id");
            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id");
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id");
            return this.View();
        }

        // POST: Administration/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,CreatedByUserId,CategoryId,ImageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(news);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Id", news.CategoryId);
            this.ViewData["CreatedByUserId"] = new SelectList(this.context.Users, "Id", "Id", news.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(this.context.Images, "Id", "Id", news.ImageId);

            return this.View(news);
        }

        // GET: Administration/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();


            }

            var news = this.dataRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Id", news.CategoryId);
            this.ViewData["CreatedByUserId"] = new SelectList(context.Users, "Id", "Id", news.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(context.Images, "Id", "Id", news.ImageId);

            return this.View(news);
        }

        // POST: Administration/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Content,CreatedByUserId,CategoryId,ImageId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(news);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.NewsExists(news.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            this.ViewData["CategoryId"] = new SelectList(context.Categories, "Id", "Id", news.CategoryId);
            this.ViewData["CreatedByUserId"] = new SelectList(context.Users, "Id", "Id", news.CreatedByUserId);
            this.ViewData["ImageId"] = new SelectList(context.Images, "Id", "Id", news.ImageId);
            return View(news);
        }

        // GET: Administration/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var news = await this.dataRepository.All()
                .Include(n => n.Category)
                .Include(n => n.CreatedByUser)
                .Include(n => n.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return this.NotFound();
            }

            return this.View(news);
        }

        // POST: Administration/News/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(news);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool NewsExists(int id)
        {
            return this.dataRepository.All().Any(e => e.Id == id);
        }
    }
}
