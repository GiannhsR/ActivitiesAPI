using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.DataContexts;
using ToDoActivities.DAL.ViewModels;
using ToDoActivities.Models;

namespace ToDoActivities.DAL.RepositoryServices
{
    public class ActivitiesDbService
    {
        private AppDbContext _appDbContext;
        public ActivitiesDbService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddActivityAsync(ActivityViewModel activityViewModel)
        {
            var _activity = new Activity
            {

                Date = activityViewModel.Date,
                Name = activityViewModel.Name,
                Description = activityViewModel.Description,
                IsCompleted = activityViewModel.IsCompleted
            };
            _appDbContext.Add(_activity);
            await _appDbContext.SaveChangesAsync();
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

        public async Task<ActivityViewModel> GetActivityByIdAsync(long id)
        {
            return await _appDbContext.Activity
                   .Where(s => s.ActivityId == id)
                   .Select(s => new ActivityViewModel
                   {
                       ActivityId = s.ActivityId,
                       Name = s.Name,
                       Description = s.Description,
                       IsCompleted = s.IsCompleted,
                       Date = s.Date,
                       IsDeleted = s.IsDeleted
                   })
                   .SingleOrDefaultAsync();
        }

        public async Task<ActivityViewModel> GetActivityByNameAsync(string name)
        {
            return await _appDbContext.Activity
                   .Where(s => s.Name == name)
                   .Select(s => new ActivityViewModel
                   { ActivityId = s.ActivityId,
                       Name = s.Name,
                       Description = s.Description,
                       IsCompleted = s.IsCompleted,
                       Date = s.Date
                   })
                   .SingleOrDefaultAsync();
        }

        public async Task UpdateActivityByIdAsync(long activityId, ActivityViewModel _activityViewModel)
        {
            var ActivityToBeUpdated = await _appDbContext.Activity.FindAsync(activityId);
            if (ActivityToBeUpdated == null)
            {
                throw new Exception("Activity not found.");
            }
            UpdateActivity(ActivityToBeUpdated, _activityViewModel);
            await _appDbContext.SaveChangesAsync();
        }

        public void UpdateActivity(Activity _activity, ActivityViewModel _activityViewModel)
        {
            _activity.Name = _activityViewModel.Name;
            _activity.Description = _activityViewModel.Description;
            _activity.IsCompleted = _activityViewModel.IsCompleted;
        }

      
        public async Task DeleteActivityByIdAsync(long activityId)
        {
            var ActivityToBeUpdated = await _appDbContext.Activity.FindAsync(activityId);
            if (ActivityToBeUpdated is null)
            {
                throw new Exception("Activity not found.");
            }
            ActivityToBeUpdated.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
