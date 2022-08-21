using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class InfoCategory:IDatabase
{
    public int Id { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Opis", FrontendType.text, true)]
    public string? Description { get; set; }

    public static InfoCategory Empty()
    {
        return new InfoCategory() { Id = 0, Name = "Brak kategorii", Description = "" };
    }
    public void Delete()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.RemovetObject(this);
    }

    public void Save()
    {
        AppDataAccessLayer ap = new AppDataAccessLayer();
        ap.UpdateObject(this);
    }

    public static InfoCategory GetCategory(string name)
    {
        AppDBContext ad = new AppDBContext();
        return ad.InfoCategories.Where(x => x.Name.ToLower().Contains(name.ToLower())).FirstOrDefault();
    }
    public static InfoCategory GetCategory(int id)
    {
        AppDBContext ad = new AppDBContext();
        return ad.InfoCategories.Where(x => x.Id==id).FirstOrDefault();
    }

    public static List<InfoCategory> GetCategories()
    {
        AppDBContext ad = new AppDBContext();
        return ad.InfoCategories.ToList();
    }
}

