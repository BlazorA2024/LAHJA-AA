using LAHJA.Data.UI.Components.Base;
using Microsoft.AspNetCore.Components;
namespace Data.SpectralVoice
{
    public class DataInputLevel
    {
        public string? Name { get; set; } = "Input Level";
        public string? IdText { get; set; } = "inputLevel";
        public string? IdMeter { get; set; } = "levelMeter";
        public string LevelText { get; set; } = "0%";
        public string MeterColor { get; set; } = "bg-green-500";
        public string WidthStyle { get; set; } = "0%";
    }
    public class ListDataInputLevel : ComponentBaseCard<DataInputLevel>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataInputLevel db)
        {
            DataBuild = db;
        }

        public static ListDataInputLevel Create(DataInputLevel data)
        {
            var instance = new ListDataInputLevel();
            instance.Build(data);
            return instance;
        }
    }
    public class DataControlHeader
    {
        public string Title { get; set; } = "Controls";
        public string StatusText { get; set; } = "Inactive";
        public string StatusColor { get; set; } = "bg-red-500";
    }
    public class ListDataControlHeader : ComponentBaseCard<DataControlHeader>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataControlHeader db)
        {
            DataBuild = db;
        }

        public static ListDataControlHeader Create(DataControlHeader data)
        {
            var instance = new ListDataControlHeader();
            instance.Build(data);
            return instance;
        }
    }

    public class DataMicToggleButton
    {
        public string? Id { get; set; } = "toggleMic";
        public string? Icon { get; set; } = "fas fa-microphone";
        public string? Label { get; set; } = "Start Analyzer";
        public string? BackgroundColor { get; set; } = "bg-blue-600";
        public string? HoverColor { get; set; } = "hover:bg-blue-700";
    }
    public class ListDataMicToggleButton : ComponentBaseCard<DataMicToggleButton>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataMicToggleButton db)
        {
            DataBuild = db;
        }

        public static ListDataMicToggleButton Create(DataMicToggleButton data)
        {
            var instance = new ListDataMicToggleButton();
            instance.Build(data);
            return instance;
        }
    }
    public class DataAnalyzerSettings
    {
        public string? Name { get; set; }

        public List<string> Options { get; set; } = new(); 

        public string SelectedValue { get; set; } = string.Empty;
    }



    public class ListDataAnalyzerSettings : ComponentBaseCard<DataAnalyzerSettings>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataAnalyzerSettings db)
        {
            DataBuild = db;
        }

        public static ListDataAnalyzerSettings Create(DataAnalyzerSettings data)
        {
            var instance = new ListDataAnalyzerSettings();
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


public class DataCardControls
{
    public string? Name { get; set; }
    public string Title { get; set; } = "";
    public string? Icon { get; set; }
    public string? Description { get; set; }
    
    public DataVolumevoice? ISizes { get; set; }

    public List<DataAnalyzerSettings> Items { get; set; } = new List<DataAnalyzerSettings>();
}

public class CardListDataCardControls : ComponentBaseCard<DataCardControls>
{

    public List<ListDataAnalyzerSettings> Items { get; set; } = new List<ListDataAnalyzerSettings>();
    
    public override TypeComponentCard Type => throw new NotImplementedException();

    public override void Build(DataCardControls db)
    {
        DataBuild = db;

        

            foreach (var item in db.Items)
            {
                var listUnifiedButtonModel = ListDataAnalyzerSettings.Create(item);
                Items.Add(listUnifiedButtonModel);
            }
        }

    public static CardListDataCardControls Create(DataCardControls data)
    {
        var instance = new CardListDataCardControls();
        instance.Build(data);
        return instance;
    }
}



    public class DataVolumevoice
    {
        public string? Name { get; set; }

        public int SelectedValue { get; set; }
        public EventCallback<int> SelectedValueChanged { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; } = "";
        public List<int> Sizes { get; set; }

    }


    public class ListDataVolumevoice : ComponentBaseCard<DataVolumevoice>
    {
        public override TypeComponentCard Type => throw new NotImplementedException();

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
    public class DataFrequency
    {
        public string Title { get; set; } = "Current Frequency";  // العنوان
        public double Frequency { get; set; } = 0.0;  // التردد الحالي
    }
    public class ListDataFrequency : ComponentBaseCard<DataFrequency>
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

    public class DataAddSpectral
    {
        public string Title { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Link { get; set; }
        public DataMicToggleButton? MicButton { get; set; }
        public DataFrequency? IFrequency { get; set; } // ✅ جديد

        public DataCardControls? IControls { get; set; }
        public DataVolumevoice? ISizes { get; set; }
        public DataControlHeader? ControlHeader { get; set; }
        public DataInputLevel? InputLevel { get; set; } // ✅ جديد
    }

    public class CardListDataAddSpectral : ComponentBaseCard<DataAddSpectral>
    {
        public CardListDataCardControls? IControls { get; set; }

        public ListDataFrequency? IFrequency { get; set; }
        public ListDataVolumevoice? ISizes { get; set; }
       public ListDataInputLevel? InputLevel { get; set; } // ✅ جديد
        public ListDataControlHeader? ControlHeader { get; set; } // ✅ جديد
        public ListDataMicToggleButton? MicButton { get; set; } // ✅ جديد

        public override TypeComponentCard Type => throw new NotImplementedException();

        public override void Build(DataAddSpectral db)
        {
            
            DataBuild = db;
            IControls = CardListDataCardControls.Create(db.IControls);
            ISizes = ListDataVolumevoice.Create(db.ISizes);
            InputLevel = ListDataInputLevel.Create(db.InputLevel); // ✅
            ControlHeader = ListDataControlHeader.Create(db.ControlHeader); // ✅
            MicButton = ListDataMicToggleButton.Create(db.MicButton); // ✅
            IFrequency = ListDataFrequency.Create(db.IFrequency); // ✅

        }

        public static CardListDataAddSpectral Create(DataAddSpectral data)
        {
            var instance = new CardListDataAddSpectral();
            instance.Build(data);
            return instance;
        }
    }



    //    public class DataAddSpectral
    //{
    //    public string Title { get; set; } = string.Empty;
    //    public string? Icon { get; set; }
    //    public string? Name { get; set; }
    //    public string? Description { get; set; }
    //    public string? Image { get; set; }
    //    public string? Link { get; set; }
    //    public DataCardControls? IControls { get; set; }
    //        public DataVolumevoice? ISizes { get; set; }

    //    }

    //    public class CardListDataAddSpectral : ComponentBaseCard<DataAddSpectral>
    //{
    //    public CardListDataCardControls? IControls { get; set; }
    //        public ListDataVolumevoice? ISizes { get; set; }

    //        public override TypeComponentCard Type => throw new NotImplementedException();

    //    public override void Build(DataAddSpectral db)
    //    {
    //        DataBuild = db;
    //        IControls = CardListDataCardControls.Create(db.IControls);

    //            ISizes = ListDataVolumevoice.Create(db.ISizes);


    //            // Items = db.Items.Select(CardListDataCardControls.Create).ToList();
    //        }

    //        public static CardListDataAddSpectral Create(DataAddSpectral data)
    //    {
    //        var instance = new CardListDataAddSpectral();
    //        instance.Build(data);
    //        return instance;
    //    }
    //}

}