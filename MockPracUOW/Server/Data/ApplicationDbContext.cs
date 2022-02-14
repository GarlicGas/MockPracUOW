using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MockPracUOW.Server.Models;
using MockPracUOW.Server.Configurations.Entities;

namespace MockPracUOW.Server.Data
{
	public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}
		public DbSet<Shared.Domain.Task> Tasks { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new TaskSeedConfiguration());
			builder.ApplyConfiguration(new UserRoleSeedConfiguration());
			builder.ApplyConfiguration(new UserSeedConfiguration());
			builder.ApplyConfiguration(new RoleSeedConfiguration());
		}
	}
}
