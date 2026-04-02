using GorodSO.Database;
using GorodSO.Models;

namespace GorodSO.Services;

public class TasksService(CompetitionsService competitionsService)
{
    public IEnumerable<QuestTask> GetByCategoryId(int categoryId)
    {
        var competitionId = competitionsService.GetActive().Id;
        using GorodSODbContext db = new GorodSODbContext();
        return db.Tasks.Where(t => t.CategoryId == categoryId && t.CompetitionId == competitionId);
    }

    public void Create(int categoryId, string name, string text, string correctAnswer, int score, QuestTaskType type)
    {
        var competitionId = competitionsService.GetActive().Id;
        using GorodSODbContext db = new GorodSODbContext();
        var task = new QuestTask()
        {
            CategoryId = categoryId,
            CompetitionId = competitionId,
            
        }
    }
}