using System;
using System.Collections.Generic;
using System.Web;
using GraphQL.Resolvers;
using GraphQL.Types;
using Sitecore;
using Sitecore.Common;
using Sitecore.Security.Accounts;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Data.Items;
using Spe.Core.Host;

namespace GoHorse.GraphQL.Ext.Providers
{
    public class GoHorseSchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new GoHorseQuery();
        }

        protected class GoHorseQuery : RootFieldType<StringGraphType, IEnumerable<string>>
        {
            public GoHorseQuery() : base(name: "runCommand", description: "Runs a specific command")
            {
                QueryArgument[] queryArgumentArray = new QueryArgument[1];
                QueryArgument<StringGraphType> queryArgument = new QueryArgument<StringGraphType>();
                queryArgument.Name = "id";
                queryArgument.Description = "The command ID to run.";
                queryArgumentArray[0] = (QueryArgument)queryArgument;
                ((FieldType)this).Arguments = new QueryArguments(queryArgumentArray);
            }

            protected override IEnumerable<string> Resolve(ResolveFieldContext context)
            {
                string id = context.GetArgument<string>("id");

                try
                {
                    // Run script
                    using (ScriptSession scriptSession = ScriptSessionManager.NewSession("Default", true))
                    {
                        var contextDb = Sitecore.Context.Database;
                        Sitecore.Context.Database = Sitecore.Configuration.Factory.GetDatabase("master");

                        Item speScriptItem = Sitecore.Context.Database.GetItem(id);
                        if (speScriptItem != null){
                            string script = speScriptItem["Script"];
                            if (!string.IsNullOrEmpty(script))
                            {
                                scriptSession.ExecuteScriptPart(script);
                            }
                        }

                        Sitecore.Context.Database = contextDb;
                    }


                }
                catch (Exception error)
                {
                    var errorList = new List<string>
                    {
                        "false : " + error.Message
                    };
                    return errorList;
                }

                // this is the object the resolver maps onto the graph type
                // (see UserGraphType below). This is your own domain object, not GraphQL-specific.
                var ret = new List<string>
                {
                    "true"
                };
                return ret;
                
            }
        }
    }
}
