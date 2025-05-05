using LAHJA.Data.UI.Components.Base;

namespace Data.ProjectModels;
public class DataSkills
{

}



public class DataListProjectModul
{
    public string Label { get; set; } = "";
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; } = "";
    public string? IconColor { get; set; }
    public string Headline { get; set; }
    public List<string> Tags { get; set; } = new();
    public List<string> Features { get; set; } = new();
    public List<string> Skills { get; set; } = new();
}



public class ListUnifiedProjectModul : ComponentBaseCard<DataListProjectModul>
{
    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataListProjectModul db)
    {
        DataBuild = db;

    }


    public static ListUnifiedProjectModul Create(DataListProjectModul data)
    {

        var listUnifiedButtonModel = new ListUnifiedProjectModul();

        listUnifiedButtonModel.Build(data);
        return listUnifiedButtonModel;
    }
}


public class DataCardListProjectModul
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }

    public List<DataListProjectModul> Items { get; set; } = new List<DataListProjectModul>();
}

public class     CardListUnifiedProjectModul : ComponentBaseCard<DataCardListProjectModul>

{

    public List<ListUnifiedProjectModul> Items { get; set; } = new List<ListUnifiedProjectModul>();
    public override TypeComponentCard Type => throw new NotImplementedException();
    public override void Build(DataCardListProjectModul db)
    {
        DataBuild = db;
        foreach (var item in db.Items)
        {
            var listUnifiedButtonModel = ListUnifiedProjectModul.Create(item);
            Items.Add(listUnifiedButtonModel);
        }

    }

    public static     CardListUnifiedProjectModul Create(DataCardListProjectModul data)
    {
        var cardListUnifiedButtonModel = new     CardListUnifiedProjectModul();
        cardListUnifiedButtonModel.Build(data);
        return cardListUnifiedButtonModel;
    }

}

