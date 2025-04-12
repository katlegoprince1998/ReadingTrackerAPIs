using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Services.DailyNotes;
using ReadingTrackerAPIs.Dtos;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/users/{userId}/books/{bookId}/[controller]")]
    [ApiController]
    public class DailyNotesController : ControllerBase
    {
        private readonly IDailyNoteService _dailyNoteService;

        public DailyNotesController(IDailyNoteService dailyNoteService)
        {
            _dailyNoteService = dailyNoteService ?? throw new ArgumentNullException(nameof(dailyNoteService));
        }

        [HttpPost]
        public async Task<ActionResult<DailyNoteDto>> CreateDailyNote(
            Guid userId,
            Guid bookId,
            [FromBody] CreateDailyNoteDto request)
        {
            if (request == null)
                return BadRequest("Daily note data must be provided.");

            var createdNote = await _dailyNoteService.CreateDailyNoteAsync(request, userId, bookId);
            return CreatedAtAction(nameof(GetDailyNoteById), new { userId, bookId, dailyNoteId = createdNote.Id }, createdNote);
        }

        [HttpPut("{dailyNoteId}")]
        public async Task<ActionResult<DailyNoteDto>> UpdateDailyNote(
            Guid userId,
            Guid bookId,
            Guid dailyNoteId,
            [FromBody] UpdateDailyNoteDto request)
        {
            if (request == null)
                return BadRequest("Update data must be provided.");

            var updatedNote = await _dailyNoteService.UpdateDailyNoteAsync(request, userId, bookId, dailyNoteId);
            return Ok(updatedNote);
        }

        [HttpDelete("{dailyNoteId}")]
        public async Task<ActionResult<DailyNoteDto>> DeleteDailyNote(
            Guid userId,
            Guid bookId,
            Guid dailyNoteId)
        {
            var deletedNote = await _dailyNoteService.DeleteDailyNote(userId, bookId, dailyNoteId);
            return Ok(deletedNote);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyNoteDto>>> GetAllDailyNotes(
            Guid userId,
            Guid bookId)
        {
            var notes = await _dailyNoteService.GetAllDailyNotes(userId, bookId);
            return Ok(notes);
        }

        [HttpGet("{dailyNoteId}")]
        public async Task<ActionResult<DailyNoteDto>> GetDailyNoteById(
            Guid userId,
            Guid bookId,
            Guid dailyNoteId)
        {
            var note = await _dailyNoteService.GetDailyNoteById(userId, dailyNoteId, bookId);
            return Ok(note);
        }
    }
}

