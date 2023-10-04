using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using IBlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IBlogWebApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public CommentController(ICommentRepository commentRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public IActionResult Create()
        {
            var commentVM = new CreateCommentViewModel();    
            return View(commentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentViewModel commentVM)
        {
            if(ModelState.IsValid)
            {
                int? blogPostId = HttpContext.Session.GetInt32("CurrentBlogPostId");
                var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
                var user = await _userRepository.GetById(curUserId);
                Comment comment = new Comment
                {
                    
                    Author = user.UserName,
                    Content = commentVM.Content,
                    CreatedAt = DateTime.Now,
                    BlogPostId = blogPostId,
                };
                _commentRepository.Add(comment);
                return RedirectToAction("Index", "BlogPost");
            }
            return View(commentVM);
        
        
        
        
        }
    }


}
