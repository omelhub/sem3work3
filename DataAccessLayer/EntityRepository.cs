using Model3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        StudentContext context = new StudentContext();

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Create(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            T item = context.Set<T>().Find(id);
            if (item != null)
                context.Set<T>().Remove(item);
        }

        public void DeleteAll()
        {
            context.Set<T>().RemoveRange(context.Set<T>());
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

}
