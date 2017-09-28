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

        public IEnumerable<BllPhoto> GetAll(int skip, int take)
        {
            return photoRepository.GetAll(skip, take).Select(p => p.ToBllPhoto());
        }

        public void Add(BllPhoto photo)
        {
            photoRepository.Insert(photo.ToDalPhoto());
        }

        public IEnumerable<string> FindTags(string tag)
        {
            return photoRepository.FindTag(tag);
        }

        public int CountByTag(string tag)
        {
            if (tag == string.Empty)
                return photoRepository.CountAll();
            return photoRepository.CountByTag(tag);
        }

        public IEnumerable<BllPhoto> GetByTag(string tag, int skip, int take)
        {
            if (tag == string.Empty)
                return photoRepository.GetAll(skip, take).Select(p => p.ToBllPhoto());
            return photoRepository.GetByTag(tag, skip, take).Select(p => p.ToBllPhoto());
        }

        public BllPhoto GetById(int id)
        {
            return photoRepository.GetById(id).ToBllPhoto();
        }

        public IEnumerable<BllPhoto> GetByUserId(int userId, int skip, int take)
        {
            return photoRepository.GetByUserId(userId, skip, take).Select(p => p.ToBllPhoto());
        }

        public int CountByUserId(int id)
        {
            return photoRepository.CountByUserId(id);
        }
    }
}
