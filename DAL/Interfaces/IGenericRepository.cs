using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.ViewModels;

namespace ToDoActivities.DAL.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class  
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }
}
