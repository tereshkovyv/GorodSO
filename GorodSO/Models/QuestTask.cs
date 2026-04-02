namespace GorodSO.Models;

public class QuestTask
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string CorrectAnswer { get; set; }
    public int Score { get; set; }
    public QuestTaskType Type { get; set; }
    public int CategoryId { get; set; }
    public int CompetitionId { get; set; }
}