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
    /// <summary>
    /// Sample of making your own schema provider
    /// This sample enables you to query on the current context user
    /// </summary>
    public class WhoAmISchemaProvider : SchemaProviderBase
    {
        public override IEnumerable<FieldType> CreateRootQueries()
        {
            yield return new WhoAmIQuery();
        }

        /// <summary>
        /// Teaches GraphQL how to resolve the `whoAmI` root field.
        ///
        /// RootFieldType<UserGraphType, User> means this root field maps a `User` domain object into the `UserGraphType` graph type object.
        /// </summary>
        protected class WhoAmIQuery : RootFieldType<StringGraphType, IEnumerable<string>>
        {
            public WhoAmIQuery() : base(name: "whoAmI", description: "Gets the current user")
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

        // because this graph type is referred to by the return type in the FieldType above, it is automatically
        // registered with the schema. For implied types (e.g. interface implementations) you need to override CreateGraphTypes() and
        // manually tell the schema they exist (because no graph type directly refers to those types)
        protected class UserGraphType : ObjectGraphType<string>
        {
            public UserGraphType()
            {
                // graph type names must be unique within a schema, so if defining a multiple-schema-provider
                // endpoint, ensure that you don't have name collisions between schema providers.
                Name = "GoHorseReturn";
                Field<NonNullGraphType<StringGraphType>>("name", resolve: context => context);
                //Field<NonNullGraphType<BooleanGraphType>>("isAdministrator", resolve: context => context.Source.IsAdministrator);

                // note that graph types can resolve other graph types; for example
                // it would be possible to add a `lockedItems` field here that would
                // resolve to an `Item[]` and map it onto `ListGraphType<ItemInterfaceGraphType>`
            }
        }
    }
}
