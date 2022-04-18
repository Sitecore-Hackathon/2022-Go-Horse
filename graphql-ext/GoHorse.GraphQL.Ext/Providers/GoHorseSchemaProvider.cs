using System;
using System.Collections.Generic;
using GraphQL.Types;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Data.Items;
using Spe.Core.Host;
using System.Linq;

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
                queryArgument.Description = "The script ID to run.";
                queryArgumentArray[0] = (QueryArgument)queryArgument;
                ((FieldType)this).Arguments = new QueryArguments(queryArgumentArray);
            }

            protected override IEnumerable<string> Resolve(ResolveFieldContext context)
            {
                var id = context.GetArgument<string>("id");
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
                                scriptSession.ExecuteScriptPart(script, true);
                            }
                        }

                        Sitecore.Context.Database = contextDb;

                        var ret = new List<string>();
                        ret = scriptSession.Output.Select(p => p.Text[p.Text.Length-1]=='\n' ? p.Text.Substring(0, p.Text.Length-1) : p.Text).ToList();
                        if (!ret.Any())
                            ret.Add("Ok!");
                        return ret;
                    }
                }
                catch (Exception error)
                {
                    var errorList = new List<string>
                    {
                        "ERROR: " + error.Message
                    };
                    return errorList;
                }
            }
        }
    }
}
