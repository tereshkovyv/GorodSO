namespace GorodSO.Models;

public class Answer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public string Value { get; set; }
    public DateTime Time { get; set; }
}