using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        // su dung 'Fluent API' de tao table
        // can override OnModelCreating() de su dung
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // set key
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikeeId});

            //HasOne(): dau quan he 1
            //WithMany(): duoi quan he nhieu
            // one 'Likee' can have many 'Likers'
            //Restrict: sau khi xoa 1 'Like', 'User' k bi delete
            builder.Entity<Like>()
                .HasOne(u => u.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // 1 sender co the gui nhieu message => tao relationships 1-n giua 'sender' o table 
            // 'Messages' voi 'messagesSent' o table User
            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
            
            // tuong tu 1 recipient (ng nhan) co the nhan nhieu messages
            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}