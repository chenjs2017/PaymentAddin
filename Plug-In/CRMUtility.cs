using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Reflection;

namespace CRMLib
{
    public  class CRMUtility
    {

        protected IOrganizationService CRMSerivce { get; set; }
        protected Action<string> Logger { get; set; }

        public CRMUtility(IOrganizationService service, Action<string> logger)
        {
            CRMSerivce = service;
            Logger = logger;
        }
        
        public Entity RetriveEntity(string entityName, string fieldName, string fieldValue, params string[] cols)
        {
            var t = new Tuple<string, object>(fieldName, fieldValue);
            return RetriveEntity(entityName, new Tuple<string, object>[] { t }, cols);
        }

        public Entity RetriveEntity(string entityName, Tuple<string, object>[] arrTuple, params string[] cols)
        {
            Entity entity = null;

            QueryExpression query = new QueryExpression
            {
                EntityName = entityName,
                ColumnSet = new ColumnSet(cols),

            };
            foreach (Tuple<string, object> tuple in arrTuple)
            {
                query.Criteria.AddCondition(new ConditionExpression
                {
                    AttributeName = tuple.Item1,
                    Operator = ConditionOperator.Equal,
                    Values = { tuple.Item2 }
                });
            }

            DataCollection<Entity> arr = CRMSerivce.RetrieveMultiple(query).Entities;
            Logger(String.Format("Retrieve from {0}: {1} == {2}", entityName, arrTuple[0].Item1, arrTuple[0].Item2));
            Logger(String.Format("Result count: {0}", arr.Count));

            if (arr.Count == 1)
            {
                entity = arr[0];
            }
            return entity;
        }
    }
}
