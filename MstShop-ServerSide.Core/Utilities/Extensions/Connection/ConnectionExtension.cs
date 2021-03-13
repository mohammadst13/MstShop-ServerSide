using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MstShop_ServerSide.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace MstShop_ServerSide.Core.Utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddDbContext<MstShopDbContext>(options =>
            {
                var connectionString = "ConnectionStrings:MstShopConnection:Development";
                options.UseSqlServer(configuration[connectionString]);
            });

            return service;
        }
    }
}
