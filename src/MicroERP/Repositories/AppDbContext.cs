using MicroERP.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroERP.Repositories;

public class AppDbContext : DbContext
{
    public DbSet<PsBoard> Boards { get; set; }
    public DbSet<BoardStageHistory> BoardStageHistories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PsBoard>(entity =>
        {
            entity.HasKey(b => b.Id);;

            entity.HasMany(b => b.StageHistory)
                  .WithOne(h => h.Board)
                  .HasForeignKey(h => h.BoardId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BoardStageHistory>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.NewStage).HasConversion<string>();
            entity.Property(h => h.OldStage).HasConversion<string>();
            entity.Property(h => h.Timestamp).IsRequired();
        });
    }
}
