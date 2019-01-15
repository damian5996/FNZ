using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FNZ.Data;
using FNZ.Data.Repository;
using FNZ.Data.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace FNZ.BL
{
    public static class PassedConfig
    {
        public static void Config(IConfiguration configuration, IServiceCollection services)
        {
            var config = new StartUpConfig(configuration);
            config.PartOfConfigureServices(services);
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
            services.AddTransient<IAdoptionRepository, AdoptionRepository>();
        }
    }
}
