using GorodSO.Models;

namespace GorodSO.Services.MessagesServices;

public class CoordinatorMessagesService : MessagesServiceBase
{
    public string Handle(AppUser user, string command, string value) =>
        command switch
        {
            "/help" => HandleHelp(),
            "/rate" => HandleRate(user, value),
            "/rates" => HandleRates(user),
            _ => HandleUnknown()
        };

    private string HandleRate(AppUser user, string value) =>
        "Выставление оценки. Не реализовано";
    
    private string HandleRates(AppUser user) =>
        "Выставленные оценки. Не реализовано";

    protected override string HandleHelp() => 
        "Помощь";
}