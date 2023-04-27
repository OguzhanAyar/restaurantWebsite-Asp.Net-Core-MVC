using Microsoft.AspNetCore.Mvc;
using RestorantDeneme.Data;
using System.Linq;

namespace RestorantDeneme.ViewComponents
{
    public class İletisimim : ViewComponent

    {
        private readonly ApplicationDbContext _db;
        public İletisimim(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var iletisim = _db.Iletisimims.ToList();
            return View(iletisim);
        }
    }
}
