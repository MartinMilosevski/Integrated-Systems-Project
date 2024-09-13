using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<T> entities;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }


        public void Delete(Guid? id)
        {
            entities.Remove(entities.FirstOrDefault(u=>u.Id == id));
            context.SaveChanges();
        }

        public T Get(Guid? id)
        {
            return entities.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public void Insert(T entity)
        {
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
