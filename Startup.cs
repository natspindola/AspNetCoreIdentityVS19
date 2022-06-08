using AspNetCoreIdentityVS19.Areas.Identity.Data;
using AspNetCoreIdentityVS19.Config;
using AspNetCoreIdentityVS19.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreIdentityVS19
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AspNetCoreIdentityVS19Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AspNetCoreIdentityVS19ContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AspNetCoreIdentityVS19Context>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "PodeExcluir", configurePolicy: policy => policy.RequireClaim("PodeExcluir"));

                options.AddPolicy(name: "PodeLer", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeLer")));
                options.AddPolicy(name: "PodeEscrever", configurePolicy: policy => policy.Requirements.Add(new PermissaoNecessaria(permissao: "PodeEscrever")));
            });

            services.ResolveDependencies();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
