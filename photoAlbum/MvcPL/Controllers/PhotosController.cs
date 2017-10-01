using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MvcPL.Infrastructure;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class PhotosController : Controller
    {
        public const int ImagesOnPage = 6;
        public const int CommentOnPage = 3;

        private readonly IPhotoService photoService;
        private readonly IAccountService accountService;

        public PhotosController(IPhotoService photoService, IAccountService accountService)
        {
            this.photoService = photoService;
            this.accountService = accountService;
        }

        //Pagination
        public ActionResult Index(int page = 1)
        {
            PhotoPageInfo pageInfo = new PhotoPageInfo { PageNumber = page, PageSize = ImagesOnPage, Tag = string.Empty, 
                TotalItems = photoService.CountByTag(string.Empty) };
            IEnumerable<PhotoViewModel> photos = photoService.GetAll(0, ImagesOnPage*page)
                .Select(p=>p.ToPhotoViewModel());
            return View(new PaginationViewModel<PhotoViewModel> {PageInfo = pageInfo, Items = photos});
        }

        public ActionResult Find(string term)
        {
            IEnumerable<string> tags = photoService.FindTags(term);

            var projection = from t in tags
                select new
                {
                    label = t,
                    value = t
                };
            if (Request.IsAjaxRequest())
            {
                return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
            }
            return View("Index");
        }

        public ActionResult Search(string tag = "", int page = 1)
        {
            PhotoPageInfo pageInfo = new PhotoPageInfo
            {
                PageNumber = page,
                PageSize = ImagesOnPage,
                Tag = tag,
                TotalItems = photoService.CountByTag(tag)
            };

            if (Request.IsAjaxRequest())
            {
                IEnumerable<PhotoViewModel> photos = photoService.GetByTag(tag, pageInfo.Skip, pageInfo.PageSize)
                    .Select(p => p.ToPhotoViewModel());

                PaginationViewModel<PhotoViewModel> pagedPhotos =
                    new PaginationViewModel<PhotoViewModel> { PageInfo = pageInfo, Items = photos };

                return Json(pagedPhotos, JsonRequestBehavior.AllowGet);
            }

            IEnumerable<PhotoViewModel> photos2 = photoService.GetByTag(tag, 0, ImagesOnPage*page)
                .Select(p => p.ToPhotoViewModel());

            PaginationViewModel<PhotoViewModel> pagedPhotos2 =
                new PaginationViewModel<PhotoViewModel> {PageInfo = pageInfo, Items = photos2};

            return View("Index", pagedPhotos2);
        }


        public ActionResult ShowImage(int id)
        {
            return File(photoService.GetById(id).Image, "image/jpeg");
        }

        public ActionResult PhotoDetails(int id)
        {
            PhotoDetailsViewModel photo = photoService.GetById(id).ToPhotoDetailsViewModel();
            photo.Owner = accountService.GetUserById(photo.UserId).ToPhotoOwnerViewModel();
            if (Request.IsAuthenticated)
            {
                photo.CurrentUserId = accountService.GetUserByLogin(User.Identity.Name).Id;
            }

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = 1,
                PageSize = CommentOnPage,
                TotalItems = photoService.CountCommentByPhotoId(id)
            };
            IEnumerable<CommentViewModel> comments = photoService.GetCommentsByPhotoId(id, 0, CommentOnPage)
                .Select(p => p.ToCommentViewModel());

            ViewBag.Comments = new PaginationViewModel<CommentViewModel> {PageInfo = pageInfo, Items = comments};

            return PartialView("_PhotoDetails", photo);
        }

        [Authorize]
        public ActionResult LikePhoto(int photoId)
        {
            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;
            photoService.LikePhoto(userId, photoId);
            PhotoRatingViewModel photo = photoService.GetById(photoId).ToPhotoRatingViewModel();
            return PartialView("_LikePhoto", photo);
        }

        [Authorize]
        public ActionResult DislikePhoto(int photoId)
        {
            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;
            photoService.DislikePhoto(userId, photoId);
            PhotoRatingViewModel photo = photoService.GetById(photoId).ToPhotoRatingViewModel();
            return PartialView("_DislikePhoto", photo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            int userId = accountService.GetUserByLogin(User.Identity.Name).Id;
            photoService.AddComment(model.ToBllComment(userId, User.Identity.Name));
            return RedirectToAction("LoadMoreComment", new {page = 0, id = model.PhotoId});
        }

        public ActionResult LoadMoreComment(int page, int id)
        {
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page+1,
                PageSize = CommentOnPage,
                TotalItems = photoService.CountCommentByPhotoId(id)
            };
            IEnumerable<CommentViewModel> comments = photoService.GetCommentsByPhotoId(id, 
                pageInfo.Skip, pageInfo.PageSize).Select(p => p.ToCommentViewModel());

            var model = new PaginationViewModel<CommentViewModel> { PageInfo = pageInfo, Items = comments };
            return PartialView("_Comments", model);
        }

        [Authorize]
        public ActionResult DeletePhoto(int photoId)
        {
            photoService.Delete(photoId);
            return RedirectToAction("Index");
        }
    
    }
}