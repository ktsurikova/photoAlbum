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
        public const int ImagesOnPage = 1;

        private readonly IPhotoService photoService;

        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
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
            //TO DO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            return View("Index");
        }

        //Pagination
        //[ValidateAntiForgeryToken]
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
            return View("PhotoDetails", photoService.GetById(id).ToPhotoViewModel());
        }

    }
}