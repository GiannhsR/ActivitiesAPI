using System;
using ToDoActivities.Models;

namespace ToDoActivities.DAL.ViewModels
{
    public class ActivityViewModel
    {
        public long ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
        
        public ActivityViewModel() { }
        public ActivityViewModel(Activity activity)
        {
            this.ActivityId = activity.ActivityId;
            this.Name = activity.Name;
            this.Description = activity.Description;
            this.Date = activity.Date;
            this.IsCompleted = activity.IsCompleted;
            this.IsDeleted = activity.IsDeleted;
        }
    }
}
