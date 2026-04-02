using GorodSO.Models;

namespace GorodSO.Services.MessagesServices;

public class ParticipantMessagesService(CompetitionsService competitionsService) : MessagesServiceBase
{
    public string Handle(AppUser user, string command, string value) =>
        command switch
        {
            "/help" => HandleHelp(),
            "/answer" => HandleAnswer(user, value),
            "/my_answers" => HandleMyAnswers(user, value),
            "/my_score" => HandleMyScore(user, value),
            "/tasks" => HandleTasks(user, value),
            _ => HandleUnknown()
        };

    private string HandleAnswer(AppUser user, string value)
    {
        var competitionId = competitionsService.GetActive();
        if (competitionId is null) return "В данный момент нельзя давать ответы";

        var parts = value.Split(' ');
        if (parts.Length != 2) return "Неправильный формат ответа"
    }
    
    private string HandleMyAnswers(AppUser user, string value)
        => "Список всех ответов. Не реализовано";
    
    private string HandleMyScore(AppUser user, string value)
        => "Мой счет. Только после завершения. Не реализовано";
    
    private string HandleTasks(AppUser user, string value)
        => "Все задания соревнования. Не реализовано";

    protected override string HandleHelp() => 
        "/answer, /my_answers, /my_score, /tasks";
}