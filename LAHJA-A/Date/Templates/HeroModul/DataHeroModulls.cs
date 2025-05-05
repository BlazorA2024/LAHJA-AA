using LAHJA.Data.UI.Components.Base;

namespace Data.HeroModul;



public class DataListToolsModul
{
    public string Label { get; set; } = "";
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; } = "";
    public string? IconColor { get; set; }
    public string ButtonLabel { get; set; }
    public string Model { get; set; }
}


public class ListUnifiedToolsModul : ComponentBaseCard<DataListToolsModul>
{
    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataListToolsModul db)
    {
        DataBuild = db;

    }


    public static ListUnifiedToolsModul Create(DataListToolsModul data)
    {

        var listUnifiedButtonModel = new ListUnifiedToolsModul();

        listUnifiedButtonModel.Build(data);
        return listUnifiedButtonModel;
    }
}


public class DataCardListToolsModul
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }

    public List<DataListToolsModul> Items { get; set; } = new List<DataListToolsModul>();
}

public class CardListUnifiedToolsModul : ComponentBaseCard<DataCardListToolsModul>

{

    public List<ListUnifiedToolsModul> Items { get; set; } = new List<ListUnifiedToolsModul>();
    public override TypeComponentCard Type => throw new NotImplementedException();
    public override void Build(DataCardListToolsModul db)
    {
        DataBuild = db;
        foreach (var item in db.Items)
        {
            var listUnifiedButtonModel = ListUnifiedToolsModul.Create(item);
            Items.Add(listUnifiedButtonModel);
        }

    }

    public static CardListUnifiedToolsModul Create(DataCardListToolsModul data)
    {
        var cardListUnifiedButtonModel = new CardListUnifiedToolsModul();
        cardListUnifiedButtonModel.Build(data);
        return cardListUnifiedButtonModel;
    }

}

