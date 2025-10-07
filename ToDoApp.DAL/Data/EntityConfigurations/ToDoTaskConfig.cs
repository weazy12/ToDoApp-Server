using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Data.EntityConfigurations
{
    internal class ToDoTaskConfig : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasMaxLength(4000);

            entity.Property(e => e.DueDate);

            entity.Property(e => e.Status)
                .IsRequired();

            entity.Property(t => t.CompletedAt);
        }
    }
}
