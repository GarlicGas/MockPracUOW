using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MockPracUOW.Shared.Domain;

namespace MockPracUOW.Server.Configurations.Entities
{
	public class TaskSeedConfiguration : IEntityTypeConfiguration<Shared.Domain.Task>
	{
		public void Configure(EntityTypeBuilder<Shared.Domain.Task> builder)
		{
			builder.HasData(
			new Shared.Domain.Task
			{
				Id = 1,
				TaskName = "Eat frog",
				DueDate = DateTime.Today.AddDays(47),
				DateCreated = DateTime.Today,
				CreatedBy = "Aung_San_Win"
			}
			);
		}
	}
}