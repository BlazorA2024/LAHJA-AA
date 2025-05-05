using LAHJA.Data.UI.Components.Base;

namespace Data.DataHeaderModels;



//public class DataUnifiedButton
//{
//    public string Label { get; set; } = "";
//    public string? Icon { get; set; }
//    public string? Link { get; set; }
//    public string? Color { get; set; }
//    public bool IsPrimary { get; set; } = false;
//    public bool IsActive { get; set; } = true;

//}
//public class UnifiedButtonModel : ComponentBaseCard<DataUnifiedButton>
//{


//    public override TypeComponentCard Type => throw new NotImplementedException();

//    public override void Build(DataUnifiedButton db)
//    {
//        DataBuild = db;

//    }


//    public static UnifiedButtonModel Create(DataUnifiedButton data)
//    {
//        var unifiedButtonModel = new UnifiedButtonModel();
//        unifiedButtonModel.Build(data);
//        return unifiedButtonModel;
//    }
//}


//public class DataListButton
//{

//    public string Title { get; set; } = "";
//    public List<DataUnifiedButton> Items { get; set; } = new List<DataUnifiedButton>();
//}
//public class ListUnifiedButtonModel : ComponentBaseCard<DataListButton>
//{
//    public List<UnifiedButtonModel> Items { get; set; } = new List<UnifiedButtonModel>();
//    public override TypeComponentCard Type => throw new NotImplementedException();

//    public override void Build(DataListButton db)
//    {
//        DataBuild = db;

//        foreach (var item in db.Items)
//        {
//            var unifiedButtonModel = UnifiedButtonModel.Create(item);
//            Items.Add(unifiedButtonModel);
//        }


//    }


//    public static ListUnifiedButtonModel Create(DataListButton data)
//    {

//        var listUnifiedButtonModel = new ListUnifiedButtonModel();

//        listUnifiedButtonModel.Build(data);
//        return listUnifiedButtonModel;
//    }
//}


public class DataCardListHeaderModel
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Name { get; set; }
     public string? Description { get; set; }
    public string? SearchPlaceholder { get; set; }
     public string? ButtonLabel { get; set; }

    public string? Image { get; set; }
    public bool IsAdultModeEnabled { get; set; }     // تفعيل وضع البالغين
    public bool IsBlurNsfwEnabled { get; set; }
    public string? AdvancedFiltersText { get; set; }

    //public List<DataListButton> Items { get; set; } = new List<DataListButton>();
}

public class CardListHeaderModel : ComponentBaseCard<DataCardListHeaderModel>

{

   // public List<ListUnifiedButtonModel> Items { get; set; } = new List<ListUnifiedButtonModel>();
    public override TypeComponentCard Type => throw new NotImplementedException();
    public override void Build(DataCardListHeaderModel db)
    {
        DataBuild = db;
        //foreach (var item in db.Items)
        //{
        //    var listUnifiedButtonModel = ListUnifiedButtonModel.Create(item);
        //    Items.Add(listUnifiedButtonModel);
        //}

    }

    public static CardListHeaderModel Create(DataCardListHeaderModel data)
    {
        var cardListUnifiedButtonModel = new CardListHeaderModel();
        cardListUnifiedButtonModel.Build(data);
        return cardListUnifiedButtonModel;
    }

}

