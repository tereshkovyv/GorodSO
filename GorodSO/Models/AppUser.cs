namespace GorodSO.Models;

public class AppUser
{
    public int Id { get; set; }
    public int VkId { get; set; }
    public int CompetitionId { get; set; }
    public AppUserRole? Role { get; set; }
    public int? CategoryId { get; set; }
    public string? CommandName { get; set; }
}