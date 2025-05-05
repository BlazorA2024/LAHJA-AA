using LAHJA.Data.UI.Components.Base;

namespace Data.ExperienceDetails;
public class DataSkills
{

}


public class DataUnifiedExperienceDetails
{
    public string Label { get; set; } = "";
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    

}

public class UnifiedExperienceDetailsModel : ComponentBaseCard<DataUnifiedExperienceDetails>
{


    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataUnifiedExperienceDetails db)
    {
        DataBuild = db;

    }


    public static UnifiedExperienceDetailsModel Create(DataUnifiedExperienceDetails data)
    {
        var unifiedButtonModel = new UnifiedExperienceDetailsModel();
        unifiedButtonModel.Build(data);
        return unifiedButtonModel;
    }
}


public class DataListExperienceDetailsModel
{
    public string Label { get; set; } = "";
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public string? Description { get; set; }
    public string Title { get; set; } = "";
    public string? IconColor { get; set; }
    public string Company { get; set; } = "";
    public string Period { get; set; } = "";
    public string Position { get; set; } = "";
    public List<DataUnifiedExperienceDetails> Items { get; set; } = new List<DataUnifiedExperienceDetails>();

}

    
public class ListUnifiedExperienceDetailsModel : ComponentBaseCard<DataListExperienceDetailsModel>
{
    public List<UnifiedExperienceDetailsModel> Items { get; set; } = new List<UnifiedExperienceDetailsModel>();

    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataListExperienceDetailsModel db)
    {
        DataBuild = db;
        foreach (var item in db.Items)
        {
            var unifiedButtonModel = UnifiedExperienceDetailsModel.Create(item);
            Items.Add(unifiedButtonModel);
        }

    }


    public static ListUnifiedExperienceDetailsModel Create(DataListExperienceDetailsModel data)
    {

        var listUnifiedButtonModel = new ListUnifiedExperienceDetailsModel();

        listUnifiedButtonModel.Build(data);
        return listUnifiedButtonModel;
    }
}


    
public class DataCardListExperienceDetailsModel
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
   
    public List<DataListExperienceDetailsModel> Items { get; set; } = new List<DataListExperienceDetailsModel>();
}

    
public class     CardListUnifiedExperienceDetailsModel : ComponentBaseCard<DataCardListExperienceDetailsModel>

{

    public List<ListUnifiedExperienceDetailsModel> Items { get; set; } = new List<ListUnifiedExperienceDetailsModel>();
    public override TypeComponentCard Type => throw new NotImplementedException();
    public override void Build(DataCardListExperienceDetailsModel db)
    {
        DataBuild = db;
        foreach (var item in db.Items)
        {
            var listUnifiedButtonModel = ListUnifiedExperienceDetailsModel.Create(item);
            Items.Add(listUnifiedButtonModel);
        }

    }

    public static     CardListUnifiedExperienceDetailsModel Create(DataCardListExperienceDetailsModel data)
    {
        var cardListUnifiedButtonModel = new     CardListUnifiedExperienceDetailsModel();
        cardListUnifiedButtonModel.Build(data);
        return cardListUnifiedButtonModel;
    }

}

