using dApp.Debug;
using LAHJA.Data.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Non
{
    public class DataListNon
    {
        public string Title { get; set; } = "";
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public List<DataActionButton> ItemButton { get; set; } = new List<DataActionButton>();

        public string? CurrentCommand { get; set; }
        public string? ClassCurrentCommand { get; set; }
        public string? ClassSampleCount { get; set; } 
        public int SampleCount { get; set; } = 0;
        public string? RecognizedCommand { get; set; } 
        public int Confidence { get; set; } = 0;
        public bool IsContinuousMode { get; set; } = false;


    }


    public class ListNon : ComponentBaseCard<DataListNon>
    {
        public List<ListModelActionButtons> ItemButton { get; set; } = new List<ListModelActionButtons>();

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataListNon db)
        {
            DataBuild = db;
            foreach (var item in db.ItemButton)
            {
                var listUnifiedButtonModel = ListModelActionButtons.Create(item);
                ItemButton.Add(listUnifiedButtonModel);
            }
        }

        public static ListNon Create(DataListNon data)
        {
            var instance = new ListNon();
            instance.Build(data);
            return instance;
        }
    }
    public class DataListNonCurrent
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string? SubText { get; set; }
        public string ValueClass { get; set; } = "text-white";


    }


    public class ListNonCurrent : ComponentBaseCard<DataListNonCurrent>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataListNonCurrent db)
        {
            DataBuild = db;
        }

        public static ListNonCurrent Create(DataListNonCurrent data)
        {
            var instance = new ListNonCurrent();
            instance.Build(data);
            return instance;
        }
    }
    public class DataActionButton
    {
        public string Label { get; set; } = "";
        public string IconClass { get; set; } = "";
        public string CssClass { get; set; } = "";
        public EventCallback Callback { get; set; }
    }
    public class ListModelActionButtons : ComponentBaseCard<DataActionButton>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataActionButton db)
        {
            DataBuild = db;
        }

        public static ListModelActionButtons Create(DataActionButton data)
        {
            var instance = new ListModelActionButtons();
            instance.Build(data);
            return instance;
        }
    }
    public class DataCardListNonModul
    {
        public string Title { get; set; } = "";
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public EventCallback<string> OnRemoveCommand { get; set; }
        public string? NamCommand { get; set; }

        public string NewCommand = string.Empty;
        public int TrainingProgress { get; set; } = 0;
        public DataListNon? modul3 { get; set; }
        public List<DataActionButton> ItemButton { get; set; } = new List<DataActionButton>();

        public List<DataListNonCurrent> Items { get; set; } = new List<DataListNonCurrent>();
    }

    public class CardListNoneModul : ComponentBaseCard<DataCardListNonModul>
    {
        public List<ListNonCurrent> Items { get; set; } = new List<ListNonCurrent>();
        public List<ListModelActionButtons> ItemButton { get; set; } = new List<ListModelActionButtons>();

        public ListNon? modul3 { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataCardListNonModul db)
        {
            DataBuild = db;

            modul3 = ListNon.Create(db.modul3);

            foreach (var item in db.Items)
            {
                var listUnifiedButtonModel = ListNonCurrent.Create(item);
                Items.Add(listUnifiedButtonModel);
            }
            foreach (var item in db.ItemButton)
            {
                var listUnifiedButtonModel = ListModelActionButtons.Create(item);
                ItemButton.Add(listUnifiedButtonModel);
            }
        }

        public static CardListNoneModul Create(DataCardListNonModul data)
        {
            var instance = new CardListNoneModul();
            instance.Build(data);
            return instance;
        }
    }
}