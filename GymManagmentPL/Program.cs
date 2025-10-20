using GymManagmentBLL;
using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentDAL.Data.DataSeed;
using GymManagmentDAL.Data.GymDBContext;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Classes;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<ISessionRepository , SessionRepository>();
            builder.Services.AddScoped<IMemberService , MemberService>();
            builder.Services.AddScoped<IAnalyticsService , AnayticsService>();
            builder.Services.AddScoped<IUnitOfWork , UnitOfWork>();

            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            var app = builder.Build();

            #region DataSeeding
            using var scope = app.Services.CreateScope();

            var gymDbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

            var pendingMigration = gymDbContext.Database.GetPendingMigrations();

            if(pendingMigration?.Any() ?? false)
            {
                gymDbContext.Database.Migrate();
            }
            GymDataSeeding.SeedDate(gymDbContext);

            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
