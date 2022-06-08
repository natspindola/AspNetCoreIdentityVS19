using AspNetCoreIdentityVS19.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityVS19.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "PodeExcluir", configurePolicy: policy => policy.RequireClaim("PodeExcluir"));

                options.AddPolicy(name: "PodeLer", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeLer")));
                options.AddPolicy(name: "PodeEscrever", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeEscrever")));
            });

            return services;
        }
    }
}
