using GraphQL.Resolvers;
using GraphQL.Types;
using Sitecore.Data.Fields;
using Sitecore.Services.GraphQL.Schemas;
using FieldType = GraphQL.Types.FieldType;

namespace GoHorse.GraphQL
{
    public class GoHorseExtender : SchemaExtender
    {
        public GoHorseExtender()
        {
            // Extend the 'Appearance' graph type
            ExtendType("Query", type =>
            {
                type.Description = "Go Horse extender";
            });
        }
    }
}
