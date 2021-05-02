using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoActivities.DAL.RepositoryServices;
using ToDoActivities.DAL.ViewModels;

namespace ToDoActivities.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private ActivitiesDbService _activitiesDbService;
        public ActivitiesController(ActivitiesDbService activitiesDbService)
        {
            _activitiesDbService = activitiesDbService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ActivityViewModel>> CreateActivityAsync(ActivityViewModel _activityViewModel)
        {
            await _activitiesDbService.AddActivityAsync(_activityViewModel);

            return CreatedAtRoute("GetToDo", new { name = _activityViewModel.Name }, _activityViewModel);
        }

        [HttpGet("AllActivities")]
        public async Task<List<ActivityViewModel>> GetAllActivitiesAsync()
        { 
            return await _activitiesDbService.GetActivitiesAsync();
        }

        [HttpGet("Activity/{id?}")]
        public async Task<ActionResult<ActivityViewModel>> GetActivityByIdAsync(int id)
        {
            var activity = await _activitiesDbService.GetActivityByIdAsync(id);

            if ( activity == null)
            {
                return NoContent();
            }

            return activity;
        }

        [HttpPut("Update/{activityId:int}")]
        public async Task<ActionResult> UpdateActivityByIdAsync(long activityId, ActivityViewModel activityViewModel)
        {
            if (activityId != activityViewModel.ActivityId)
            {
                return BadRequest();
            }

            await _activitiesDbService.UpdateActivityByIdAsync(activityId,activityViewModel);
            return NoContent();
        }

        [HttpDelete("Delete/{activityId:int}")]
        public async Task<ActionResult> DeleteById(long activityId)
        {
            await _activitiesDbService.DeleteActivityByIdAsync(activityId);
            return NoContent();
        }

        [HttpGet("{name}", Name = "GetTodo")]
        public async Task<ActionResult<ActivityViewModel>> GetById(ActivityViewModel ActivityViewModel)
        {
            var activity = await _activitiesDbService.GetActivityByNameAsync(ActivityViewModel.Name);

            if (activity == null)
            {
                return NoContent();
            }

            return activity;
        }
    }
}
