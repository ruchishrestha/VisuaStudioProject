
using JsonServices;
using JsonServices.Web;

namespace WebApplication4
{
  
    public class Handler1 : JsonHandler
    {

        public Handler1()
        {
            this.service.Name = "WebApplication4";
            this.service.Description = "JSON API for android appliation";
            InterfaceConfiguration IConfig = new InterfaceConfiguration("RestAPI", typeof(IServiceAPI), typeof(ServiceAPI));
            this.service.Interfaces.Add(IConfig);
        }
    }
}