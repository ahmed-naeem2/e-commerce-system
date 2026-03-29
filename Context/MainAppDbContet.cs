using e_commerce_system.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_system.Context
{
	public class MainAppDbContet:IdentityDbContext<User,Role,Guid>
	{

		public MainAppDbContet(DbContextOptions<MainAppDbContet>options):base(options) { }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);





		}   

	}
}
