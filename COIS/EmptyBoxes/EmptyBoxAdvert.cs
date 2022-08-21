using System;
using System.Collections.Generic;
using System.Text;


public class EmptyBoxAdvert:IDatabase
{
    public static event EventHandler<EmptyBoxAdvert> OnNewAdvert;
    public static event EventHandler<EmptyBoxOffert> OnNewOffert;
    public static event EventHandler<EmptyBoxOffert> OnOffertAccept;
    public static event EventHandler<EmptyBoxOffert> OnOffertCancel;
    public static event EventHandler NeedRefresh;
    public int Id { get; set; }
    public int OrgUnitId { get; set; }
    public int AdvertType { get; set; }
    public EmptyBoxType EmptyBox { get; set; }
    public int Amount { get; set; }
    public bool Closed { get; set; }
    public List<EmptyBoxOffert> Offerts { get; set; }
    public string? Comment { get; set; }
    public UserType User { get; set; }

    public EmptyBoxAdvert()
    {
        EmptyBoxOffert.OnAccept += EmptyBoxOffert_OnAccept;
        EmptyBoxOffert.OnCancel += EmptyBoxOffert_OnCancel;
    }
    public int EnableAmmount()
    {
        int am = Amount;

        foreach(var o in Offerts)
        {
            if (o.Status == 2 || o.Status == 4)
                am -= o.AcceptedAmount;
        }

        return am;
    }

    public void Close(UserType user)
    {
        foreach (var o in Offerts) 
        {
            if (o.Status == 1)
            {
                o.Cancel(user, "Odrzucono z powodu anulowania ogłoszenia");
            }
        }

        this.Closed = true;
        this.Save();

        PushRefresh();
    }
    private void EmptyBoxOffert_OnCancel(object? sender, EmptyBoxOffert e)
    {
        if (Offerts.Find(x => x.GuidId == e.GuidId)!=null)
        {
            Save();


            EventHandler<EmptyBoxOffert> handler = OnOffertCancel;
            handler?.Invoke(this, e);

            PushRefresh();
        }
    }

    private void EmptyBoxOffert_OnAccept(object? sender, EmptyBoxOffert e)
    {
        if (Offerts.Find(x => x.GuidId == e.GuidId) != null)
        {
            if (EnableAmmount() < 1)
                this.Closed = true;
            Save();


            EventHandler<EmptyBoxOffert> handler = OnOffertAccept;
            handler?.Invoke(this, e);
            if (AdvertType == 1)
            {
                EmptyBoxOrderType.NewOrder(Orgunit(),OrgUnit.GetOrgUnitById(Orgunit().EmptyBoxParentOrgUnitId), EmptyBox, e.AcceptedAmount, e.Comment);
            }
            else
            {
                EmptyBoxOrderType.NewOrder(e.Orgunit(), OrgUnit.GetOrgUnitById(e.Orgunit().EmptyBoxParentOrgUnitId), EmptyBox, e.AcceptedAmount, e.Comment);
            }

            PushRefresh();
        }
    }

    public string AvertTypeString()
    {
        if (AdvertType == 1)
            return "Oddam";
        return "Potrzebuje";
    }

    public static string ChecAdvertBeforeAdd()
    {
        return "";
    }
    public OrgUnit Orgunit()
    {
        return OrgUnit.GetOrgUnitById(OrgUnitId);
    }

    static void PushRefresh()
    {
        EventHandler handler = NeedRefresh;
        handler?.Invoke(null, null);
    }
    public static void NewAdvert(int orgunitid,int adverttype,EmptyBoxType emptybox, int amount,string comment,UserType user)
    {
        if (comment == null)
            comment = "";
        EmptyBoxAdvert o = new EmptyBoxAdvert()
        {
            OrgUnitId = orgunitid,
            AdvertType = adverttype,
            EmptyBox = emptybox,
            Amount = amount,
            Comment = comment,
            Offerts = new List<EmptyBoxOffert>(),
            User = user
        };

        o.Save();

        EventHandler<EmptyBoxAdvert> handler = OnNewAdvert;
        handler?.Invoke(o, o);

        PushRefresh();
    }
    public void AddOffert(int orgunitid,int amount,string comment,UserType user)
    {
        EmptyBoxOffert o = new EmptyBoxOffert()
        {
            AddTime = DateTime.Now,
            User = user,
            Amount = amount,
            Comment = comment,
            OrgUnitId=orgunitid,
            Status=1
        };
        Offerts.Add(o);
        Save();

        EventHandler<EmptyBoxOffert> handler = OnNewOffert;
        handler?.Invoke(this, o);

        PushRefresh();
    }
    public static List<EmptyBoxAdvert> GetMyActiveAdverts(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxAdverts.ToList().Where(x => x.Closed==false && x.OrgUnitId == orgunitid).ToList();
    }
    public static List<EmptyBoxAdvert> GetOtherActiveAdverts(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxAdverts.ToList().Where(x => x.Closed== false && x.OrgUnitId != orgunitid).ToList();
    }
    public static List<EmptyBoxAdvert> GetClosedAdverts(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxAdverts.ToList().Where(x => x.Closed==true && x.OrgUnitId == orgunitid).ToList();
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






