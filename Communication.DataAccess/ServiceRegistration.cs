using Communication.DataAccess.Abstractions.Repositories.Company.Call;
using Communication.DataAccess.Abstractions.Repositories.Company.Group;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUser;
using Communication.DataAccess.Abstractions.Repositories.Company.GroupUserMessage;
using Communication.DataAccess.Abstractions.Repositories.Company.RequestToJoin;
using Communication.DataAccess.Abstractions.Repositories.File.CallFile;
using Communication.DataAccess.Abstractions.Repositories.File.PostFile;
using Communication.DataAccess.Abstractions.Repositories.User.UserPosts;
using Communication.DataAccess.Concretes.Repositories.Company.Call;
using Communication.DataAccess.Concretes.Repositories.Company.Group;
using Communication.DataAccess.Concretes.Repositories.Company.GroupUser;
using Communication.DataAccess.Concretes.Repositories.Company.GroupUserMessage;
using Communication.DataAccess.Concretes.Repositories.Company.RequestToJoin;
using Communication.DataAccess.Concretes.Repositories.File.CallFile;
using Communication.DataAccess.Concretes.Repositories.File.PostFile;
using Communication.DataAccess.Concretes.Repositories.User.UserPosts;
using Communication.DataAccess.Contexts;
using Communication.Entity.Models.User.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.DataAccess
{
    public static class ServiceRegistration
    {
        public static void addDataAccessServices(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<CommunicationAppDBContext>(options => options.UseNpgsql(connectionString));
            
            services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<CommunicationAppDBContext>();

            services.ConfigureApplicationCookie(_ =>
            {
                _.LoginPath = new PathString("/Identity/Account/LogIn");
                _.Cookie = new CookieBuilder
                {
                    Name = "accessToken",
                    HttpOnly = false,
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.Always,
                };
                _.SlidingExpiration = true;
                _.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.AddScoped<IGroupReadRepository,GroupReadRepository>();
            services.AddScoped<IGroupWriteRepository, GroupWriteRepository>();

            services.AddScoped<IGroupUserReadRepository, GroupUserReadRepository>();
            services.AddScoped<IGroupUserWriteRepository, GroupUserWriteRepository>();

            services.AddScoped<IPostFileReadRepository,PostFileReadRepository>();
            services.AddScoped<IPostFileWriteRepository,PostFileWriteRepository>();

            services.AddScoped<ICallFileReadRepository, CallFileReadRepository>();
            services.AddScoped<ICallFileWriteRepository, CallFileWriteRepository>();

            services.AddScoped<ICallReadRepository, CallReadRepository>();
            services.AddScoped<ICallWriteRepository, CallWriteRepository>();

            services.AddScoped<IUserPostWriteRepository,UserPostWriteRepository>();
            services.AddScoped<IUserPostReadRepository,UserPostReadRepository>();

            services.AddScoped<IRequestToJoinGroupReadRepository, RequestToJoinGroupReadRepository>();
            services.AddScoped<IRequestToJoinGroupWriteRepository, RequestToJoinGroupWriteRepository>();

            services.AddScoped<IGroupUserMessageReadRepository, GroupUserMessageReadRepository>();
            services.AddScoped<IGroupUserMessageWriteRepository, GroupUserMessageWriteRepository>();
        }
    }
}
