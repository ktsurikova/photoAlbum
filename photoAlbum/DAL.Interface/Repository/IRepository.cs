using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        //IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
