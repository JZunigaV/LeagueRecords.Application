using System;
using LeagueRecords.Application.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueRecords.Application.Core.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<ISummonerService, SummonerService>();
            services.AddHttpClient<ISummonerService, SummonerService>("riot", c =>
            {
                c.BaseAddress = new Uri("https://la1.api.riotgames.com/lol/");
            });

            return services;
        }

    }
}