using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using blog.vanwalsum.com.au.Models;

namespace blog.vanwalsum.com.au.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<TagPosts> TagPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
     
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasMany(u => u.Posts).WithOne(u => u.Owner);

            builder.Entity<Post>().HasOne(u => u.Category);

            builder.Entity<TagPosts>().HasKey(x => new { x.PostId, x.TagId });

            builder.Entity<TagPosts>().HasOne(x => x.Post)
                .WithMany(p => p.TagPosts)
                .HasForeignKey(x => x.PostId);

            builder.Entity<TagPosts>().HasOne(x => x.Tag)
                .WithMany(p => p.TagPosts)
                .HasForeignKey(x => x.TagId);


        }
    }
}
