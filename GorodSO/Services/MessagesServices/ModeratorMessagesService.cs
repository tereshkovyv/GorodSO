using GorodSO.Models;

namespace GorodSO.Services.MessagesServices;

public class ModeratorMessagesService : MessagesServiceBase
{
    public string Handle(AppUser user, string command, string value) =>
        command switch
        {
            "/help" => HandleHelp(),
            "/add_task" => HandleAddTask(user, value),
            "/edit_task" => HandleEditTask(user, value),
            "/tasks" => HandleTasks(user, value),
            "/delete_task" => HandleDeleteTask(user, value),
            "/rate" => HandleRate(user, value),
            "/score" => HandleScore(user, value),
            _ => HandleUnknown()
        };

    private string HandleAddTask(AppUser user, string value) =>
        "Добавление задания. Не реализовано";
    
    private string HandleEditTask(AppUser user, string value) =>
        "Изменение задания. Не реализовано";
    
    private string HandleTasks(AppUser user, string value) =>
        "Все задания. Не реализовано";
    
    private string HandleDeleteTask(AppUser user, string value) =>
        "Удаление задания. Не реализовано";
    
    private string HandleRate(AppUser user, string value) =>
        "Выставление баллов. Не реализовано";
    
    private string HandleScore(AppUser user, string value) =>
        "Результаты по командам. Не реализовано";

    protected override string HandleHelp() => 
        "Помощь";
}