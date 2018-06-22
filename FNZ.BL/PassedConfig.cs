using System;
using System.Collections.Generic;
using System.Text;
using FNZ.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FNZ.BL
{
    public static class PassedConfig
    {
        public static void Config(IConfiguration configuration, IServiceCollection services)
        {
            var config = new StartUpConfig(configuration);
            config.PartOfConfigureServices(services);
        }

        private static void AutoMapperConfiguration()
        {

        }
    }
}
