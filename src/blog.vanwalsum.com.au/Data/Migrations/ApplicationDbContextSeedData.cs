using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using blog.vanwalsum.com.au.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace blog.vanwalsum.com.au.Data.Migrations
{
    public static class ApplicationDbContextSeedData
    {
        public static void SeedData(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                if (!context.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        Email = "jamie@vanwalsum.com.au",
                        NormalizedEmail = "jamie@vanwalsum.com.au",
                        UserName = "Admin",
                        NormalizedUserName = "ADMIN",
                        PhoneNumber = "+61405054401",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        Posts = new List<Post>()
                    };

                    if (!context.Users.Any(u => u.UserName == user.UserName))
                    {
                        var password = new PasswordHasher<ApplicationUser>();
                        var hashed = password.HashPassword(user, "secret");
                        user.PasswordHash = hashed;

                        var userStore = new UserStore<ApplicationUser>(context);
                        var result = userStore.CreateAsync(user);

                    }

                    

                    context.SaveChangesAsync();
                }
                if (!context.Categories.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category
                        {
                            Name = "System Engineering",
                            Description = "System Engineering posts",
                            UrlSlug = "SystemEngineering"
                        },
                        new Category
                        {
                            Name = "Network Engineering",
                            Description = "Network Engineering posts",
                            UrlSlug = "NetworkEngineering"
                        },
                        new Category
                        {
                            Name = "Servers",
                            Description = "Servers",
                            UrlSlug = "Servers"
                        },
                    };
                    context.AddRange(categories);
                    context.SaveChanges();
                }
                if (!context.Tags.Any())
                {
                    var tags = new List<Tag>
                    {
                        new Tag
                        {
                            Name = "MVC",
                            Description = "Model View Controller",
                            UrlSlug = "MVC"
                        },
                        new Tag
                        {
                            Name = "Drones",
                            Description = "RC Drones",
                            UrlSlug = "Drones"
                        },
                        new Tag
                        {
                            Name = "asp.net",
                            Description = "Microsoft's ASP.Net",
                            UrlSlug = "aspnet"
                        },
                        new Tag
                        {
                            Name = "Exchange",
                            Description = "Microsoft Exchange",
                            UrlSlug = "Exchange"
                        },
                        new Tag
                        {
                            Name = "Outlook",
                            Description = "Microsoft Outlook",
                            UrlSlug = "Outlook"
                        }
                    };
                    context.AddRange(tags);
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {

                    var post = new Post
                    {
                        Title = "My First Post",
                        Category = context.Categories.First(c => c.Name == "System Engineering"),
                        published = true,
                        Created = DateTime.Now,
                        ShortDescription = "This is my first post",
                        UrlSlug = "My-First-Post",
                        Owner = context.Users.First(u => u.UserName == "Admin"),
                        HeaderImageUrl = "",
                        Meta = "",
                        SummaryImageUrl = "http://placehold.it/300x250",
                        TagPosts = new List<TagPosts>()
                    };

                    var post2 = new Post
                    {
                        Title = "My second Post",
                        Category = context.Categories.First(c => c.Name == "System Engineering"),
                        published = true,
                        Created = DateTime.Now,
                        ShortDescription = "This is my second post",
                        UrlSlug = "My-second-Post",
                        Owner = context.Users.First(u => u.UserName == "Admin"),
                        HeaderImageUrl = "",
                        Meta = "",
                        SummaryImageUrl = "http://placehold.it/300x250",
                        TagPosts = new List<TagPosts>()
                    };
                    context.SaveChanges();

                    // find the tags to add to the post
                    Tag tag = context.Tags.First(t => t.Name == "Outlook");
                    Tag tag2 = context.Tags.First(t => t.Name == "Exchange");

                    // create the mapping objects
                    var TagPosts = new TagPosts
                    {
                        Tag = tag,
                        Post = post,
                        TagId = tag.Id,
                        PostId = post.Id
                    };

                    var TagPosts2 = new TagPosts
                    {
                        Tag = tag2,
                        Post = post,
                        TagId = tag2.Id,
                        PostId = post.Id
                    };

                    // create the mapping objects
                    var TagPosts3 = new TagPosts
                    {
                        Tag = tag,
                        Post = post2,
                        TagId = tag.Id,
                        PostId = post2.Id
                    };

                    var TagPosts4 = new TagPosts
                    {
                        Tag = tag2,
                        Post = post2,
                        TagId = tag2.Id,
                        PostId = post2.Id
                    };

                    // add the tagposts to the post
                    post.TagPosts.Add(TagPosts);
                    post.TagPosts.Add(TagPosts2);
                    post.TagPosts.Add(TagPosts3);
                    post.TagPosts.Add(TagPosts4);

                    // add the post to the db
                    context.Add(post);
                    context.SaveChanges();
                }
            }
        }
    }
}
