using Microsoft.EntityFrameworkCore;
using Web_MZ.Data;
using Web_MZ.Entities;
using Web_MZ.Repository;
using Web_MZ.Services;
using QuestPDF.Infrastructure;


namespace Web_MZ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Data connect
            builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioDbConnection")));

            // injection des services
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<ICreatorService, CreatorService>();
            builder.Services.AddScoped<ILangueService, LangueService>();
            builder.Services.AddScoped<ICertificationService, CertificationService>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<IExperienceService, ExperienceService>();
            builder.Services.AddScoped<ICompetenceService, CompetenceService>();
            builder.Services.AddScoped<IModifCreatorService, ModifCreatorService>();
            builder.Services.AddScoped<IRecruiterService, RecruiterService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            // injection rep
            builder.Services.AddScoped<ICreatorRepository, CreatorRepository>();
            builder.Services.AddScoped<ILangueRepository, LangueRepository>();
            builder.Services.AddScoped<ICertificationRepository, CertificationRepository>();
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>(); 
            builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
            builder.Services.AddScoped<ICompetenceRepository, CompetenceRepository>();
            builder.Services.AddScoped<IModifCreatorRepository, ModifCreatorRepository>();
            builder.Services.AddScoped<IRecruiterRepository, RecruiterRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
       


            QuestPDF.Settings.License = LicenseType.Community;

            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseSession();

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
