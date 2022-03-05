using GraphQL.Resolvers;
using GraphQL.Types;
using Sitecore.Data.Fields;
using Sitecore.Services.GraphQL.Schemas;
using FieldType = GraphQL.Types.FieldType;

namespace GoHorse.GraphQL
{
    public class GoHorseExtender : SchemaExtender
    {
        /// <summary>
        /// This is a simple example of the capabilities of an extender. It's designed to show the right way to do some common needs.
        /// </summary>
        public GoHorseExtender()
        {
            // Extend the 'Appearance' graph type
            /*
            ExtendType("Query", type =>
            {
                type.Description = "Modified by extender!";
            });
            */

            // Extend the 'Appearance' graph type, assuming that it is also a derivative of IComplexGraphType
            // useful because IComplexGraphType is the first type that brings Fields into the type (e.g. not a scalar)
            ExtendType<IComplexGraphType>("Appearance", type =>
            {
                // Extend every string field on the type and hack its description
                ExtendField<StringGraphType>(type, field =>
                {
                    field.Description = "I got hacked by an extender!";
                });

                // Extend a field by name and tweak its description
                ExtendField(type, "contextMenu", field =>
                {
                    field.Description = "Yoink! Gotcher description!";
                });
            });

            // extends any type which defines a mapping for the Field backend type
            // (e.g. all things that represent template fields)
            /*
            ExtendTypes<ObjectGraphType<Field>>(type =>
            {
                // add a new field to the field object type
                // note the resolve method's Source property is the Field so you can get at its data
                type.Field<StringGraphType>("bar",
                    description: "Field added to all fields by an extender",
                    resolve: context => "I'm adding this string to the display name: " + context.Source.DisplayName);
            });
            */

            // Extends three named types and adds a 'foo' field to them
            /*
            ExtendTypes<IComplexGraphType>(new[] { "ItemLanguage", "ItemWorkflow", "ItemWorkflowState" }, type =>
            {
                // add a "foo" field that returns "foo, bar, bas" to every complex type in the schema
                // note: using a more specific generic than IComplexGraphType (e.g. ObjectGraphType<T>) may provide
                // superior options when adding fields like the Field<T> method
                type.AddField(new FieldType
                {
                    Name = "foo",
                    Description = "A field passed in from an extender",
                    Resolver = new FuncFieldResolver<string>(context => "foo, bar, bas"),
                    Type = typeof(StringGraphType)
                });
            });
            */

            ExtendTypes(type =>
            {
                // this will be called for _every_ type in the whole schema
            });

            // You can also add graph types, for example, to add complex data as a new field.
            // This type is added, as opposed to being used. It will appear in the schema
            // but cannot be queried because it's not attached to any other node in the graph
            // (for example, as a root query or as a property on another graph type)
            AddType(() => new FooGraphType());
        }

        protected class FooGraphType : InterfaceGraphType
        {
            public FooGraphType()
            {
                Name = "Foo";
                Field<StringGraphType>("bar");
            }
        }
    }
}
