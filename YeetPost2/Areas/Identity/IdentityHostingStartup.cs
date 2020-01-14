using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YeetPost2.Models;

[assembly: HostingStartup(typeof(YeetPost2.Areas.Identity.IdentityHostingStartup))]
namespace YeetPost2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AccountContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AccountContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>()
                    //.AddEntityFrameworkStores<AccountContext>();
            });
        }
    }
}