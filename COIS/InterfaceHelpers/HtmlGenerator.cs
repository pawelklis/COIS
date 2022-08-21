using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HtmlGenerator
{
    public List<IHtmlElement> Elements { get; set; }

    public HtmlGenerator()
    {
        Elements = new List<IHtmlElement>();
    }

    public void AddStartSection()
    {
        Elements.Add(new HtmlElementStartSection());
    } 
    public void AddEndSection()
    {
        Elements.Add(new HtmlElementEndSection());
    }

    public string toHtmlString()
    {
        StringBuilder sb = new StringBuilder();

        foreach(var el in Elements)
        {
            sb.AppendLine(el.toString());
        }

        return sb.ToString();
    }
    public class HtmlElementHeader : IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "header";
        public List<IHtmlElement> Elements { get; set; }
        private int size;
        public HtmlElementHeader(int _size)
        {
            if (_size < 1)
                _size = 1;
            if (_size > 6)
                _size = 6;

            Elements = new List<IHtmlElement>();
            size = _size;

            Class="header"+size;
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"<h{size} class='{Class}'>");
            sb.AppendLine(@$"{Text}");
            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</h{size}>");

            return sb.ToString();
        }
    }
    public class HtmlElementLabel:IHtmlElement
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public string Class { get; set; } = "label";
        public string NameClass { get; set; } = "labelname";
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementLabel()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"<div class='labelsection' style='page-break-inside:auto;'>");
            sb.AppendLine(@$"<div class='labelheader'>");
            sb.AppendLine(@$"<label class='{NameClass}'>");
            sb.AppendLine(@$"{Name}");
            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</label>");
            sb.AppendLine(@$"</div>");

            sb.AppendLine(@$"<div class='labelbody'>");
            sb.AppendLine(@$"<label class='{Class}'>");
            sb.AppendLine(@$"{Text}");
            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</label>");
            sb.AppendLine(@$"</div>");
            sb.AppendLine(@$"</div>");

            return sb.ToString();
        }
    }
    public class HtmlElementCheckBox: IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "checkbox";
        public bool Value { get; set; }
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementCheckBox()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@$"<div class='labelsection'>");
            sb.AppendLine(@$"<label class='{Class}'>");
            sb.AppendLine(@$"{Text}");

            if(Value==true)
                sb.AppendLine(@$"<span class='checkmarkyes'>TAK</span>");
            else
                sb.AppendLine(@$"<span class='checkmarkno'>NIE</span>");


            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</label>");
            sb.AppendLine(@$"</label>");

            return sb.ToString();
        }
    }
    public class HtmlElementSection :  IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "section";
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementSection()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"<div class='{Class}'>");
            sb.AppendLine(@$"{Text}");
            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</div>");

            return sb.ToString();
        }
    } 
    public class HtmlElementStartSection :  IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "section";
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementStartSection()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"<div class='{Class}'>");
            return sb.ToString();
        }
    }
    public class HtmlElementEndSection :  IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "section";
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementEndSection()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"</div>");
            return sb.ToString();
        }
    }

    public class HtmlElementTable :  IHtmlElement
    {
        public string Text { get; set; }
        public string Class { get; set; } = "table";
        public DataTable Table { get; set; }
        public List<IHtmlElement> Elements { get; set; }
        public HtmlElementTable()
        {
            Elements = new List<IHtmlElement>();
        }

        public string toString()
        {
            if (Table == null)
                Table = new DataTable();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@$"<table class='{Class}'>");
            sb.AppendLine(@$"<thead>");
            sb.AppendLine(@$"<tr>");
            for (int i = 0; i < Table.Columns.Count; i++)
            {
                sb.AppendLine(@$"<th>");
                sb.AppendLine(@$"{Table.Columns[i].ColumnName}");
                sb.AppendLine(@$"</th>");
            }
            sb.AppendLine(@$"</tr>");
            sb.AppendLine(@$"</thead>");
            sb.AppendLine(@$"<tbody>");
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                sb.AppendLine(@$"<tr>");
                for (int x = 0; x < Table.Columns.Count; x++)
                {
                    sb.AppendLine(@$"<td>");
                    sb.AppendLine(@$"{Table.Rows[i][x].ToString()}");
                    sb.AppendLine(@$"</td>");
                }
                sb.AppendLine(@$"</tr>");
            }
            sb.AppendLine(@$"</tbody>");
            Elements.ForEach(x => sb.AppendLine(x.toString()));
            sb.AppendLine(@$"</table>");

            return sb.ToString();
        }
    }
}

public enum IHtmlElemtTypes
{
    header,label,table,checkbox,section
}
public interface IHtmlElement
{
    public string Text { get; set; }
    public string Class { get; set; }
    public List<IHtmlElement> Elements { get; set; }
    public string toString();
}

