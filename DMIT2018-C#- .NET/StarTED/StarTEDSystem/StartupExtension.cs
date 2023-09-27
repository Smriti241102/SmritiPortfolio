using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RegisterSkillsSystem.DAL;
using RegisterSkillsSystem.BLL;

namespace StarTEDSystem
{
    public static class StartupExtension
    {
        public static void AddDependencies(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContext<WorkScheduleContext>(options);

            services.AddTransient<Services>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<WorkScheduleContext>();

                return new Services(context);
            }
            );


        }

    }
}
