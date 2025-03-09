using Microsoft.Extensions.DependencyInjection;
using OldPhonePad.Application.Interfaces;
using OldPhonePad.Application.Services;
using OldPhonePad.Domain.Entities;
using OldPhonePad.Infrastructure.Data;

namespace OldPhonePad.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register the PhonePadDecoder with its dependencies
            services.AddSingleton<PhonePadDecoder>(sp =>
                new PhonePadDecoder(KeyPadData.KeyMap));

            // Register the application service
            services.AddScoped<IPhonePadService, PhonePadService>();

            return services;
        }
    }
}
