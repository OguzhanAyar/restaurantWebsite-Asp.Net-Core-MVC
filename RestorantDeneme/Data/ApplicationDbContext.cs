using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RestorantDeneme.Models;

namespace RestorantDeneme.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Category>Categories { get; set; }
		public DbSet<Menu>Menus { get; set; }
		public DbSet<Rezervasyon>Reservation { get; set; }
		public DbSet<Galeri>Galeris { get; set; }
		public DbSet<Hakkında>Hakkındas { get; set; }
		public DbSet<Blog>Blogs { get; set; }
		public DbSet<Iletisim>Iletisims { get; set; }
		public DbSet<Iletisimim>Iletisimims { get; set; }
		public DbSet<Appuser>Appusers { get; set; }

	}
}
