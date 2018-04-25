namespace Kiki.WebApp
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration["ConnectionStrings:Sqlite"]));
            services.AddLogging();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/");
                    options.Conventions.AllowAnonymousToPage("/Account/Login");
                    options.Conventions.AllowAnonymousToFolder("/Account/");
                    //}).AddRazorPagesOptions(options =>
                    //{
                    //    options.Conventions.AddPageRoute("/Products/Index", "");
                });

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddTransient<ApplicationDbContextSeed>();
            services.AddTransient<IPriceCalculatorService, PriceCalculatorService>();
            services.AddTransient<IExcelReaderService, ExcelReaderService>();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContextSeed contextSeed)
        {
            await contextSeed.SeedAsync().ConfigureAwait(false);
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}