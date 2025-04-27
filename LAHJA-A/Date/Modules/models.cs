using LAHJA.Data.UI.Components.Base;

namespace Data.Modelues;

public class ServiceBase
{
    // خصائص عامة لأي خدمة
    public string Name { get; set; }            // اسم الخدمة
    public string Description { get; set; }     // وصف الخدمة
    public string ServiceUrl { get; set; }      // رابط الخدمة
    public bool IsActive { get; set; }          // حالة الخدمة
    public string Text { get; set; }             // نص عام
    public string ButtonLabel { get; set; }      // نص الزر
    public string Image { get; set; }   // صورة الخلفية
    public string Link { get; set; }             // رابط الزر أو رابط آخر
    public string Icon { get; set; }          // رابط الأيقونة
    public string ImageUrl { get; set; }         // رابط الصورة
    public string Title { get; set; }            // عنوان رئيسي
    public string Subtitle { get; set; }         // عنوان فرعي
    public string SearchPlaceholder { get; set; } // نص حقل البحث
    public string CssClass { get; set; }         // كلاس CSS
    public string CustomStyle { get; set; }      // ستايل خاص مخصص
    public bool IsAdultModeEnabled { get; set; }     // تفعيل وضع البالغين
    public bool IsBlurNsfwEnabled { get; set; }      // تفعيل تشويش الصور الحساسة
    public string AdvancedFiltersText { get; set; }  // نص زر الفلاتر المتقدمة
    public string Email { get; set; }
    public ServiceBase()
    {
        IsActive = true;
        //CssClass = string.Empty;
        //CustomStyle = string.Empty;
    }
}

public class HeaderModel : ServiceBase
{
 
    //public HeaderModel()
    //{
    //    Name = "Discover";
    //    Title = " the Best AI Tools";
    //    Subtitle = "Explore our curated collection of cutting-edge AI tools to boost your productivity and creativity.";
    //    Image = "https://images.unsplash.com/photo-1677442135136-760c813a743d?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2070&q=80";
    //    SearchPlaceholder = "Search AI tools...";
    //    ButtonLabel = "Search";
    //    IsAdultModeEnabled = false;
    //    IsBlurNsfwEnabled = true;
    //    AdvancedFiltersText = "Advanced Filters";
    //}

    
    
}
public class IconMediaCard : ServiceBase
{
   
}


public  class DataUnifiedButton
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
    

    public static   ListUnifiedButtonModel Create(DataListButton data)
    {
       
        var listUnifiedButtonModel = new ListUnifiedButtonModel();
        
        listUnifiedButtonModel.Build(data);
        return listUnifiedButtonModel;
    }
}


public  class DataCardListButtonModel
{
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public  string? Name { get; set; }
    public string? Description { get; set; }

    public  List<DataListButton> Items { get; set; } = new List<DataListButton>();
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

public class ToolCard : ServiceBase
{
    public string? Model { get; set; }
}
public class HeroSectionModel : ServiceBase 
{
    public bool IsEmail { get; set; } = false;
    public bool IsGit { get; set; } = false;
    public bool IsLinkedin { get; set; } = false;
    public bool IsDiscord { get; set; } = false;
    public string IconColr { get; set; } = "";

    public string? ContactEmail { get; set; }
    public string? GithubUrl { get; set; }
    public string? LinkedinUrl { get; set; }
    public string? DiscordUrl { get; set; }
    public string? ContactLink { get; set; }
    //public string ImageUrl { get; set; }
   
}
