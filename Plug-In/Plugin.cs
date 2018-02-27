
using System;
using System.ServiceModel;

// Microsoft Dynamics CRM namespace(s)
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using CRMLib;
namespace Plug_Ins
{
    public class CalculatePricePlugin : IPlugin
    {       
        public void Execute(IServiceProvider serviceProvider)
        {
            //Extract the tracing service for use in debugging sandboxed plug-ins.
            ITracingService tracingService =
                (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            
            // Obtain the execution context from the service provider.
            IPluginExecutionContext context = (IPluginExecutionContext)
                serviceProvider.GetService(typeof(IPluginExecutionContext));

            IOrganizationServiceFactory serviceFactory =
               (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            if (context.InputParameters.Contains("Target") &&
               context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                if (entity.LogicalName == "new_order")
                {
                    tracingService.Trace("-----------------updating order-----------------");
                    CRMUtility utility = new CRMUtility(service, s => tracingService.Trace(s));
                    Entity configEntity = utility.RetriveEntity("new_authorizenetoption","new_name","default","new_option");
                    string config = configEntity["new_option"] as string;
                    tracingService.Trace(config);
                    Money m = entity["new_amount"] as Money;
                    entity["new_token"] = AuthorizeConnector.getToken(
                        AuthorizeNetOption.DefaultOption(),
                        m.Value.ToString(),
                        s => tracingService.Trace(s)
                        );
                }
            }
        }
    }
}
