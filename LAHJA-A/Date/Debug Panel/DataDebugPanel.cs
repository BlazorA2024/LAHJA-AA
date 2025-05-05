using LAHJA.Data.UI.Components.Base;

namespace Data.DebugPanel;

public class DataUnifiedButton
{
    public string Label { get; set; } = "";
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public bool IsPrimary { get; set; } = false;
    public bool IsActive { get; set; } = true;

}
public class UnifiedButtonModel : ComponentBaseCard<DataUnifiedButton>
{


    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataUnifiedButton db)
    {
        DataBuild = db;

    }


    public static UnifiedButtonModel Create(DataUnifiedButton data)
    {
        var unifiedButtonModel = new UnifiedButtonModel();
        unifiedButtonModel.Build(data);
        return unifiedButtonModel;
    }
}


public class DataListButton
{

    public string Title { get; set; } = "";
    public List<DataUnifiedButton> Items { get; set; } = new List<DataUnifiedButton>();
}
public class ListUnifiedButtonModel : ComponentBaseCard<DataListButton>
{
    public List<UnifiedButtonModel> Items { get; set; } = new List<UnifiedButtonModel>();
    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataListButton db)
    {
        DataBuild = db;

        foreach (var item in db.Items)
        {
            var unifiedButtonModel = UnifiedButtonModel.Create(item);
            Items.Add(unifiedButtonModel);
        }


    }


    public static ListUnifiedButtonModel Create(DataListButton data)
    {

        var listUnifiedButtonModel = new ListUnifiedButtonModel();

        listUnifiedButtonModel.Build(data);
        return listUnifiedButtonModel;
    }
}


public class DataCardListButtonModel
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<DataListButton> Items { get; set; } = new List<DataListButton>();
}

public class CardListUnifiedButtonModel : ComponentBaseCard<DataCardListButtonModel>

{

    public List<ListUnifiedButtonModel> Items { get; set; } = new List<ListUnifiedButtonModel>();
    public override TypeComponentCard Type => throw new NotImplementedException();
    public override void Build(DataCardListButtonModel db)
    {
        DataBuild = db;
        foreach (var item in db.Items)
        {
            var listUnifiedButtonModel = ListUnifiedButtonModel.Create(item);
            Items.Add(listUnifiedButtonModel);
        }

    }

    public static CardListUnifiedButtonModel Create(DataCardListButtonModel data)
    {
        var cardListUnifiedButtonModel = new CardListUnifiedButtonModel();
        cardListUnifiedButtonModel.Build(data);
        return cardListUnifiedButtonModel;
    }

}

