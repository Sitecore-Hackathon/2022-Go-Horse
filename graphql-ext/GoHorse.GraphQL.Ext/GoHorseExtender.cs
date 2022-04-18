using Sitecore.Services.GraphQL.Schemas;

namespace GoHorse.GraphQL
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
