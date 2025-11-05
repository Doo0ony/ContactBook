using ContactBook.Application.Interfaces.Services;
using ContactBook.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ContactBook.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPhoneService, PhoneService>();

        return services;
    }
    
    public static IServiceCollection AddMappingProfiles(this IServiceCollection services, IConfiguration builder)
    {
        // Configure AutoMapper with License Key
        services.AddAutoMapper( cfg =>
            cfg.LicenseKey = builder.GetConnectionString("AutoMapper"));
            
        // Add AutoMapper profiles
        services.AddAutoMapper(cfg => { }, typeof(DependencyInjection).Assembly);

        return services;
    }
}