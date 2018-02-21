
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
namespace Plug_Ins
{
    public class CalculatePricePlugin : IPlugin
    {
        const string apiLoginId = "5KP3u95bQpv";
        const string transactionKey = "346HZ32z3fP4hTG2";
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
                    entity["new_token"] = AuthorizeConnector.getToken(
                        s => tracingService.Trace(s),
                        apiLoginId,
                        transactionKey,
                        entity["new_amount"].ToString()
                        );
                    /*
                    try
                    {
                        entity["EntityState"] = null;
                        service.Update(entity);
                    }catch(System.Exception ex)
                    {
                        tracingService.Trace(ex.ToString());
                    }*/
                }
            }
        }
    }
}
