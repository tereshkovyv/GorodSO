namespace GorodSO.Services.MessagesServices;

public abstract class MessagesServiceBase
{
    protected virtual string HandleHelp() => 
        "Помощь.";
    
    protected string HandleUnknown()
        => "Неизвестная команда. Нажмите 'Помощь', чтобы посмотреть, какие команды можно использовать.";
}