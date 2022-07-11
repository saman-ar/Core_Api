using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core2_Api.Infra;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core2_Api.Models;

namespace Core2_Api.Models
{
	public class AppDbContext : IdentityDbContext //<AppUser,Role,string>
	{
		/// متد سازنده
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{	}

		public DbSet<RoomEntity> Rooms { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<Role> Roles { get; set; }


		//public DbSet<Model1> Tests { get; set; }

		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//	builder.Entity<Model2>()//.Property("");
		//		.HasOne(c => c.Model1s);

		//	builder.Entity<Model1>()
		//		.HasOne(c => c.Models2s)
		//		.WithOne(c => c.Model1s)
		//		.HasForeignKey(nameof(Model1.FK_Model2))
		//		.HasForeignKey(nameof(Model2.Fk_Model1));
					
				

		//	base.OnModelCreating(builder);
		//}


		//protected overriSingularTableNamede void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.PlurizeTablesName();
		//	modelBuilder.SingularTablesName();
		//	modelBuilder.("");


		//	base.OnModelCreating(modelBuilder);
		//}

		#region Overriding SaveChange

		/////override کردن این متد مهم 
		/////برای اینکه قبل از دخیره تنطیمات عملیاتی روی داده های ذخیره شونده
		/////اعمال شده و سپس عمل ذخیره انجام شود
		//public override int SaveChanges()
		//{
		//	CleanString();
		//	return base.SaveChanges();
		//}
		//public override int SaveChanges(bool acceptAllChangesOnSuccess)
		//{
		//	CleanString();
		//	return base.SaveChanges(acceptAllChangesOnSuccess);
		//}
		//public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		//{
		//	CleanString();
		//	return base.SaveChangesAsync(cancellationToken);
		//}

		#endregion Overriding SaveChange

		/// <summary>
		/// برای پاکسازی رشته ها از حروف و اعداد عربی و غیره
		/// </summary>
		//private void CleanString()
		//{
		//	//TODO : کدهای داخلش نوشته شود

		//	var changedEntities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

		//	foreach (var entity in changedEntities)
		//	{
		//		var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where( p=>p.CanRead && p.CanWrite && p.PropertyType==typeof(string));

		//	}
		//}
	}
	


	public class PostConfiguration : IEntityTypeConfiguration<Post>
	{
		public void Configure(EntityTypeBuilder<Post> builder)
		{

			//throw new NotImplementedException();
		}
	}
	public class Post
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}



}
