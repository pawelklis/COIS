using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Stosować tylko metody statyczne dla nowego zdarzenia
/// </summary>
public class EmptyBoxOrderType:IDatabase,IEmptyBoxAmountChange
{
    public static event EventHandler<EmptyBoxOrderType> OnNewOrder;

    public int Id { get; set; }
    public OrgUnit OrderUnit { get; set; }
    public OrgUnit ParentOrgUnit { get; set; }
    public EmptyBoxType EmptyBox { get; set; }
    public int OrderAmount { get; set; }
    public int CompletionAmount { get; set; }
    public List<EmptyBoxOrderHistory> History { get; set; }
    public bool Closed { get; set; }

    public static List<EmptyBoxOrderType> GetOrdersToProgress(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxOrders.ToList().Where(x => x.GetStatus()== EmptyBoxOrderHistory.EmptyBoxOrderStatus.New && x.ParentOrgUnit.Id == orgunitid).OrderBy(x=>x.History.First().Date).ToList();
    }
    public static List<EmptyBoxOrderType> GetOrdersInProgress(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxOrders.ToList().Where(x => x.GetStatus() == EmptyBoxOrderHistory.EmptyBoxOrderStatus.InProgress && x.ParentOrgUnit.Id == orgunitid).ToList();
    }
    public static List<EmptyBoxOrderType> GetOrdersCompleted(int orgunitid)
    {
        AppDBContext db = new AppDBContext();
        return db.BoxOrders.ToList().Where(x => x.Closed==true && x.ParentOrgUnit.Id == orgunitid).ToList();
    }
    public EmptyBoxOrderHistory.EmptyBoxOrderStatus GetStatus()
    {
        return History.Last().Status;
    }

 /// <summary>
 /// Tworzy nowe zamówienie
 /// </summary>
 /// <param name="orderUnit">jednostka zamawiająca</param>
 /// <param name="parentOrgUnit">jednostka realizująca zamówienie</param>
 /// <param name="emptyBox">rodzaj OZ</param>
 /// <param name="orderAmount">ilość zamówiona</param>
 /// <param name="comment">komentarz</param>
 /// <param name="zone">Opcjonalnie jeśli możliwe do ustalenia</param>
 /// <param name="user">Opcjonalnie jeśli możliwe do ustalenia</param>
    public static void NewOrder( OrgUnit orderUnit, OrgUnit parentOrgUnit, EmptyBoxType emptyBox, int orderAmount, string comment,ZoneType zone=null,UserType user=null)
    {
        EmptyBoxOrderType o = new EmptyBoxOrderType();

        o.History = new List<EmptyBoxOrderHistory>();
        o.OrderUnit = orderUnit;
        o.ParentOrgUnit = parentOrgUnit;
        o.EmptyBox = emptyBox;
        o.OrderAmount = orderAmount;
        o.CompletionAmount = 0;
        o.History.Add(new EmptyBoxOrderHistory()
        {
            Date = DateTime.Now,
            Status = EmptyBoxOrderHistory.EmptyBoxOrderStatus.New,
            User = user,
            Comment = comment,
            Zone=zone
        });

        o.Save();

        EventHandler<EmptyBoxOrderType> handler = OnNewOrder;
        handler?.Invoke(o, o);

    }

    /// <summary>
    /// Przyjęcie do realizacji
    /// </summary>
    /// <param name="comment">komentarz</param>
    /// <param name="user">użytkownik</param>
    /// <param name="zone">strefa</param>
    public void GetToProgress(string comment,UserType user,ZoneType zone)
    {
        this.History.Add(new EmptyBoxOrderHistory()
        {
            Comment = comment,
            Date = DateTime.Now,
            Status = EmptyBoxOrderHistory.EmptyBoxOrderStatus.InProgress,
            User = user,
            Zone = zone
        });
        this.Save();
    }
    /// <summary>
    /// Kończy zamówienie
    /// </summary>
    /// <param name="comment">wymagany jeśli zreazliowano częściowo</param>
    /// <param name="user">użytkownik</param>
    /// <param name="zone">strefa</param>
    /// <param name="completedamount">liczba zrealizowana</param>
    public void SetCompleted(string comment, UserType user, ZoneType zone, int completedamount)
    {
        if (completedamount == OrderAmount)
        {
            CompletionAmount = completedamount;
            this.History.Add(new EmptyBoxOrderHistory()
            {
                Comment = comment,
                Date = DateTime.Now,
                Status = EmptyBoxOrderHistory.EmptyBoxOrderStatus.Completed,
                User = user,
                Zone = zone
            });
        }
        else
        {
            CompletionAmount = completedamount;
            this.History.Add(new EmptyBoxOrderHistory()
            {
                Comment = comment,
                Date = DateTime.Now,
                Status = EmptyBoxOrderHistory.EmptyBoxOrderStatus.PartCompleted,
                User = user,
                Zone = zone
            });
        }

        Closed = true;

        this.Save();
    }

    /// <summary>
    /// Anulowano
    /// </summary>
    /// <param name="comment">wymagane dlaczego anulowano</param>
    /// <param name="user">użytkownik</param>
    /// <param name="zone">strefa</param>
    public void Cancel(string comment, UserType user, ZoneType zone)
    {
        this.History.Add(new EmptyBoxOrderHistory()
        {
            Comment = comment,
            Date = DateTime.Now,
            Status = EmptyBoxOrderHistory.EmptyBoxOrderStatus.Canceled,
            User = user,
            Zone = zone
        });

        CompletionAmount = 0;
        Closed = true;
        this.Save();
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

    /// <summary>
    /// Dodaje zrealizowaną ilość do magazynu
    /// </summary>
    public void PlusAmount()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Odejmuje zrealizowaną ilośc z magazynu
    /// </summary>
    public void MinusAmount()
    {
        throw new NotImplementedException();
    }
}

