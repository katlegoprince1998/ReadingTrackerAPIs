
using Microsoft.AspNetCore.Mvc;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Services.ReadProgress;

namespace ReadingTrackerAPIs.Controllers
{
    [Route("api/users/{userId}/books/{bookId}/[controller]")]
    [ApiController]
    public class ReadingProgressController : ControllerBase
    {
        private readonly IReadingProgressService _readingProgressService;

        public ReadingProgressController(IReadingProgressService readingProgressService)
        {
            _readingProgressService = readingProgressService ?? throw new ArgumentNullException(nameof(readingProgressService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid userId, Guid bookId)
        {
            var progress = await _readingProgressService.GetReadingProgress(userId, bookId);
            return Ok(progress);
        }

        [HttpGet("{readingProgressId}")]
        public async Task<IActionResult> GetById(Guid userId, Guid bookId, Guid readingProgressId)
        {
            var progress = await _readingProgressService.GetReadingProgressById(userId, bookId, readingProgressId);
            if (progress == null) return NotFound();
            return Ok(progress);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid userId, Guid bookId, [FromBody] CreateReadingProgressDto dto)
        {
            if (dto == null) return BadRequest("Reading progress data is required.");
            var created = await _readingProgressService.CreateReadingProgress(dto, userId, bookId);
            return CreatedAtAction(nameof(GetById), new { userId, bookId, readingProgressId = created.Id }, created);
        }

        [HttpPut("{readingProgressId}")]
        public async Task<IActionResult> Update(Guid userId, Guid bookId, Guid readingProgressId, [FromBody] UpdateReadingProgressDto dto)
        {
            if (dto == null) return BadRequest("Reading progress data is required.");
            var updated = await _readingProgressService.UpdateReadingProgress(dto, userId, bookId, readingProgressId);
            return Ok(updated);
        }

        [HttpDelete("{readingProgressId}")]
        public async Task<IActionResult> Delete(Guid userId, Guid bookId, Guid readingProgressId)
        {
            var deleted = await _readingProgressService.DeleteReadingProgress(userId, bookId, readingProgressId);
            return Ok(deleted);
        }
    }
}

