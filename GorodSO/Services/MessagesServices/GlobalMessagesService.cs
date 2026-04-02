using GorodSO.Models;

namespace GorodSO.Services.MessagesServices;

public class GlobalMessagesService(
    UsersService usersService,
    ParticipantMessagesService participantMessagesService,
    CoordinatorMessagesService coordinatorMessagesService,
    ModeratorMessagesService moderatorMessagesService,
    AdminMessagesService adminMessagesService)
{
    public string Handle(int requesterId, string message)
    {
        if (!usersService.TryGetUser(requesterId, out var user))
            return "Error";
        var (command, value) = GetMessageParts(message);
        switch (user.Role)
        {
            case AppUserRole.Admin:
                return adminMessagesService.Handle(user, command, value);
            case AppUserRole.Moderator:
                return moderatorMessagesService.Handle(user, command, value);
            case AppUserRole.Coordinator:
                return coordinatorMessagesService.Handle(user, command, value);
            case AppUserRole.Participant:
                return participantMessagesService.Handle(user, command, value);
            case null:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        return "Got answer";
    }
    
    private (string, string) GetMessageParts(string message)
    {
        string[] parts = message.Split(new char[] { ' ' }, 2);

        string firstPart = parts[0];     // "Hello"
        string secondPart = parts.Length != 1 ? parts[1] : "";    // "world this is a test"

        return (firstPart, secondPart);
    }
}