public class DeviceDamageHistory
{
    public string? Description { get; set; }
    public UserType User { get; set; }
    public DateTime Time { get; set; }
    public DeviceDamageLevel DamageLevel { get; set; }

    public DeviceDamageHistory(string description, UserType user, DeviceDamageLevel damageLevel)
    {
        Description = description;
        User = user;
        Time = DateTime.Now;
        DamageLevel = damageLevel;
    }
}

public enum DeviceDamageLevel
{
    Zarejestrowano,Przyjęto,Przekazano,Zwrócono,Naprawiono,Zutylizowano,Komentarz,Zakończono
}