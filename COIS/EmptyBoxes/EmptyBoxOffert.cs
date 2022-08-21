

public class EmptyBoxOffert
{
    public static event EventHandler<EmptyBoxOffert> OnAccept;
    public static event EventHandler<EmptyBoxOffert> OnCancel;
    public string GuidId { get;private set; }
    public DateTime AddTime { get; set; }
    public DateTime EndTime { get; set; }
    public int OrgUnitId { get; set; }
    public int Amount { get; set; }
    /// <summary>
    ///  1=nowe,2=zatwierdzone,4-zatwierdzone częściowo,3-odrzucone
    /// </summary>
    public int Status { get; set; }
    public string? Comment { get; set; }
    public UserType User { get; set; }
    public int AcceptedAmount { get; set; }
    public string? ActiontComment { get; set; }
    public UserType ActiontUser { get; set; }

    public EmptyBoxOffert()
    {
        GuidId = Guid.NewGuid().ToString();
    }

    public void Accept(UserType acceptuser, string acceptcomment,int acceptamount=0)
    {
        ActiontUser = acceptuser;
        ActiontComment = acceptcomment;
        if (acceptamount == 0 || acceptamount==Amount)
        {
            AcceptedAmount = Amount;
            Status = 2;
        }
        else
        {
            AcceptedAmount = acceptamount;
            Status = 4;
        }

        EndTime = DateTime.Now;

        EventHandler<EmptyBoxOffert> handler = OnAccept;
        handler?.Invoke(this, this);
    }
    public void Cancel(UserType user, string comment)
    {
        ActiontUser = user;
        ActiontComment = comment;
        Status = 3;
        EndTime = DateTime.Now;
        EventHandler<EmptyBoxOffert> handler = OnCancel;
        handler?.Invoke(this, this);
    }
    public string StatusString()
    {
        //1=nowe,2=zatwierdzone,4-zatwierdzone częściowo,3-odrzucone
        if (Status == 1)
            return "Nowe";
        if (Status == 2)
            return "Zatwierdzone";
        if (Status == 4)
            return "Zatwierdzone częściowo";
        if (Status == 3)
            return "Odrzucone";
        return "";
    }

    public OrgUnit Orgunit()
    {
        return OrgUnit.GetOrgUnitById(OrgUnitId);
    }
}