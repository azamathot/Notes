using Microsoft.EntityFrameworkCore;
using Notes.Dal.Entities;

namespace Notes.Dal
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.Migrate();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteShare> NoteShares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteShare>()
                .HasKey(ns => new { ns.NoteId, ns.UserId });
        }
    }
}
