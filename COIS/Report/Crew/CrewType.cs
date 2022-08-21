using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


public class CrewType:INoDatabase
{

    public string Guid { get; set; }
    public List<CrewItem> Items { get; set; }


    public CrewType()
    {
        Guid = System.Guid.NewGuid().ToString();
        Items = new List<CrewItem>();
    }

    public List<EmployeeType> Employees()
    {
        return (from c in Items.ToList()
                select c.Employee).ToList();
    }
    public void SortItems()
    {
        Items = Items.OrderBy(x => x.StartTime).ThenBy(x => x.EndTime).ThenBy(x => x.Employee.LastName).ToList();
    }

    public void Import(ZoneType zone, DateTime startTime, DateTime endTime)
    {
        AppDBContext db = new AppDBContext();

        Items = new List<CrewItem>();

       

        List<CrewModel> cm = db.CrewModels.AsEnumerable<CrewModel>().Where(x =>
        x.IsWorking == true &&
        x.Zone.Id == zone.Id &&
        x.IsWork(startTime, endTime) == true
        ).ToList();

        cm.ForEach(x =>
        {
            Items.Add(new CrewItem(x.StartTime,x.EndTime,x.Employee));
        });

        SortItems();
    }

    public DataTable toTable()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("Pracownik");
        dt.Columns.Add("Praca od");
        dt.Columns.Add("Praca do");
        dt.Columns.Add("Obecny");
        dt.Columns.Add("Czynności");


        foreach(var item in Items)
        {
            dt.Rows.Add(item.Employee.FullName(), item.StartTime, item.EndTime, (item.Present) ? "TAK" : "NIE", string.Join(", ", item.Activities.Select(x => x.Name)));
        }

        return dt;
    }
 
}

