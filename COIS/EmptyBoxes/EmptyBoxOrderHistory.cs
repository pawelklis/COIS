using System;

public class EmptyBoxOrderHistory : INoDatabase
{
    public string Guid { get; set; }
    public DateTime Date { get; set; }
    public UserType User { get; set; }
    public EmptyBoxOrderStatus Status { get; set; }
    public string? Comment { get; set; }
    public ZoneType Zone { get; set; }

    public EmptyBoxOrderHistory()
    {
        Guid = System.Guid.NewGuid().ToString();
    }


    public enum EmptyBoxOrderStatus 
    { 
        New,InProgress,Completed,PartCompleted,Canceled
    }

}