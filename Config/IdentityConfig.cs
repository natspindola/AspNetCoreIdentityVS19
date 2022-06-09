using AspNetCoreIdentityVS19.Areas.Identity.Data;
using AspNetCoreIdentityVS19.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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


        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AspNetCoreIdentityVS19Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AspNetCoreIdentityVS19ContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AspNetCoreIdentityVS19Context>();

            return services;
        }
    }
}
