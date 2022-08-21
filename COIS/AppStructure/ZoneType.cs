

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

public class ZoneType:IDatabase
{
    public int Id { get; set; }
    [FrontEnd("Nazwa", FrontendType.text, true)]
    public string Name { get; set; }
    [FrontEnd("Kod", FrontendType.text, true)]
    public string Code { get; set; }
    /// <summary>
    /// Nazwa strefy z interfejsu do zasilania obsady, wg tego pola wskazywana jest strefa dla importowanej pozycji
    /// </summary>
    [FrontEnd("Nazwa alternatywna", FrontendType.text, true)]
    public string? AlternativeName { get; set; }

    [FrontEnd("Jednostka organizacyjna", FrontendType.list, false, true, ObjectListNames.orgunits, "id", "name")]
    public int OrganizationUnitID {get;set;}

    public static ZoneType Empty()
    {
        ZoneType zone = new ZoneType()
        {
            AlternativeName = "",
            Code = "",
            Name = "",
            OrganizationUnitID = 0
        };
        return zone;
    }
    public OrgUnit OrganizationUnit()
    {
        return OrgUnit.GetOrgUnitById(OrganizationUnitID);
    }
    public static ZoneType GetZoneByAlternativeName(string altername)
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.Where(x => x.AlternativeName.ToLower() == altername.ToLower()).FirstOrDefault();
    }
    public static ZoneType GetZoneById(int id)
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.Where(x => x.Id == id).FirstOrDefault();
    }

    public static List<ZoneType> GetZoneByOrgUnitId(int orgUnitId)
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.ToList().Where(x => x.OrganizationUnitID == orgUnitId).ToList();
    }
    public static List<ZoneType> GetZones()
    {
        AppDBContext db = new AppDBContext();
        return db.Zones.Where(x => x.Id!=0).ToList();
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