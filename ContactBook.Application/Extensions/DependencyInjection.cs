using ContactBook.Application.Interfaces.Services;
using ContactBook.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBook.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPhoneService, PhoneService>();
        
        return services;
    }
}