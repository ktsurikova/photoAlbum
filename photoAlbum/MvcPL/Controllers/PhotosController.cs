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
    public class PhotosController : Controller
    {
        private readonly IPhotoService photoService;

        public PhotosController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Search(string tag)
        {
            IEnumerable<PhotoViewModel> photos = photoService.GetByTag(tag, 0, 12)
                .Select(p => p.ToPhotoViewModel());
            if (Request.IsAjaxRequest())
            {
                return Json(photos.ToList(), JsonRequestBehavior.AllowGet);
            }
            return View("Search", photos);
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