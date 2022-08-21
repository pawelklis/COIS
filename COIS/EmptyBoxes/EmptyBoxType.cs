using System;
using System.Collections.Generic;
using System.Text;


public class EmptyBoxType : IDatabase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }


    public static List<EmptyBoxType> GetEmptyBoxTypes()
    {
        AppDBContext db = new AppDBContext();
        return db.EmptyBoxes.ToList();
    }  
    public static EmptyBoxType GetEmptyBoxById(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.EmptyBoxes.ToList().Find(x=>x.Id==id);
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
}

