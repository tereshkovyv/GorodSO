using GorodSO.Database;
using GorodSO.Models;

namespace GorodSO.Services;

public class UsersService
{
    public bool Create(string vkId, AppUserRole role, int? categoryId = null, string? commandName = null)
    {
        using GorodSODbContext db = new GorodSODbContext();
        var user = new AppUser()
        {
            VkId = int.Parse(vkId),
            Role = role,
            CategoryId = categoryId,
            CommandName = commandName
        };
        db.Users.Add(user);
        db.SaveChanges();

        return true;
    }

    public bool TryGetUser(int vkId, out AppUser? user)
    {
        using var db = new GorodSODbContext();
        user = db.Users.Find(vkId);
        return true;
    }

    public IEnumerable<AppUser> GetAllUsers()
    {
        using var db = new GorodSODbContext();
        return db.Users;
    }

    public void RemoveUser(int vkId)
    {
        using var db = new GorodSODbContext();
        var user = db.Users.Find(vkId);
        db.Users.Remove(user);
    }
}