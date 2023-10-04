using IBlogWebApp.Data;
using IBlogWebApp.Data.Enum;
using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using IBlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IBlogWebApp.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository,
            IPhotoService photoService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogPost> blogs = await _blogPostRepository.GetAllBlogPostsAsync();
            return View(blogs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            BlogPost blogPost = await _blogPostRepository.GetByIdAsync(id);
            _httpContextAccessor.HttpContext.Session.SetInt32("CurrentBlogPostId", blogPost.Id);
            return View(blogPost);
        }
        public IActionResult GetBlogByAuthor()
        {
            BlogAuthorViewModel blogVM = new BlogAuthorViewModel();
            return View(blogVM);
        }
        public async Task<IActionResult> BlogByAuthor(BlogAuthorViewModel blogVM)
        {
            var author = blogVM.Author.ToString();
            IEnumerable<BlogPost> blogPosts = await _blogPostRepository.GetBlogByAuthor(author);
            return View(blogPosts);           
        }

        public IActionResult GetBlogByCategory()
        {
            var blogVM = new BlogCategoryViewModel();
            return View(blogVM);
        }
        public async Task<IActionResult> BlogByCategory(BlogCategoryViewModel category)
        {
            var blogCategory = (BlogPostCategory)category.BlogPostCategory;
            IEnumerable<BlogPost> blogPosts = await _blogPostRepository.GetBlogPostByCategory(blogCategory);
            return View(blogPosts);
        }
        public async Task<IActionResult> UserBlog(string id)
        {
            
            var blogs = await _blogPostRepository.GetBlogPostByUser(id);
            return View(blogs);
        }
        public IActionResult Create()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();  
            var createBlogVM = new CreateBlogViewModel { AppUserId = curUserId };
            return View(createBlogVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogViewModel blogVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(blogVM.Image);
                var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var user = await _userRepository.GetById(curUserId);

                var blogPost = new BlogPost
                {
                    Title = blogVM.Title,
                    Content = blogVM.Content,
                    Image = result.Url.ToString(),
                    CreatedAt = DateTime.Now,
                    Author = user.UserName,
                    AppUserId = blogVM.AppUserId,
                    BlogPostCategory = blogVM.BlogPostCategory,
                    Comments = blogVM.Comments,
                };
                _blogPostRepository.Add(blogPost);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(blogVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _blogPostRepository.GetByIdAsync(id);
            if (blog == null)
                return View("Error");
            var blogVM = new EditBlogViewModel
            {
                Title = blog.Title,
                Content = blog.Content,
                CreatedAt = blog.CreatedAt,
                Author = blog.Author,
                AppUserId = blog.AppUserId,
                BlogPostCategory = blog.BlogPostCategory,
                Comments = blog.Comments,
                URL = blog.Image,
            };
            return View(blogVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBlogViewModel blogVM) 
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetById(curUserId);
            
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", blogVM);
            }
            var blogPost = await _blogPostRepository.GetByIdNoTrackingAsync(id);

            if(blogPost != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(blogPost.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(blogVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(blogVM.Image);
                var blog = new BlogPost
                {
                    Id = id,
                    Title = blogVM.Title,
                    Content = blogVM.Content,
                    Image = photoResult.Url.ToString(),
                    CreatedAt = blogVM.CreatedAt,
                    LastUpdated = DateTime.Now,
                    Comments = blogVM.Comments,
                    Author = user.UserName,
                    AppUserId = blogVM.AppUserId,
                    BlogPostCategory = blogVM.BlogPostCategory
                };
                _blogPostRepository.Update(blog);
                return RedirectToAction("Index");
            }
            return View(blogVM);

        }
        public async Task<IActionResult> Delete (int id)
        {
            var blogDetails = await _blogPostRepository.GetByIdAsync(id);
            if (blogDetails == null)
            {
                return View("Error");
            }
            return View(blogDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blogDetails = await _blogPostRepository.GetByIdAsync(id);
            if (blogDetails == null)
                return View("Error");
            _blogPostRepository.Delete(blogDetails);
            return RedirectToAction("Index");   
        }
    }
}   
