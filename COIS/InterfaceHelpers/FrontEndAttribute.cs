using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.All)]
public class FrontEndAttribute:Attribute
{
    // Private fields.
    private string name;
    private FrontendType frontendTypeControl;
    private bool show;
    private bool showInEditor;
    private ObjectListNames objectListName;
    private string objectListIDName;
    private string objectListNAMEName;

    // This constructor defines two required parameters: name and level.

    public FrontEndAttribute(string name, FrontendType frontendtypeControl, bool show, bool showineditor=true,
        ObjectListNames objectlistname = ObjectListNames.orgunits, string objectlistidname="",string objectlistnamename="")
    {
        this.name = name;
        this.frontendTypeControl = frontendtypeControl;
        this.show = show;
        this.showInEditor= showineditor;
        this.objectListName = objectlistname;
        this.objectListNAMEName = objectlistnamename;
        this.objectListIDName = objectlistidname;
    }

    public IList<T> ObjectList<T>(int param = 0)
    {
        switch (ObjectListName)
        {
            case ObjectListNames.orgunits:
                return (IList<T>)OrgUnit.GetOrgUnits();
            case ObjectListNames.employees:
                return (IList<T>)EmployeeType.GetEmployees(param);
            case ObjectListNames.zones:
                if(param==0)
                    return (IList<T>)ZoneType.GetZones();
                else
                    return (IList<T>)ZoneType.GetZoneByOrgUnitId(param);
            default:
                return new List<T>();
        }
        return new List<T>();
    }

    // Define Name property.
    // This is a read-only attribute.

    public virtual string Name
    {
        get { return name; }
    }

    // Define Level property.
    // This is a read-only attribute.

    public virtual FrontendType FrontendTypeControl
    {
        get { return frontendTypeControl; }
    }

    // Define Reviewed property.
    // This is a read/write attribute.

    public virtual bool Show
    {
        get { return show; }
        set { show = value; }
    }

    public virtual bool ShowInEditor
    {
        get { return showInEditor; }
        set { showInEditor = value; }
    }
    public virtual ObjectListNames ObjectListName
    {
        get { return objectListName; }
        set { objectListName = value; }
    }
    public virtual string ObjectListIDName
    {
        get { return objectListIDName; }
        set { objectListIDName = value; }
    }
    public virtual string ObjectListNameName
    {
        get { return objectListNAMEName; }
        set { objectListNAMEName = value; }
    }
}
public enum FrontendType
{
    text, number, select, checkbox, list,listmulti, image
}
public enum ObjectListNames
{
    orgunits,employees,zones
}