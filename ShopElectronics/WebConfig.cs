using System.Web.Http;

namespace ShopElectronics;

public class WebConfig
{
    public static void Register(HttpConfiguration config)
    {
        config.MapHttpAttributeRoutes();
    }
}