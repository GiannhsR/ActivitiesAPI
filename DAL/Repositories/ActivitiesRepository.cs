using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.DataContexts;
using ToDoActivities.DAL.ViewModels;

namespace ToDoActivities.DAL.Repositories
{
    public class ActivitiesRepository : IActivitiesRepository, IDisposable
    {
        private AppDbContext _appDbContext;
        private bool disposed = false;

        public ActivitiesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task AddActivityAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteActivityByIdAsync(long activityId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActivityViewModel>> GetActivitiesAsync()
        {
            return await _appDbContext.Activity
                 .Select(s => new ActivityViewModel
                 {
                     ActivityId = s.ActivityId,
                     Date = s.Date,
                     Name = s.Name,
                     Description = s.Description,
                     IsCompleted = s.IsCompleted,
                     IsDeleted = s.IsDeleted
                 })
                 .ToListAsync();
        }

        public Task<ActivityViewModel> GetActivityByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityViewModel> GetActivityByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateActivityByIdAsync(long activityId, ActivityViewModel _activityViewModel)
        {
            throw new NotImplementedException();
        }

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
