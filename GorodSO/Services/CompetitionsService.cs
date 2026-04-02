using GorodSO.Database;
using GorodSO.Exceptions;
using GorodSO.Models;

namespace GorodSO.Services;

public class CompetitionsService
{
    public IEnumerable<Competition> GetAll()
    {
        using GorodSODbContext db = new GorodSODbContext();
        return db.Competitions;
    }

    public Competition? GetActive()
    {
        using GorodSODbContext db = new GorodSODbContext();
        //TODO
        return db.Competitions.FirstOrDefault(c => c.State != CompetitionState.NonActive);
    }

    public void Create(string name)
    {
        using GorodSODbContext db = new GorodSODbContext();
        var competition = new Competition() { Name = name, State = CompetitionState.NonActive };
        db.Competitions.Add(competition);
        db.SaveChanges();
    }

    public void ChangeState(int id, CompetitionState newState)
    {
        using GorodSODbContext db = new GorodSODbContext();
        if (newState != CompetitionState.NonActive && db.Competitions.Any(c => c.State != CompetitionState.NonActive))
            throw new GorodSoException("Cannot activate competition. At least on active competition already exists.");
        var competition = db.Competitions.Find(id);
        competition.State = newState;
        db.SaveChanges();

    }
}