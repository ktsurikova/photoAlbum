using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Services;
using DAL;
using DAL.Concrete;
using MongoDB.Bson;
using MongoDB.Driver;
using ORM;
using ORM.Entities;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            var theModel = GetThePictures();
            PhotoRepository r = new PhotoRepository();
            r.DislikePhoto(theModel[0].Id, 5);
            //r.ChangeName(1, "VeryBeautiful");
            //ViewBag.H = r.GetId();
            return View(theModel);
        }

        public ActionResult AddPicture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPicture(HttpPostedFileBase theFile)
        {
            if (theFile.ContentLength > 0)
            {
                //get the file's name
                string theFileName = Path.GetFileName(theFile.FileName);

                //get the bytes from the content stream of the file
                byte[] thePictureAsBytes = new byte[theFile.ContentLength];
                using (BinaryReader theReader = new BinaryReader(theFile.InputStream))
                {
                    thePictureAsBytes = theReader.ReadBytes(theFile.ContentLength);
                }

                //convert the bytes of image data to a string using the Base64 encoding
                //string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

                //create a new mongo picture model object to insert into the db
                Photo thePicture = new Photo()
                {
                    Id = 2,
                    Image = thePictureAsBytes
                };

                //insert the picture object
                bool didItInsert = InsertPictureIntoDatabase(thePicture);

                if (didItInsert)
                    ViewBag.Message = "The image was updated successfully";
                else
                    ViewBag.Message = "A database error has occured";
            }
            else
                ViewBag.Message = "You must upload an image";

            return View();
        }

        /// <summary>
        /// This method will insert the picture into the db
        /// </summary>
        /// <param name="thePicture"></param>
        /// <returns></returns>
        private bool InsertPictureIntoDatabase(Photo thePicture)
        {
            // var thePictureColleciton = GetPictureCollection();
            ModelContext.Create().Photos.InsertOne(thePicture);

            return true;
        }

        /// <summary>
        /// This method will return just the id's and filenames of the pictures to use to retrieve the image from the db
        /// </summary>
        /// <returns></returns>
        private List<Photo> GetThePictures()
        {
            //var thePictureColleciton = ModelContext.Create().Photos;
            //var thePictureCursor = thePictureColleciton.Find(i => true);

            //PhotoService service = new PhotoService(new PhotoRepository());
            PhotoRepository service = new PhotoRepository();
            return service.GetAll().Select(u => u.ToOrmPhoto()).ToList();

            //use SetFields to just return the id and the name of the picture instead of the entire document
            //thePictureCursor.SetFields(Fields.Include("_id", "FileName"));

            //return thePictureCursor.ToList() ?? new List<Photo>();
        }

        /// <summary>
        /// This action will return an image result to render the data from the picture as a jpeg
        /// </summary>
        /// <param name="id">id of the picture as a string</param>
        /// <returns></returns>
        public ActionResult ShowPicture(int id)
        {
            var thePictureColleciton = ModelContext.Create().Photos;

            //get pictrue document from db
            var thePicture = GetThePictures().FirstOrDefault(i => i.Id == id);
            //thePictureColleciton.Find(i => i.Id == id).FirstOrDefault();

            //transform the picture's data from string to an array of bytes
            //var thePictureDataAsBytes = Convert.FromBase64String(thePicture.Image);

            //return array of bytes as the image's data to action's response. We set the image's content mime type to image/jpeg
            return File(thePicture.Image, "image/jpeg");
            // return new FileContentResult(thePictureDataAsBytes, "image/jpeg");
        }

        /// <summary>
        /// This will return the mongoDB picture collection object to use dor data related actions
        /// </summary>
        /// <returns></returns>
        //private MongoCollection<MongoPictureModel> GetPictureCollection()
        //{
        //    //set this to what ever your connection is or from config
        //    var theConnectionString = "mongodb://localhost";

        //    //get the mongo db client object
        //    var theDBClient = new MongoClient(theConnectionString);

        //    //get reference to db server
        //    var theServer = theDBClient.GetServer();

        //    //gets the database , if it doesn't exist it will create a new one
        //    string databaseName = "PictureApplication";//replace with whatever name you choose
        //    var thePictureDB = theServer.GetDatabase(databaseName);

        //    //finally attempts to get a collection, if not there it will make a new one
        //    string theCollectionName = "pictures";
        //    var thePictureColleciton = thePictureDB.GetCollection<MongoPictureModel>(theCollectionName);

        //    return thePictureColleciton;
        //}
    }
}