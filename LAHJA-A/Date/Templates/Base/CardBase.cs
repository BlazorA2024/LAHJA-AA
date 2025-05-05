using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;


namespace LAHJA.Data.UI.Components.Base
{

    public  enum TypeComponentCard
    {
        Base,
        Icon,
        Text,
        TextAndIcon,
        Card
    } 

    public interface IStyleBaseComponentCard
    {
        public string? ClassItem { set; get; }
        public string? ClassContainer { set; get; }

        public Dictionary<string,string> ? Classes { set; get; }
        public bool IsIgnoredStyle { get; set; }

    }

    public  class StyleBaseComponentCard : ComponentBase, IStyleBaseComponentCard
    {
        public static string KeyClassItem { get; set; } = "classItem";
        public static string KeyClassContainer { get; set; } = "classContainer";


        [Parameter]
        public string? ClassItem { get; set; } = "";
        [Parameter]
        public string? ClassContainer { get; set; } = "";
        [Parameter]
        public Dictionary<string, string>? Classes { set; get; }
        [Parameter]
        public bool IsIgnoredStyle { set; get; }
        public readonly static Dictionary<string, string> CLASSES = new Dictionary<string, string>()
        {
            {KeyClassItem, "" },
            {KeyClassContainer, "" },
           


        };

       
        public virtual Task<bool> UpdateStyleAsync(Dictionary<string, string>  classes)
        {

            if (ClassContainer == null)
                ClassContainer = "";
            if (ClassItem == null)
                ClassItem = "";

            if (Classes == null)
                Classes = new Dictionary<string, string>();


            if (classes == null||!IsIgnoredStyle)
                return Task.FromResult(false);
           
           
            ClassItem += classes[KeyClassItem];
            ClassContainer += classes[KeyClassContainer];


            

          foreach (var item in classes)
                {
                    if (!Classes.ContainsKey(item.Key))
                        Classes.Add(item.Key, item.Value);
                    else
                        Classes[item.Key] += item.Value;
                }
            



            return Task.FromResult(true);
        }

       
    }

    public interface IDataBaseComponentCard<T>
    {
        
        void Build(T db);

    }


    public interface IBaseComponentCard<T>: IDataBaseComponentCard<T>
    {
        TypeComponentCard Type { get; }      
        bool IsActive { get; set; }
        bool IsAuth { get; set; }
        
        T DataBuild { set; get; }




    }


    public abstract class ComponentBaseCard<T> : ComponentBase, IBaseComponentCard<T>
    {


      
       
        public ComponentBaseCard()
        {
           
        }
        public abstract TypeComponentCard Type { get; }
        public bool IsActive { get; set; } =true;
        public bool IsAuth { get; set; } = false;
        public T DataBuild { get; set; }
       
       
       

       
       
        public abstract void Build(T db);


       


    }

}
