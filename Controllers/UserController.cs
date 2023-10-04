using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using IBlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IBlogWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userVM = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userVM);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetById(id);
            var userVm = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                EmailAddress = user.Email,
                LastName = user.LastName,
                State   = user.State,
                City    = user.City,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVm);
        }
        public async Task<IActionResult> UserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _userRepository.GetById(curUserId);
            var userVm = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                EmailAddress = user.Email,
                LastName = user.LastName,
                State = user.State,
                City = user.City,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVm);
        }
    }
}
