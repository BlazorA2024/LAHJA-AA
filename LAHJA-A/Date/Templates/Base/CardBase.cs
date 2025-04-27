using Microsoft.AspNetCore.Components;


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

    }
    public interface IDataBaseComponentCard<T>
    {
        
        void Build(T db);

    }


    public interface IBaseComponentCard<T>: IDataBaseComponentCard<T>,IStyleBaseComponentCard
    {
        TypeComponentCard Type { get; }      
        bool IsActive { get; set; }
        bool IsAuth { get; set; }
        
        T DataBuild { set; get; }




    }


    public abstract class ComponentBaseCard<T> : ComponentBase, IBaseComponentCard<T>
    {
        public abstract TypeComponentCard Type { get; }
        public bool IsActive { get; set; } =true;
        public bool IsAuth { get; set; } = false;
        public T DataBuild { get; set; }
        public string? ClassItem { get; set; } = "";
        public string? ClassContainer { get; set; } = "";

        public abstract void Build(T db);
       
    }

}
