using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M1MartDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace M1MartDataAccess
{
    public class Dependencies
    {
        public static void ConfigureService(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<M1MartV2Context>(
                option => option.UseSqlServer(configuration.GetConnectionString("M1MartConnection"))
            );
        }
    }
}
