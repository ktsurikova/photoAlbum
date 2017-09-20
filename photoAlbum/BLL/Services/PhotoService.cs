using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IPhotoRepository repository)
        {
            photoRepository = repository;
        }

        public IEnumerable<BllPhoto> GetAll()
        {
            return photoRepository.GetAll().Select(p => p.ToBllPhoto());
        }

        public void Add(BllPhoto photo)
        {
            throw new NotImplementedException();
        }
    }
}
