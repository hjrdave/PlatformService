using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {

        public static void PrepPopulation(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        private static void SeedData(AppDbContext context)
        {
            if (!context.platforms.Any())
            {
                Console.WriteLine("....seeding data");
                context.platforms.AddRange(
                 new platform { Name = "Dot Net", Publisher = "Microsoft", cost = "free" },
                 new platform { Name = "Sql Server Express", Publisher = "Microsoft", cost = "free" },
                 new platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", cost = "free" }
                );
                context.SaveChanges();

            }
            else
            {
                Console.WriteLine("....we already have data!");
            }

        }
    }
}