using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using WebApiThrottle;
using System.Web.Http.Cors;

namespace BookingBio
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.MapHttpAttributeRoutes();
            //var cors = new EnableCorsAttribute("http://localhost:3000", "*", "*");
            //config.EnableCors(cors);
            config.EnableCors(new EnableCorsAttribute("http://localhost:3000", "*", "*"));


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                // Generic rate limit applied to ALL APIs
                Policy = new ThrottlePolicy(perSecond: 1, perMinute: 20, perHour: 200)
                {
                    IpThrottling = true,
                    ClientThrottling = true,
                    EndpointThrottling = true,
                    EndpointRules = new Dictionary<string, RateLimits>
        { 
             //Fine tune throttling per specific API here
            { "api/search", new RateLimits { PerSecond = 10, PerMinute = 100, PerHour = 1000 } }
        }
                },
                Repository = new CacheRepository()
            });

        }
       
    }
}
