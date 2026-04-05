using e_commerce_system.Models;
using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Context
{
	public class MainAppDbContet:IdentityDbContext<User,Role,Guid>
	{

	public 	DbSet<Product> products { get; set; }

		public DbSet<Order>orders {  get; set; }

	

		public DbSet<Cart> carts { get; set; }

		public DbSet<CartItem> cartItems { get; set; }

		public DbSet <OrderItem> orderItems { get; set; }

		public DbSet<ProductImage>ProductImages { get; set; }

		public DbSet<Categorie> categories {  get; set; }

		public MainAppDbContet() { }
		public MainAppDbContet(DbContextOptions<MainAppDbContet>options):base(options) { }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			builder.Entity<User>(entity =>
			{
				entity.HasKey(u=>u.Id);

				entity.HasIndex(u=>u.PhoneNumber)
				.IsUnique();

				entity.Property(u => u.Address)
				.HasMaxLength(100)
				.IsRequired();

				entity.Property(u=>u.PersonName)
				.HasMaxLength(100)
				.IsRequired();

				entity.Property(u => u.UserName)
				.HasMaxLength(100)

				.IsRequired();

				entity.HasIndex(u=>u.UserName)
				.IsUnique();	
				

			});

			builder.Entity<Order>(entity => {

				entity.HasKey(o => o.ID);
				entity
				.Property(o => o.OrderNumber)
				.HasMaxLength(100);







				entity.Property(o => o.TotalAmount).HasPrecision(18, 2);
				entity.Property(o => o.SubTotal).HasPrecision(18, 2);

				entity.Property(o => o.ShippingAmount).HasPrecision(18, 2);



				entity.Property(o=>o.Status)
				.HasConversion<string>()
				.HasMaxLength(50);

				entity.ToTable(t => t.HasCheckConstraint(
					"CK_Order_Status",
					"[Status] IN (N'PendingPayment',N'Completed',N'Cancelled',N'Paid')"
					)).Property(t=>t.Status).HasDefaultValue("PendingPayment");

				entity.HasOne(o=>o.user)
				      .WithMany(u=>u.Orders)            
					  .HasForeignKey(O=>O.UserId)         
							   .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(o=>o.Items)
				.WithOne(i=>i.order)
				.HasForeignKey(i=>i.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
			});
			builder.Entity<Cart>(entity =>
			{
				entity.HasKey(c => c.ID);


				entity.HasOne(c => c.user)
				.WithMany(u => u.Carts)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Restrict);

				entity.Property(c => c.Status)
				.HasConversion<string>()
				.HasMaxLength(50);


				entity.ToTable(t => t.HasCheckConstraint(
					"CK_Cart_Status",
					"[Status] IN (N'Active',N'Cancelled',N'Converted',N'Abandoned')"
					)).Property(t=>t.Status).HasDefaultValue("Active");

			});
			builder.Entity<OrderItem>(entity =>
			{
				entity.HasKey(o => o.ID);

				entity
				.HasIndex(o => new { o.OrderId, o.ProductId })
				.IsUnique();

				entity.HasOne(o=>o.order)
				.WithMany(o=>o.Items)
				.HasForeignKey(o=>o.OrderId)
				.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(o=>o.product)
				.WithMany()
				.HasForeignKey(o=>o.ProductId)
				.OnDelete(DeleteBehavior.Restrict);



			});

			builder.Entity<CartItem>(entity =>
			{
				entity.HasKey(o => o.ID);

				entity
				.HasIndex(o => new { o.CartId, o.ProductId })
				.IsUnique();

				entity.HasOne(o => o.cart)
				.WithMany(c=>c.Items)
				.HasForeignKey(o => o.CartId)
				.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(o => o.product)
				.WithMany()
				.HasForeignKey(o => o.ProductId)
				.OnDelete(DeleteBehavior.Restrict);



			});

			builder.Entity<Product>(entity =>
			{
				entity.HasKey(p => p.ID);

				entity.Property(P => P.Name)
				.HasMaxLength(150)
				.IsRequired();

				entity.Property(p => p.Description)
				.HasMaxLength(300)
				.IsRequired();

				entity.Property(P => P.Price)
				.HasPrecision(18, 2)
				.IsRequired();

			entity.HasOne(p=>p.Categorie)
				.WithMany(C=>C.Products)
				.HasForeignKey(p=>p.CategorieId)
				.OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(P=>P.Images)

				.WithOne(I=>I.product)
				.HasForeignKey(I=>I.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

				

				

			});

			builder.Entity<Categorie>(entity =>
			{
				entity.HasKey(C=>C.ID);

				entity.Property(c=>c.Name)
				.HasMaxLength(100)
				.IsRequired();

				

			});
			builder.Entity<ProductImage>(entity =>
			{
				entity.HasKey(P=>P.Id);

				entity.Property(p =>p.ImagePath)
				.HasMaxLength(100)
				.IsRequired();



			});




		}

	}
}
