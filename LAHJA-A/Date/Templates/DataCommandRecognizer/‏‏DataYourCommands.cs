
using LAHJA.Data.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dApp.Debug
{
    public class DataListModul
    {
        public string Label { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? IconColor { get; set; }
        public string? Link { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string NewCommand { get; set; } = string.Empty;
        public List<string> Commands { get; set; } = new();
    }


    public class ListUnifiedModul : ComponentBaseCard<DataListModul>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataListModul db)
        {
            DataBuild = db;
        }

        public static ListUnifiedModul Create(DataListModul data)
        {
            var instance = new ListUnifiedModul();
            instance.Build(data);
            return instance;
        }
    }
    public class DataListModul2
    {
        public string Title { get; set; } = "";
        public string Label { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? IconColor { get; set; }
        public string? Link { get; set; }
        public string? Name { get; set; }
        public int TrainingProgress { get; set; } = 0;

        public EventCallback OnAddCommand { get; set; }
        public string? NamCommand { get; set; }

        public string NewCommand = string.Empty;
        public string? Description { get; set; }
        public List<string> Commands { get; set; } = new();
    }


    public class ListUnifiedModul2 : ComponentBaseCard<DataListModul2>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataListModul2 db)
        {
            DataBuild = db;
        }

        public static ListUnifiedModul2 Create(DataListModul2 data)
        {
            var instance = new ListUnifiedModul2();
            instance.Build(data);
            return instance;
        }
    }

    public class DataListModul3
    {
        public string Title { get; set; } = "";
        public string? Name { get; set; }
        public int TrainingProgress { get; set; } = 0;


    }


    public class ListModul3 : ComponentBaseCard<DataListModul3>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataListModul3 db)
        {
            DataBuild = db;
        }

        public static ListModul3 Create(DataListModul3 data)
        {
            var instance = new ListModul3();
            instance.Build(data);
            return instance;
        }
    }

    public class DataCardListModul
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
        public DataListModul2? ItemLM2 { get; set; }
        public DataListModul3? modul3 { get; set; }

        public List<DataListModul> Items { get; set; } = new List<DataListModul>();
    }

    public class CardListUnifiedModul : ComponentBaseCard<DataCardListModul>
    {
        public List<ListUnifiedModul> Items { get; set; } = new List<ListUnifiedModul>();
        public ListUnifiedModul2? ItemLM2 { get; set; }
        public ListModul3? modul3 { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataCardListModul db)
        {
            DataBuild = db;

            ItemLM2 = ListUnifiedModul2.Create(db.ItemLM2);
            modul3 = ListModul3.Create(db.modul3);





            foreach (var item in db.Items)
            {
                var listUnifiedButtonModel = ListUnifiedModul.Create(item);
                Items.Add(listUnifiedButtonModel);
            }
        }

        public static CardListUnifiedModul Create(DataCardListModul data)
        {
            var instance = new CardListUnifiedModul();
            instance.Build(data);
            return instance;
        }
    }


    public class DataCardListModulw
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public EventCallback OnAddCommand { get; set; }
        public EventCallback<string> OnRemoveCommand { get; set; }

        public string NewCommand { get; set; } = string.Empty;
        public int TrainingProgress { get; set; } = 0;
        public List<DataCardListModul> Items { get; set; } = new List<DataCardListModul>();
    }

    public class CardListUnifiedModulw : ComponentBaseCard<DataCardListModulw>
    {
        public List<CardListUnifiedModul> Items { get; set; } = new List<CardListUnifiedModul>();

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataCardListModulw db)
        {
            DataBuild = db;
            foreach (var item in db.Items)
            {
                var listUnifiedButtonModel = CardListUnifiedModul.Create(item);
                Items.Add(listUnifiedButtonModel);
            }

            // Items = db.Items.Select(CardListUnifiedModul.Create).ToList();
        }

        public static CardListUnifiedModulw Create(DataCardListModulw data)
        {
            var instance = new CardListUnifiedModulw();
            instance.Build(data);
            return instance;
        }
    }

}