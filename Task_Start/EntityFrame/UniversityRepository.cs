using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;


namespace EntityFrame
{
  public  class UniversityRepository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Items { get; set; }
        UniversityContext _context;
        public UniversityRepository(UniversityContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
           Items= _context.Set<T>();

        }

        public T Create(T entity)
        {
           var result= _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T GetById(int id)
        {
           var item= Items.Find(id);
            return item;
        }

        public void Remove(int id)
        {
            var item = Items.Find(id);
            _context.Remove(item);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }



        List<T> IRepository<T>.GetAll()
        {
            return Items.ToList<T>();
        }
    }
}
