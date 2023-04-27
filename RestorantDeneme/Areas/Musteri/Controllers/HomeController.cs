using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using RestorantDeneme.Data;
using RestorantDeneme.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestorantDeneme.Areas.Musteri.Controllers
{
    [Area("Musteri")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _whe;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IToastNotification toast, IWebHostEnvironment whe)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _whe = whe;
        }

        public IActionResult Index()
        {
            var menu = _db.Menus.Where(i => i.SpecialMenu).ToList();
            return View(menu);
        }
        public IActionResult CategoryDetails(int? id)
        {
            var menu = _db.Menus.Where(i => i.CategoryId == id).ToList();
            ViewBag.KategoriId = id;
            return View(menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rezervasyon(Rezervasyon rezervasyon)
        {

            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Rezervasyon işleminiz başarıyla gerçekleşti...");
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }
        public IActionResult Menu()
        {
            var menu = _db.Menus.ToList();
            return View(menu);
        }
        public IActionResult Galeri()
        {
            var galeri = _db.Galeris.ToList();
            return View(galeri);
        }
        public IActionResult Rezervasyon()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,TelefonNo,Sayi,Saat,Tarih")] Rezervasyon rezervasyon)
        {
            
            if (ModelState.IsValid)
            {
                _db.Add(rezervasyon);
                await _db.SaveChangesAsync();
                //_toast.AddSuccesToastMessage("");
                return RedirectToAction(nameof(Index));
            }
            return View(rezervasyon);
        }
        public IActionResult Hakkında()
        {
            var hakkinda = _db.Hakkındas.ToList();
            return View(hakkinda);
        }

        public IActionResult Blog()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Tarih = DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_whe.WebRootPath, @"website\menu");
                    var extn = Path.GetExtension(files[0].FileName);

                    if (blog.Image != null)
                    {
                        var ImagePath = Path.Combine(_whe.WebRootPath, blog.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(ImagePath))
                        {
                            System.IO.File.Delete(ImagePath);
                        }
                    }
                    using (var filesStream = new FileStream(Path.Combine(uploads, fileName + extn), FileMode.Create))
                    {
                        files[0].CopyTo(filesStream);
                    }
                    blog.Image = @"\website\menu\" + fileName + extn;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Yorumunuz iletildi.Onaylandığında yorumlar sayfamızdan görebilirsiniz...");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        public IActionResult Iletisim()
        {
            return View();
        }

        // POST: Yonetici/Iletisim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Iletisim([Bind("Id,Name,Email,Telefon,Mesaj,Tarih")] Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                iletisim.Tarih = DateTime.Now;
                _db.Add(iletisim);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Mesajınız başarıyla iletildi.");
                return RedirectToAction(nameof(Index));
            }
            return View(iletisim);
        }
        public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult Contact()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
