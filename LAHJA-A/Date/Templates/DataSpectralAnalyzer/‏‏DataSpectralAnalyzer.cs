
using LAHJA.Data.UI.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using static MudBlazor.CategoryTypes;

namespace dApp.Spectral
{
    public class DataVolumevoice
    {
        public string? Name { get; set; }

        public int SelectedValue { get; set; }        
        public EventCallback<int> SelectedValueChanged { get; set; }

        public List<int> Sizes { get; set; }

    }

    public class StyleListDataVolumevoice : StyleBaseComponentCard
    {
        [Parameter]
        public  string? ClassSelect { set; get; }
        [Parameter]
        public string? ClassIcon { set; get; }

        public static string KeyClassSelect { get; set; } = "classSelect";
        public static string KeyClassIcon { get; set; } = "classIcon";

        public readonly new static Dictionary<string, string> CLASSES = new Dictionary<string, string>()
        {
            {KeyClassItem, StyleBaseComponentCard.KeyClassItem },
            {KeyClassContainer, StyleBaseComponentCard.KeyClassContainer },
            {KeyClassSelect, "" },
            {KeyClassIcon, "" },
            { "classTextIcon", "" },
            { "classCard", "" }


        };
        public override Task<bool> UpdateStyleAsync(Dictionary<string, string> classes)
        {

            if(ClassSelect == null)
                ClassSelect = "";
            if (ClassIcon == null)
                ClassIcon = "";

            if (classes == null || !IsIgnoredStyle)
                return Task.FromResult(false);


            ClassSelect += classes[KeyClassSelect];
            ClassIcon += classes[KeyClassIcon];


            return base.UpdateStyleAsync(classes);
        }

      
    }
    public class ListDataVolumevoice : ComponentBaseCard<DataVolumevoice>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

    



        public   static ICollection<string>  NAMECLASSES => StyleListDataVolumevoice.CLASSES.Keys.ToList();


     


        public ListDataVolumevoice()
        {

           

        }
        public override void Build(DataVolumevoice db)
        {
            DataBuild = db;
        }

