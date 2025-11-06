namespace ContactBook.Client.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddClientServices(this IServiceCollection services)
    {
        // Register client services here
        services.AddScoped<Services.PhoneService>();
        services.AddScoped<Services.UserService>();

        return services;
    }
}