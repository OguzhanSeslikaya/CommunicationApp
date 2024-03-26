using Communication.Business.Abstractions.ClientServices.GroupServices;
using Communication.Business.Abstractions.Services.GroupServices;
using Communication.Business.Abstractions.Services.UserServices;
using Communication.Business.Abstractions.StorageServices;
using Communication.Business.Concretes.ClientServices.GroupServices;
using Communication.Business.Concretes.Services.GroupServices;
using Communication.Business.Concretes.Services.UserServices;
using Communication.Business.Concretes.StorageServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Business
{
    public static class ServiceRegistration
    {
        public static void addBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseGroupService, BaseGroupService>();
            services.AddScoped<IHomeGroupService, HomeGroupService>();
            services.AddScoped<IGroupSettingsService, GroupSettingsService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IStorageService, StorageService>();
            
        }
        public static void addStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
