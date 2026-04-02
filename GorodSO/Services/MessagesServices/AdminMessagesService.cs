using GorodSO.Models;

namespace GorodSO.Services.MessagesServices;

public class AdminMessagesService(
    UsersService usersService,
    CategoriesService categoriesService,
    CompetitionsService competitionsService)
    : MessagesServiceBase
{
    public string Handle(AppUser user, string command, string value) =>
        command switch
        {
            "/help" => HandleHelp(),
            "/competitions" => HandleCompetitions(),
            "/add_competition" => HandleAddCompetition(user, value),
            "/change_competition_state" => HandleChangeCompetitionState(user, value),
            "/add_category" => HandleAddCategory(user, value),
            "/remove_category" => HandleRemoveCategory(user, value),
            "/add_user" => HandleAddUser(user, value),
            "/edit_user" => HandleEditUser(user, value),
            "/get_users" => HandleGetUsers(user, value),
            "/remove_user" => HandleRemoveUser(user, value),
            _ => HandleUnknown()
        };

    private string HandleCompetitions()
    {
        return string.Join('\n', competitionsService.GetAll().Select(c => $"Cometition: {c.Name} is {c.State}"));
    }
    
    private string HandleAddCompetition(AppUser user, string value)
    {
        competitionsService.Create(value);
        return "Competition created successfully";
    }

    private string HandleChangeCompetitionState(AppUser user, string value)
    {
        var items = value.Split(' ');
        competitionsService.ChangeState(int.Parse(items[0]), Enum.Parse<CompetitionState>(items[1]));
        return "State changed successfully";
    }

    private string HandleAddCategory(AppUser user, string value)
    {
        categoriesService.Create(value);
        return "Категория добавлена";
    }

    private string HandleRemoveCategory(AppUser user, string value)
    {
        if (!int.TryParse(value, out var id))
            return "Неправильный id";
        categoriesService.Delete(id);

        return "Категория удалена";
    }
    private string HandleAddUser(AppUser user, string value)
    {
        var parts = value.Split(' ');
        var userId = parts[0];

        if (parts.Length == 1)
            return "Пропущена роль";
        if (!Enum.TryParse(parts[1], out AppUserRole userRole))
            return "Неизвестная роль";
        if (userRole == AppUserRole.Coordinator)
            return usersService.Create(userId, AppUserRole.Coordinator)
                ? "Пользователь добавлен"
                : "Ошибка при добавлении пользователя";
        if (parts.Length == 2)
            return "Пропущена категория";
            
        if(!int.TryParse(parts[2], out var categoryId))
            return "Категория имеет неверный формат";
        return usersService.Create(userId, userRole, categoryId)
            ? "Пользователь добавлен"
            : "Ошибка при добавлении пользователя";

    }
    
    private string HandleEditUser(AppUser user, string value) =>
        "Изменение пользователя. Не реализовано";

    private string HandleGetUsers(AppUser user, string value) =>
        string.Join("\n",
            usersService.GetAllUsers().Select(u => $"VKID: {u.VkId} Role: {u.Role} Category: {u.CategoryId}").ToArray());

    private string HandleRemoveUser(AppUser user, string value)
    {
        if (!int.TryParse(value, out var userId))
            return "Неправильный id";
        usersService.RemoveUser(userId);
        return "Пользователь удален(наверное...)";
    }
    
    private string HandleHelp() => 
        "Помощь \n" +
        "/add_user [id] [moderator|coordinator|participant] [?category]";
}