using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.DataContexts;
using ToDoActivities.DAL.Interfaces;
using ToDoActivities.DAL.ViewModels;
using ToDoActivities.Models;

namespace ToDoActivities.DAL.RepositoryServices
{
    public class ActivitiesDbService
    {
        private AppDbContext _appDbContext;
        private IGenericRepository<Activity> _activitiesRepository;

        public ActivitiesDbService(AppDbContext appDbContext, IGenericRepository<Activity> activitiesRepository)
        {
            _appDbContext = appDbContext;
            _activitiesRepository = activitiesRepository;
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
            _activitiesRepository.Insert(_activity);


            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Activity>> GetActivitiesAsync()
        {
                                                                                 
            return (List<Activity>)await _activitiesRepository.GetAllAsync();
                                      
                     
        public async Task<ActivityViewModel> GetActivityByIdAsync(long id)
        {
            var activity = await _activitiesRepository.GetByIdAsync(id);
            ActivityViewModel activityViewModel = new ActivityViewModel(activity);
            return activityViewModel;
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
            /* Update the selected activity
             * var ActivityToBeUpdated = await _appDbContext.Activity.FindAsync(activityId);
               if (ActivityToBeUpdated == null)
               {
                   throw new Exception("Activity not found.");
               }
               UpdateActivity(ActivityToBeUpdated, _activityViewModel);
            await _appDbContext.SaveChangesAsync();*/

            /* This is an Update implemantation, part of the repository pattern */
            var ActivityToBeUpdated = await _activitiesRepository.GetByIdAsync(activityId);
            UpdateActivity(ActivityToBeUpdated, _activityViewModel);
            _activitiesRepository.Update(ActivityToBeUpdated); 
        }

        public void UpdateActivity(Activity _activity, ActivityViewModel _activityViewModel)
        {
            _activity.Name = _activityViewModel.Name;
            _activity.Description = _activityViewModel.Description;
            _activity.IsCompleted = _activityViewModel.IsCompleted;
        }
          
        public async Task DeleteActivityByIdAsync(long activityId)
        { //Does not actually deletes the entry,only updates IsDeleted --> true
            var ActivityToBeUpdated = await _appDbContext.Activity.FindAsync(activityId);
            if (ActivityToBeUpdated is null)
            {
                throw new Exception("Activity not found.");
            }
            ActivityToBeUpdated.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();

            /* This is a Delete implemantation, part of the repository pattern
            var ActivityToBeRemoved = await _activitiesRepository.GetByIdAsync(activityId);
            _activitiesRepository.Delete(ActivityToBeRemoved);
            await _appDbContext.SaveChangesAsync();*/

        }
    }
}
