using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LoggerType
{
    public event EventHandler<LoggerLog> OnLog;
    public event EventHandler<LoggerLog> OnStartWork;
    public event EventHandler<LoggerLog> OnEndWork;

    public void Log(string text,double percent,int count)
    {
        LoggerLog log = new LoggerLog()
        {
            Text = text,
            Percent = percent,
            Count = count
        };

        EventHandler<LoggerLog> handler = OnLog;
        handler?.Invoke(log, log);
    }

    public void StartWork()
    {
        EventHandler<LoggerLog> handler = OnStartWork;
        handler?.Invoke(new LoggerLog(), new LoggerLog());
    }
    public void EndWork()
    {
        EventHandler<LoggerLog> handler = OnEndWork;
        handler?.Invoke(new LoggerLog(), new LoggerLog());
    }


}
public class LoggerLog
{
    public string Text { get; set; }
    public double Percent { get; set; }
    public double Count { get; set; }
}

