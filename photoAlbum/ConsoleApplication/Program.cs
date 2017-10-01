using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using BLL.Interface.Entities;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
//using ORM;
//using ORM.Entities;

namespace ConsoleApplication
{
    class Program
    {

        public static void  Main(string[] args)
        {
            Ma();
            Console.ReadLine();
        }

        public static void Ma()
        {

            //ModelContext modelContext = ModelContext.Create();
            //////var filter = Builders<RestaurantDb>.Filter.Eq(r => r.Borough, "Brooklyn");
            //////var restaurants = modelContext.Restaurants.FindSync<RestaurantDb>(filter).ToList();
            //////var restaurant = modelContext.Restaurants.FindSync<RestaurantDb>(filter).FirstOrDefault();
            ////// Console.WriteLine(restaurant);

            //User user = new User();
            ////user.Id = 1;
            //user.Email = "afsdadf";
            //user.Password = "sadsasa";
            //user.Login = "asdsa";
            //modelContext.Users.InsertOne(user);

            ////Author author = new Author();
            ////author.Id = 4;
            ////author.Name = "Andrew";
            ////Comment c = new Comment();
            ////c.Author = author;
            ////c.Text = "dsasasa adfadfdaaf";
            ////c.Posted = DateTime.Now;
            ////c.PhotoId = 6;
            ////c.Id = 78;

            ////modelContext.Comments.InsertOne(c);

            ////IEnumerable<Comment> users = modelContext.Comments.Find(r => true).ToEnumerable();
            ////foreach (var u in users)
            ////{
            ////    Console.WriteLine(u);
            ////}
            
            PhotoRepository rep = new PhotoRepository();
            //rep.AddTag(1, "#ballet");
            //rep.AddTag(2, "#ballet");
            //rep.AddTag(1, "#steven");
            //rep.AddTag(2, "#steven");
            //rep.AddTag(1, "#sarah");
            //rep.AddTag(1, "#summer");
            //rep.AddTag(2, "#summer");

            //foreach (var VARIABLE in rep.FindTag("#s"))
            //{
            //    Console.WriteLine(VARIABLE);
            //}

            //IEnumerable<DalPhoto> p = rep.GetByTag("#", 0, 10);
            //foreach (var VARIABLE in p)
            //{
            //    Console.WriteLine(p);
            //}

            //PhotoService s = new PhotoService(new PhotoRepository());
            //IEnumerable<BllPhoto> p = s.GetAll(0, 1);
            //foreach (var VARIABLE in p)
            //{
            //    Console.WriteLine(VARIABLE.Id);
            //}

            //AccountService service= new AccountService(new UserRepository());
            //service.CreateUser(new BllUser()
            //{
            //    Login = "steven_mcrae",
            //    Email = "stevenmcrae@gmail.com",
            //    Password = Crypto.HashPassword("111111"),
            //    Roles = new []{"User"}
            //});

            //DalPhoto p = rep.GetById(4);
            ////p.UploadDate = new DateTime(2017, 9, 21, 15, 30, 30 );
            ////rep.Update(p);
            //p.
            //rep.Update(p);

            //CommentRepository c = new CommentRepository();
            ////DalComment com = c.GetById(4);
            ////com.Text = "oh, miss you too.I'm looking forward to our meeting next friday)";
            ////c.Update(com);
            //DalComment c1 = new DalComment()
            //{
            //    Id = 5,
            //    PhotoId = 4,
            //    Author = new DalAuthor()
            //    {
            //        Id = 6,
            //        Name = "yasmine_naghdi"
            //    },
            //    Posted = new DateTime(2017, 9, 30, 22, 00, 30),
            //    Text = "you're as beautiful as ever"
            //};
            //c.Insert(c1);

            //p.NumberOfLikes = 0;
            //p.UserLikes = new List<int>(0);
            //rep.Update(p);
            //p.UserId = 1;
            //rep.Update(p);
            //p = rep.GetById(2);
            //p.UploadDate = new DateTime(2017, 9, 21, 19, 30, 30);
            ////p.UserId = 1;
            //rep.Update(p);

            //UserRepository rp = new UserRepository();
            //DalUser u = rp.GetById(1);
            //u.Roles = new List<string>() {"User", "Admin"};
            //rp.Update(u);

            //DalPhoto p = rep.GetById(11);
            ////p.Tags = new List<string>() {"#steven", "#ballet", "#muscular", "#royalballet"};
            //p.UploadDate = new DateTime(2017, 9, 21, 18, 30, 30);
            //rep.Update(p);

            Console.ReadKey();
        }
    }
}
