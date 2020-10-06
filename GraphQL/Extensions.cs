using FluentValidation;
using GraphQL.Models;
using HotChocolate;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.Queries;
using GraphQL.Mutations;
using GraphQL.Subscriptions;
using GraphQL.Models.Scalars;

namespace GraphQL
{
    public static class Extension
    {
        /// <summary>
        /// Hooks up GraphQL.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Extension).Assembly);

            services.AddInMemorySubscriptions();

            services.AddGraphQL(SchemaBuilder.New()
                .AddType<AppReleaseResultType>()
                .AddType<AppReleaseType>()
                .AddType<AppReleaseCollectionType>()
                .AddType<ErrorType>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<AppReleaseSubscriptions>()
                .BindClrType<byte[], ByteArrayType>()
                .Create());

            return services;
        }

        /// <summary>
        /// Enables GraphQL for use.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomGraphQL(this IApplicationBuilder app) => app.UseGraphQL().UsePlayground();
    }
}