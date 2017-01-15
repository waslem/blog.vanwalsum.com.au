using blog.vanwalsum.com.au.Data;
using blog.vanwalsum.com.au.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.vanwalsum.com.au.Models.Repository
{
    public class BlogRepository
    {
        public ApplicationDbContext _context { get; private set; }

        public BlogRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public List<Post> SelectAllPosts()
        {
            return _context.Posts.ToList();
        }

        public List<Post> SelectPostById()
        {
            return _context.Posts.ToList();
        }

        public bool InsertPost(Post newPost)
        {
            try
            {
                _context.Entry(newPost).State = EntityState.Added;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public SideBarItems GetSideBar(int latestPostCount)
        {
            var sideBar = new SideBarItems { Tags = new List<string>(), Categories = new List<string>(), LatestPosts = new List<PostDetails>() };
            if (_context.Categories.Any())
            {
                foreach (var cat in _context.Categories)
                    sideBar.Categories.Add(cat.Name);
            }
            if (_context.Tags.Any())
            {
                foreach (var tag in _context.Tags)
                    sideBar.Tags.Add(tag.Name);
            }

            if (_context.Posts.Any())
            {
                foreach (var latestPosts in _context.Posts.OrderBy(u => u.Created).Take(latestPostCount))
                {
                    sideBar.LatestPosts.Add(new PostDetails
                    {
                        PostHeader = latestPosts.Title,
                        PostID = latestPosts.Id,
                        PostSlug = latestPosts.UrlSlug
                    }
                    );
                }
            }

            return sideBar;
        }
        // view model queries
        public List<PostSummaryViewModel> GetTopPostSummaries(int? count)
        {
            var posts = from p in _context.Posts
                        select new PostSummaryViewModel ()
                        {
                            ID = p.Id,
                            Title = p.Title,
                            ShortDescription = p.ShortDescription,
                            Created = p.Created.ToString("dd/MM/yyyy"),
                            WrittenBy = p.Owner.UserName,
                            SummaryImageUrl = p.SummaryImageUrl,
                            Slug = p.UrlSlug
                        };
            if (count != null)
                return posts.Take((int)count).ToList();
            else
                return posts.ToList();
        }

        public List<PostSummaryViewModel> GetPostSummariesByCategory(string category)
        {
            var posts = from p in _context.Posts.Where(p => p.Category.Name.ToUpper() == category.ToUpper())
                        select new PostSummaryViewModel()
                        {
                            ID = p.Id,
                            Title = p.Title,
                            ShortDescription = p.ShortDescription,
                            Created = p.Created.ToString("dd/MM/yyyy"),
                            WrittenBy = p.Owner.UserName,
                            SummaryImageUrl = p.SummaryImageUrl,
                            Slug = p.UrlSlug,
                            CategoryName = p.Category.Name
                        };
            return posts.ToList();
        }

        public List<PostSummaryViewModel> GetPostSummariesByTag(int TagId)
        {
            var posts = from p in _context.TagPosts.Include(p => p.Post)
                        .Where(pt => pt.TagId == TagId)
                        .Select(p => p.Post)
                        select new PostSummaryViewModel()
                        {
                            ID = p.Id,
                            Title = p.Title,
                            ShortDescription = p.ShortDescription,
                            Created = p.Created.ToString("dd/MM/yyyy"),
                            WrittenBy = p.Owner.UserName,
                            SummaryImageUrl = p.SummaryImageUrl,
                            Slug = p.UrlSlug
                        };

            return posts.ToList();
        }

        public List<Post> GetPostsByTagId(int tagId)
        {
            return _context.TagPosts.Include(p => p.Post)
                       .Where(pt => pt.TagId == tagId)
                       .Select(pt => pt.Post)
                       .ToList();
        }

        public List<Post> GetPostsByCategory(string category)
        {

            return _context.Posts.Where(u => u.Category.Name.ToUpper() == category.ToUpper()).ToList();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public bool InsertCategory(Category category)
        {
            try
            {
                _context.Entry(category).State = EntityState.Added;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
