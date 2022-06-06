using System;
using AspNetCoreIdentityVS19.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AspNetCoreIdentityVS19.Areas.Identity.IdentityHostingStartup))]
namespace AspNetCoreIdentityVS19.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AspNetCoreIdentityVS19Context>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AspNetCoreIdentityVS19ContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<AspNetCoreIdentityVS19Context>();
            });
        }
    }
}