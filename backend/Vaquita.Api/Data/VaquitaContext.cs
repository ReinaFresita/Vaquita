using Microsoft.EntityFrameworkCore;
using Vaquita.Api.Models;

namespace Vaquita.Api.Data;

public class VaquitaContext : DbContext {
    public VaquitaContext(DbContextOptions<VaquitaContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GroupMember>()
            .HasOne(gm => gm.User)
            .WithMany(u => u.GroupMembers)
            .HasForeignKey(gm => gm.UserId);

        modelBuilder.Entity<GroupMember>()
            .HasOne(gm => gm.Group)
            .WithMany(g => g.GroupMembers)
            .HasForeignKey(gm => gm.GroupId);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.RegisteredByUser)
            .WithMany(u => u.RegisteredExpenses)
            .HasForeignKey(e => e.RegisteredByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.PaidByUser)
            .WithMany(u => u.PaidExpenses)
            .HasForeignKey(e => e.PaidByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Group)
            .WithMany(g => g.Expenses)
            .HasForeignKey(e => e.GroupId);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.PayerUser)
            .WithMany(u => u.SentPayments)
            .HasForeignKey(p => p.PayerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.ReceiverUser)
            .WithMany(u => u.ReceivedPayments)
            .HasForeignKey(p => p.ReceiverUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Group)
            .WithMany(g => g.Payments)
            .HasForeignKey(p => p.GroupId);

        modelBuilder.Entity<GroupMember>()
            .HasIndex(gm => new {gm.UserId, gm.GroupId})
            .IsUnique();

        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasPrecision(18, 2);
    }

    public DbSet<User> Users {get; set;} = null!;
    public DbSet<Group> Groups {get; set;} = null!;
    public DbSet<GroupMember> GroupMembers {get; set;} = null!;
    public DbSet<Expense> Expenses {get; set;} = null!;
    public DbSet<Payment> Payments {get; set;} = null!;
}
