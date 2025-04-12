using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReadingTrackerAPIs.Data;
using ReadingTrackerAPIs.Dtos;
using ReadingTrackerAPIs.Dtos.Request;
using ReadingTrackerAPIs.Models.Entity;

namespace ReadingTrackerAPIs.Services.YearlyGoals
{
    public class YearlyGoalService : IYearlyGoalService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public YearlyGoalService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<YearlyGoalDto> createYearlyGoal(CreateYearlyGoalDto request, Guid userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var yearlyGoal = _mapper.Map<YearlyGoal>(request);
            yearlyGoal.Id = Guid.NewGuid();
            yearlyGoal.UserId = userId;
            yearlyGoal.CreatedAt = DateTime.UtcNow;
    

            _context.YearlyGoals.Add(yearlyGoal);
            await _context.SaveChangesAsync();

            return _mapper.Map<YearlyGoalDto>(yearlyGoal);
        }

        public async Task<YearlyGoalDto> updateYearlyGoal(UpdateYearlyGoalDto request, Guid userId, Guid id)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var existingGoal = await _context.YearlyGoals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId);
            if (existingGoal == null)
                throw new KeyNotFoundException($"Yearly goal with ID '{id}' not found for user '{userId}'.");

            _mapper.Map(request, existingGoal);
            existingGoal.UpdatedAt = DateTime.UtcNow;
            existingGoal.Year = request.Year;
            existingGoal.BooksRead = request.BooksRead;
            existingGoal.GoalCount = request.GoalCount;

            _context.YearlyGoals.Update(existingGoal);
            await _context.SaveChangesAsync();

            return _mapper.Map<YearlyGoalDto>(existingGoal);
        }

        public async Task<YearlyGoalDto> deleteYearlyGoal(Guid id)
        {
            var goal = await _context.YearlyGoals.FindAsync(id);
            if (goal == null)
                throw new KeyNotFoundException($"Yearly goal with ID '{id}' not found.");

            _context.YearlyGoals.Remove(goal);
            await _context.SaveChangesAsync();

            return _mapper.Map<YearlyGoalDto>(goal);
        }

        public async Task<IEnumerable<YearlyGoalDto>> getAllYearlyGoal(Guid userId)
        {
            var goals = await _context.YearlyGoals
                .Where(g => g.UserId == userId)
                .OrderByDescending(g => g.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<YearlyGoalDto>>(goals);
        }

        public async Task<YearlyGoalDto> GetYearlyGoalById(Guid id)
        {
            var goal = await _context.YearlyGoals.FindAsync(id);
            if (goal == null)
                throw new KeyNotFoundException($"Yearly goal with ID '{id}' not found.");

            return _mapper.Map<YearlyGoalDto>(goal);
        }
    }
}
