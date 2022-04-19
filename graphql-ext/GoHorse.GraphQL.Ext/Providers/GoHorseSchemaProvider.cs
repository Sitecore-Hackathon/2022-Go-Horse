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
            yield return new RunScriptIdQuery();
            yield return new RunScriptInlineQuery();
        }

        protected class RunScriptIdQuery : RootFieldType<StringGraphType, IEnumerable<string>>
        {
            public RunScriptIdQuery() : base(name: "runScriptId", description: "Runs a specific command")
            {
                var queryArgumentArray = new QueryArgument[2];
                queryArgumentArray[0] = new QueryArgument<StringGraphType>
                {
                    Name = "id",
                    Description = "The script ID to run."
                };
                queryArgumentArray[1] = new QueryArgument<StringGraphType>
                {
                    Name = "sessionId",
                    Description = "The Session ID to keep the context."
                };
                ((FieldType)this).Arguments = new QueryArguments(queryArgumentArray);
            }

            protected override IEnumerable<string> Resolve(ResolveFieldContext context)
            {
                var id = context.GetArgument<string>("id");
                var sessionId = context.GetArgument<string>("sessionId");
                try
                {
                    // Run script
                    var ret = RunPowershellScriptById(id, sessionId);
                    return ret;
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

        protected class RunScriptInlineQuery : RootFieldType<StringGraphType, IEnumerable<string>>
        {
            public RunScriptInlineQuery() : base(name: "runScriptInline", description: "Runs inline powershell script")
            {
                var queryArgumentArray = new QueryArgument[2];
                queryArgumentArray[0] = new QueryArgument<StringGraphType>
                {
                    Name = "sessionId",
                    Description = "The Session ID to keep the context."
                };
                queryArgumentArray[1] = new QueryArgument<StringGraphType>
                {
                    Name = "script",
                    Description = "The inline script to run."
                };                
                ((FieldType)this).Arguments = new QueryArguments(queryArgumentArray);
            }

            protected override IEnumerable<string> Resolve(ResolveFieldContext context)
            {
                var script = context.GetArgument<string>("script");
                var sessionId = context.GetArgument<string>("sessionId");
                try
                {
                    // Run script
                    var ret = RunPowershellScript(script, sessionId);
                    return ret;
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

        public static List<string> RunPowershellScriptById(string id, string sessionId)
        {
            // Get script from scriptId
            var script = string.Empty;
            var contextDb = Sitecore.Context.Database;
            Sitecore.Context.Database = Sitecore.Configuration.Factory.GetDatabase("master");
            Item speScriptItem = Sitecore.Context.Database.GetItem(id);
            if (speScriptItem != null)
                script = speScriptItem["Script"];
            Sitecore.Context.Database = contextDb;

            // Execute the script
            if (string.IsNullOrEmpty(script))
                return new List<string>();
            return RunPowershellScript(script, sessionId);
        }

        public static List<string> RunPowershellScript(string script, string sessionId)
        {
            // Get (or create) session
            var scriptSession = ScriptSessionManager.GetAll().FirstOrDefault(p => p.ID == sessionId);
            if (scriptSession == null)
                scriptSession = ScriptSessionManager.GetSession(sessionId, "Default", true);
            else
                scriptSession.Output.Clear();

            // Run script
            var contextDb = Sitecore.Context.Database;
            Sitecore.Context.Database = Sitecore.Configuration.Factory.GetDatabase("master");
            if (!string.IsNullOrEmpty(script))
                scriptSession.ExecuteScriptPart(script, true);
            Sitecore.Context.Database = contextDb;

            // Format output
            var ret = new List<string>();
            ret = scriptSession.Output.Select(p => p.Text[p.Text.Length - 1] == '\n' ? p.Text.Substring(0, p.Text.Length - 1) : p.Text).ToList();
            if (!ret.Any())
                ret.Add("Ok!");
            return ret;
        }
    }
}
