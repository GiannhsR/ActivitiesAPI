using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoActivities.DAL.ViewModels;

namespace ToDoActivities.DAL
{
    public interface IActivitiesRepository : IDisposable
    {
        Task AddActivityAsync();
        Task<List<ActivityViewModel>> GetActivitiesAsync();
        Task<ActivityViewModel> GetActivityByIdAsync(long id);
        Task<ActivityViewModel> GetActivityByNameAsync(string name);
        Task UpdateActivityByIdAsync(long activityId, ActivityViewModel _activityViewModel);
        Task DeleteActivityByIdAsync(long activityId);

    }
}
