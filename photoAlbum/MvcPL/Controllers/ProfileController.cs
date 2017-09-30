using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public const int ImagesOnPage = 1;
        private readonly IAccountService accountService;
        private readonly IPhotoService photoService;

        public ProfileController(IAccountService accountService, IPhotoService photoService)
        {
            this.accountService = accountService;
            this.photoService = photoService;
        }

        public ActionResult Index()
        {
            ProfileViewModel profileViewModel = accountService.
                GetUserByLogin(User.Identity.Name).ToProfileViewModel();

            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = 1,
                PageSize = ImagesOnPage,
                TotalItems = photoService.CountByUserId(userId)
            };
            IEnumerable<PhotoViewModel> photos = photoService.GetByUserId(userId, 0, ImagesOnPage * 1)
                .Select(p => p.ToPhotoViewModel());
            ViewBag.Photos = new PaginationViewModel<PhotoViewModel> { PageInfo = pageInfo, Items = photos };

            return View("Index", profileViewModel);
        }

        public ActionResult Search(int page = 1)
        {
            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = ImagesOnPage,
                TotalItems = photoService.CountByUserId(userId)
            };

            if (Request.IsAjaxRequest())
            {
                IEnumerable<PhotoViewModel> photos = photoService.GetByUserId(userId, pageInfo.Skip, pageInfo.PageSize)
                    .Select(p => p.ToPhotoViewModel());

                PaginationViewModel<PhotoViewModel> pagedPhotos =
                    new PaginationViewModel<PhotoViewModel> { PageInfo = pageInfo, Items = photos };

                return Json(pagedPhotos, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<PhotoViewModel> photos2 = photoService.GetByUserId(userId, 0, ImagesOnPage * page)
                .Select(p => p.ToPhotoViewModel());

            PaginationViewModel<PhotoViewModel> pagedPhotos2 =
                new PaginationViewModel<PhotoViewModel> { PageInfo = pageInfo, Items = photos2 };

            //return View("Index", pagedPhotos2);
            return new EmptyResult();
        }

        public ActionResult ShowImage()
        {
            return File(accountService.GetUserByLogin(User.Identity.Name).ProfilePhoto, "image/jpeg");
        }

        [AllowAnonymous]
        public ActionResult ShowProfilePhoto(int userId)
        {
            return File(accountService.GetUserById(userId).ProfilePhoto, "image/jpeg");
        }

        public ActionResult UploadPhoto()
        {
            return View("UploadPhoto");
        }

        [HttpPost]
        public ActionResult UploadPhoto(UploadPhotoViewModel photo)
        {
            photoService.Add(photo.ToBllPhoto(accountService.GetUserByLogin(User.Identity.Name).Id));
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult EditeProfile()
        {
            ProfileInfoViewModel profileViewModel = accountService.
                GetUserByLogin(User.Identity.Name).ToProfileInfoViewModel();
            return View("EditeProfile", profileViewModel);
        }

        [HttpPost]
        public ActionResult EditePtofile(EditProfileViewModel model)
        {
            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;
            accountService.EditeUserPtofile(userId, model.Name, model.ImageFile.ToByteArray());
            return RedirectToAction("Index", "Profile");
        }

    }
}