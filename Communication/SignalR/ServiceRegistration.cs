using Microsoft.Extensions.DependencyInjection;
using SignalR.Abstractions;
using SignalR.HubServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR
{
    public static class ServiceRegistration
    {
       public static void addSignalRServices(this IServiceCollection services)
        {
            //services.AddTransient<IUserMessageHubService, UserMessageHubService>();
            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 102400000; // Örnek: 100 MB
            });
        }
    }
}
