
using Communication.Entity.ViewModels.Groups;
using Communication.Entity.ViewModels.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.GroupValidations;
using Validation.UserValidations;

namespace Validation
{
    public static class ServiceRegistration
    {
        public static void addValidationServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegisterVM>, RegisterValidation>();
            services.AddScoped<IValidator<CreateGroupVM>, CreateGroupValidation>();
        }
    }
}
