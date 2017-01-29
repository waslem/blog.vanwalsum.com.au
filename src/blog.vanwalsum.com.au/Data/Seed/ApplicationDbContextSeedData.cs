using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using blog.vanwalsum.com.au.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using blog.vanwalsum.com.au.Models.Models;

namespace blog.vanwalsum.com.au.Data.Migrations
{
    public static class ApplicationDbContextSeedData
    {
        public static async void SeedData(this IServiceScopeFactory scopeFactory)
        {
            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                var adminRole = new ApplicationRole
                {
                    Name = "Administrator",
                    Description = "Website Administrator",
                    Created = DateTime.Now
                };

                var userRole = new ApplicationRole
                {
                    Name = "User",
                    Description = "User",
                    Created = DateTime.Now
                };

                if (!context.Roles.Any())
                {
                    var roleStore = new RoleStore<ApplicationRole>(context);
                    var result = roleStore.CreateAsync(adminRole);
                    var result2 = roleStore.CreateAsync(userRole);
                    
                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    var adminUser = new ApplicationUser
                    {
                        Email = "jamie@vanwalsum.com.au",
                        NormalizedEmail = "jamie@vanwalsum.com.au",
                        UserName = "jamie@vanwalsum.com.au",
                        NormalizedUserName = "jamie@vanwalsum.com.au",
                        PhoneNumber = "+61405054401",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        Posts = new List<Post>()
                    };

                    var standardUser = new ApplicationUser
                    {
                        Email = "jvw@westnet.com.au",
                        NormalizedEmail = "jvw@westnet.com.au",
                        UserName = "jvw@westnet.com.au",
                        NormalizedUserName = "jvw@westnet.com.au",
                        PhoneNumber = "+61405054401",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        Posts = new List<Post>()
                    };

                    var userStore = new UserStore<ApplicationUser>(context);

                    if (!context.Users.Any(u => u.UserName == standardUser.UserName))
                    {
                        var passwordUser = new PasswordHasher<ApplicationUser>();
                        var hashedUser = passwordUser.HashPassword(standardUser, "password");
                        standardUser.PasswordHash = hashedUser;

                        var resultStandard = userStore.CreateAsync(standardUser);
                        await userStore.AddToRoleAsync(standardUser, userRole.Name);
    
                        await context.SaveChangesAsync();
                    }
                    if (!context.Users.Any(u => u.UserName == adminUser.UserName))
                    {
                        var passwordAdmin = new PasswordHasher<ApplicationUser>();
                        var hashedAdmin = passwordAdmin.HashPassword(adminUser, "secret");
                        adminUser.PasswordHash = hashedAdmin;

                        var resultAdmin = userStore.CreateAsync(adminUser);
                        await userStore.AddToRoleAsync(adminUser, adminRole.Name);

                        await context.SaveChangesAsync();
                    }

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

                    var post1 = new Post
                    {
                        Title = "My First Post",
                        Category = context.Categories.First(c => c.Name == "System Engineering"),
                        published = true,
                        Created = DateTime.Now,
                        ShortDescription = "This is my first post",
                        UrlSlug = "My-First-Post",
                        Owner = context.Users.First(u => u.UserName == "jamie@vanwalsum.com.au"),
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
                        Owner = context.Users.First(u => u.UserName == "jamie@vanwalsum.com.au"),
                        HeaderImageUrl = "",
                        Meta = "",
                        SummaryImageUrl = "http://placehold.it/300x250",
                        TagPosts = new List<TagPosts>()
                    };

                    context.SaveChanges();

                    // find the tags to add to the post
                    Tag tag1 = context.Tags.First(t => t.Name == "Outlook");
                    Tag tag2 = context.Tags.First(t => t.Name == "Exchange");

                    // create the mapping objects
                    var TagPosts = new TagPosts
                    {
                        Tag = tag1,
                        Post = post1,
                        TagId = tag1.Id,
                        PostId = post1.Id
                    };

                    var TagPosts2 = new TagPosts
                    {
                        Tag = tag2,
                        Post = post1,
                        TagId = tag2.Id,
                        PostId = post1.Id
                    };

                    // create the mapping objects
                    var TagPosts3 = new TagPosts
                    {
                        Tag = tag1,
                        Post = post2,
                        TagId = tag1.Id,
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
                    post1.TagPosts.Add(TagPosts);
                    post1.TagPosts.Add(TagPosts2);
                    post2.TagPosts.Add(TagPosts3);
                    post2.TagPosts.Add(TagPosts4);
                    context.Add(post1);
                    context.Add(post2);

                    context.SaveChanges();
                }
            }
        }
    }
}
