using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.DataContexts;
using ToDoActivities.DAL.Interfaces;
using ToDoActivities.DAL.ViewModels;
using ToDoActivities.Models;

namespace ToDoActivities.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AppDbContext _appDbContext;
        private DbSet<T> table = null;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            table = _appDbContext.Set<T>();
        }

        public void Delete(object id)
        {
            table.Remove((T) id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await table.FindAsync(id);
        }

        public async void Insert(T obj)
        {
            await table.AddAsync(obj);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            table.Update(obj);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}