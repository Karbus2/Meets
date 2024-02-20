using Azure;
using Humanizer;
using Meets.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Meets.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<ApplicationUserMeeting> ApplicationUserMeetings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(au => 
            {
                au.Property(au => au.Version)
                  .IsRowVersion();

                au.HasMany(au => au.InvitationsSent)
                  .WithOne()
                  .OnDelete(DeleteBehavior.NoAction)
                  .HasForeignKey(inv => inv.RequesterId)
                  .IsRequired();

                au.HasMany(au => au.InvitationsReceived)
                  .WithOne()
                  .OnDelete(DeleteBehavior.NoAction)
                  .HasForeignKey(inv => inv.AddresseeId)
                  .IsRequired();

                au.HasMany(au => au.Friends)
                    .WithOne(f => f.ApplicationUser)
                    .HasPrincipalKey(au => au.Id)
                    .HasForeignKey(f => f.ApplicationUserId)
                    .IsRequired();
            });

            builder.Entity<Meeting>(m =>
            {
                m.Property(m => m.Version)
                 .IsRowVersion();

                m.Property(m => m.PrivacyPolicy)
                  .HasConversion<string>();

                m.HasMany(m => m.ApplicationUsers)
                 .WithMany(m => m.Meetings)
                 .UsingEntity<ApplicationUserMeeting>();

                m.Property(m => m.CreatedDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
                m.Property(m => m.UpdateDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
            });
            builder.Entity<ApplicationUserMeeting>(aum =>
            {
                aum.Property(aum => aum.Version)
                  .IsRowVersion();

                aum.Property(aum => aum.Role)
                  .HasConversion<string>();
                aum.Property(aum => aum.JoiningStatus)
                  .HasConversion<string>();

                aum.Property(aum => aum.CreatedDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
                aum.Property(aum => aum.UpdateDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
            });
            builder.Entity<Invitation>(inv => 
            {
                inv.Property(inv => inv.Version)
                 .IsRowVersion();

                inv.Property(inv => inv.RequesterId)
                 .HasMaxLength(450);
                inv.Property(inv => inv.AddresseeId)
                 .HasMaxLength(450);
                inv.HasKey(inv => new { inv.RequesterId, inv.AddresseeId });

                inv.Property(inv => inv.CreatedDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
            });
            builder.Entity<Friend>(f =>
            { 
                f.Property(f => f.Version)
                 .IsRowVersion();
                f.Property(f => f.ApplicationUserId)
                 .HasMaxLength(450);
                f.Property(f => f.FriendId)
                 .HasMaxLength(450);
                f.HasKey(f => new { f.ApplicationUserId, f.FriendId });
                f.Property(f => f.CreatedDate)
                 .HasDefaultValueSql("GETDATE()")
                 .IsRequired();
            });
        }
    }
}
