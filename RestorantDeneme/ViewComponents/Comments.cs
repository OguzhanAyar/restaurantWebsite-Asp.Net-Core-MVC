using Microsoft.AspNetCore.Mvc;
using RestorantDeneme.Data;
using System.Linq;

namespace RestorantDeneme.ViewComponents
{
    public class Comments:ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public Comments(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var comment = _db.Blogs.Where(i => i.Onay).ToList();
            return View(comment);
        }
    }
}
