using KissLog;
using KissLog.Apis.v1.Listeners;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreIdentityVS19.Config
{
    public class LogConfig
    {
        public void RegisterKissLogListeners(IConfiguration configuration)
        {
            KissLogConfiguration.Listeners.Add(new KissLogApiListener(
                organizationId: configuration["KissLog.OrganizationId"],
                applicationId: configuration["KissLog.ApllicationId"],
                environment: configuration["KissLog.Enviroment"]
                ));
        }
    }
}
