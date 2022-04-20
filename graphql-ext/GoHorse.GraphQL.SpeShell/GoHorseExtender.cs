using Sitecore.Services.GraphQL.Schemas;

namespace GoHorse.GraphQL.Ext
{
    public class GoHorseExtender : SchemaExtender
    {
        public GoHorseExtender()
        {
            ExtendType("Query", type =>
            {
                type.Description = "Go Horse extender";
            });
        }
    }
}
