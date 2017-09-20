using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;

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
    }
}