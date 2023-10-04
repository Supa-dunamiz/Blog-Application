using CloudinaryDotNet.Actions;
using IBlogWebApp.Interfaces;
using IBlogWebApp.Models;
using IBlogWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IBlogWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        private void MapUserEdit(AppUser user, 
            EditUserProfileViewModel editVM, 
            ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.UserName;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.State = editVM.State;
            user.City = editVM.City;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }
        public async Task<IActionResult> Index()
        {
            var userBlogs = await _dashboardRepository.GetAllUserBlogs();
            var dashboardVM = new DashboardViewModel()
            {
                BlogPosts = userBlogs
            };
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(curUserId);
            if (user == null)
            {
                return NotFound();
            }
            var userVM = new EditUserProfileViewModel()
            {
                Id = curUserId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                State = user.State,
                ProfileImageUrl = user.ProfileImageUrl,
            };
            return View(userVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", userVM);
            }
            var user = await _dashboardRepository.GetUserByIdNoTracking(userVM.Id);
            
            if(user.ProfileImageUrl == null || user.ProfileImageUrl == "") 
            {
                var photoResult = await _photoService.AddPhotoAsync(userVM.Image);
                MapUserEdit(user, userVM, photoResult);
                _dashboardRepository.Update(user);
                 return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete picture");
                }
                var photoResult = await _photoService.AddPhotoAsync(userVM.Image);
                MapUserEdit(user, userVM, photoResult);
                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
        }
    }
}
