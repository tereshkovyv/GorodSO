using GorodSO.Database;
using GorodSO.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GorodSO.Services;

public class CategoriesService
{
    public void Create(string name)
    {
        using var db = new GorodSODbContext();
        var category = new Category() { Name = name };
        db.Categories.Add(category);
        db.SaveChanges();
    }

    public Category? GetById(int id)
    {
        using var db = new GorodSODbContext();
        return db.Categories.Find(id);
    }

    public IEnumerable<Category> GetAll()
    {
        using var db = new GorodSODbContext();
        return db.Categories;
    }

    public void Delete(int id)
    {
        using var db = new GorodSODbContext();
        var category = db.Categories.Find(id);
        db.Categories.Remove(category);
        db.SaveChanges();
    }
}