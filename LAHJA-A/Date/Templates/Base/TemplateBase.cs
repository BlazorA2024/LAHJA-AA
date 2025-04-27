//using AutoMapper;
//using LAHJA.Helpers.Services;

//namespace LAHJA.Data.UI.Templates.Base;


//public  enum TypeTemplate
//{
//    Base
//}


//public  interface IBuilderApi<T>
//{
    
//    //Task <bool> SendAsync(E data);
//    //T DeleteAsync(E data);
//    //T GetOneAsync(E data);
//    //IEnumerable<T> GetAllAsync(int skip = 0, int take = 1);
//}

//public interface IBuilderComponents<T>
//{
//}

//public class BuilderApi<T,E> : IBuilderApi<E> 
//{
//    protected readonly IMapper Mapper;
//    protected readonly T Service;

//    public BuilderApi(IMapper mapper, T service) { 
//        Mapper = mapper;
//        this.Service = service;
//    }

//    public object GetInstance()
//    {
//        return this;
//    }
//}

//public class BuilderComponents<T> : IBuilderComponents<T>
//{
  
//}
//public interface ITemplateBase<T,E>
//{
//    bool IsActive { set; get; }
//    TypeTemplate Type { get; }

 

//}


//public abstract class TemplateBase<T,E> : ITemplateBase<T, E>
//{
//    public bool IsActive { get; set; }
//    //public bool IsAuth { get=> _isAuth; }
//    //protected bool _isAuth = false;
//    public  TypeTemplate Type { get=> TypeTemplate.Base; }

//    protected readonly IMapper mapper;
//    protected readonly AuthService authService;
//    //protected readonly ProtectedSessionStorage PSession;
//    protected readonly T client;
//    protected List<string> _errors;


//    public TemplateBase(IMapper mapper,
//        AuthService authService,
//        //ProtectedSessionStorage PSession,
//    T client)
//    {

//        this.mapper = mapper;
//        _errors = new List<string>();
//        this.authService = authService;
//        this.client = client;
//        //this.PSession = PSession;
//        //_isAuth = authService.isAuth().Result;
//        //this.customAuthenticationStateProvider = customAuthenticationStateProvider;
//    }

//}





