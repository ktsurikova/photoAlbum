using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            kernel.Bind<IPhotoService>().To<PhotoService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IPhotoRepository>().To<PhotoRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
        }
    }
}
