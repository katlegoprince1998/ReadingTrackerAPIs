using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Services.YearlyGoals;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class YearlyGoalController : ControllerBase
    {
        private readonly IYearlyGoalService _yearlyGoalService;

        public YearlyGoalController(IYearlyGoalService yearlyGoalService)
        {
            _yearlyGoalService = yearlyGoalService ?? throw new ArgumentNullException(nameof(yearlyGoalService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid userId, [FromBody] CreateYearlyGoalDto dto)
        {
            if (dto == null)
                return BadRequest("Yearly goal data is required.");

            var created = await _yearlyGoalService.createYearlyGoal(dto, userId);
            return CreatedAtAction(nameof(GetById), new { userId = userId, id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid userId, Guid id, [FromBody] UpdateYearlyGoalDto dto)
        {
            if (dto == null)
                return BadRequest("Yearly goal update data is required.");

            var updated = await _yearlyGoalService.updateYearlyGoal(dto, userId, id);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid userId, Guid id)
        {
            var deleted = await _yearlyGoalService.deleteYearlyGoal(id);
            return Ok(deleted);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            var goals = await _yearlyGoalService.getAllYearlyGoal(userId);
            return Ok(goals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid userId, Guid id)
        {
            var goal = await _yearlyGoalService.GetYearlyGoalById(id);
            if (goal == null)
                return NotFound();

            return Ok(goal);
        }
    }
}