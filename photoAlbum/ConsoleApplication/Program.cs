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

            DalPhoto p = rep.GetById(1);
            p.UserId = 1;
            rep.Update(p);
            p = rep.GetById(2);
            p.UserId = 1;
            rep.Update(p);


            Console.ReadKey();
        }
    }
}
