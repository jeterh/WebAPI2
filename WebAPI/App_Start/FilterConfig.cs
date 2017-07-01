using System.Web;
using System.Web.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new MyExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