        public static ListDataVolumevoice Create(DataVolumevoice data)
        {
            var instance = new ListDataVolumevoice();
            instance.Build(data);
            return instance;
        }



     
    }
    public class DataSmoothing
    {
        public string? Name { get; set; }
        public string? Sharp { get; set; }
        public string Smooth { get; set; } = "";
        public double Value { get; set; }

    }


    public class ListDataSmoothing : ComponentBaseCard<DataSmoothing>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataSmoothing db)
        {
            DataBuild = db;
        }

        public static ListDataSmoothing Create(DataSmoothing data)
        {
            var instance = new ListDataSmoothing();
            instance.Build(data);
            return instance;
        }
    }

    public class DataVolumevoicess
    {
        public string? Name { get; set; }
        public double Volume { get; set; }
        public string Title { get; set; } = "";
        public string? Icon { get; set; }



    }


    public class ListDataVolumevoicess : ComponentBaseCard<DataVolumevoicess>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataVolumevoicess db)
        {
            DataBuild = db;
        }

        public static ListDataVolumevoicess Create(DataVolumevoicess data)
        {
            var instance = new ListDataVolumevoicess();
            instance.Build(data);
            return instance;
        }
    }

    public class DataCardControls
    {
        public string? Name { get; set; }
        public string Title { get; set; } = "";
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public DataSmoothing? ISharp { get; set; }
        public DataVolumevoicess? IVolume { get; set; }
        public DataVolumevoice? ISizes { get; set; }

        //public List<DataVolumevoice> Items { get; set; } = new List<DataVolumevoice>();
    }

    public class CardListDataCardControls : ComponentBaseCard<DataCardControls>
    {
        public ListDataVolumevoice? ISizes { get; set; }

        public List<ListDataVolumevoice> Items { get; set; } = new List<ListDataVolumevoice>();
        public ListDataSmoothing? ISharp { get; set; }
        public ListDataVolumevoicess? IVolume { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataCardControls db)
        {
            DataBuild = db;
            ISizes = ListDataVolumevoice.Create(db.ISizes);

            ISharp = ListDataSmoothing.Create(db.ISharp);
            IVolume = ListDataVolumevoicess.Create(db.IVolume);

            //foreach (var item in db.Items)
            //{
            //    var listUnifiedButtonModel = ListDataVolumevoice.Create(item);
            //    Items.Add(listUnifiedButtonModel);
            //}
        }

        public static CardListDataCardControls Create(DataCardControls data)
        {
            var instance = new CardListDataCardControls();
            instance.Build(data);
            return instance;
        }
    }
    public class DataFrequency
    {
        public string? Name { get; set; }
        public string Label { get; set; } = "";
        public string Value { get; set; } 
       
    }
    public class ListDataFrequency: ComponentBaseCard<DataFrequency>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataFrequency db)
        {
            DataBuild = db;
        }

        public static ListDataFrequency Create(DataFrequency data)
        {
            var instance = new ListDataFrequency();
            instance.Build(data);
            return instance;
        }
    }
    public class DataCardListModulFrequency
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        // public DataCardControls? IControls { get; set; }
     
        public List<DataFrequency> Items { get; set; } = new List<DataFrequency>();
    }

    public class CardListDataCardListModulFrequency : ComponentBaseCard<DataCardListModulFrequency>
    {
        public List<ListDataFrequency> Items { get; set; } = new List<ListDataFrequency>();
       // public CardListDataCardControls? IControls { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataCardListModulFrequency db)
        {
            DataBuild = db;
        //    IControls = CardListDataCardControls.Create(db.IControls);

            foreach (var item in db.Items)
            {
                var listUnifiedButtonModel = ListDataFrequency.Create(item);
                Items.Add(listUnifiedButtonModel);
            }

            // Items = db.Items.Select(CardListDataCardControls.Create).ToList();
        }

        public static CardListDataCardListModulFrequency Create(DataCardListModulFrequency data)
        {
            var instance = new CardListDataCardListModulFrequency();
            instance.Build(data);
            return instance;
        }
    }
    public  class DataColors
    {
        public string Div { get; set; } = string.Empty;
        public string? Colors { get; set; }
    }
    public class DataSpectralAnalyzer
    {
        public string? Name { get; set; }
        public string Label { get; set; } = "";
        public string Title { get; set; }
        public List<string> IDiv { get; set; } = new List<string>();

        
    }
    public class ListDataSpectralAnalyzer : ComponentBaseCard<DataSpectralAnalyzer>
    {

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataSpectralAnalyzer db)
        {
            DataBuild = db;


        }

        public static ListDataSpectralAnalyzer Create(DataSpectralAnalyzer data)
        {
            var instance = new ListDataSpectralAnalyzer();
            instance.Build(data);
            return instance;
        }
    }
    public class DataAddSpectral
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public DataCardControls? IControls { get; set; }
        public DataCardListModulFrequency? Ifreq { get; set; }
        public DataSpectralAnalyzer? IAnal { get; set; }

    }

    public class CardListDataAddSpectral : ComponentBaseCard<DataAddSpectral>
    {
        public CardListDataCardControls? IControls { get; set; }
        public CardListDataCardListModulFrequency? Ifreq { get; set; }
        public ListDataSpectralAnalyzer? IAnal { get; set; }

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataAddSpectral db)
        {
            DataBuild = db;
            IControls = CardListDataCardControls.Create(db.IControls);
            Ifreq = CardListDataCardListModulFrequency.Create(db.Ifreq);
            IAnal = ListDataSpectralAnalyzer.Create(db.IAnal);

            

            // Items = db.Items.Select(CardListDataCardControls.Create).ToList();
        }

        public static CardListDataAddSpectral Create(DataAddSpectral data)
        {
            var instance = new CardListDataAddSpectral();
            instance.Build(data);
            return instance;
        }
    }
   
}