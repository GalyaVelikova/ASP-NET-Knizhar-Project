namespace Knizhar
{
    using Knizhar.Areas.Admin.Services;
    using Knizhar.Controllers;
    using Knizhar.Data;
    using Knizhar.Data.Models;
    using Knizhar.Infrastructure.Extensions;
    using Knizhar.Services;
    using Knizhar.Services.Books;
    using Knizhar.Services.Knizhari;
    using Knizhar.Services.Messages;
    using Knizhar.Services.Statistics;
    using Knizhar.Services.Votes;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<KnizharDbContext>(options => options
                    .UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<KnizharDbContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddSignalR();
            services.AddControllers()
                        .AddNewtonsoftJson();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });


            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IKnizharService, KnizharService>();
            services.AddTransient<IVoteService, VoteService>();
            services.AddTransient<ICategoriesService, CategoriesServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {

                    endpoints.MapDefaultAreaRoute();
                    endpoints.MapControllerRoute(
                        name: "Book Details",
                        pattern: "/Books/Details/{id}/{information}",
                        defaults: new 
                        { 
                            controller = typeof(BooksController).GetControllerName(), 
                            action = nameof(BooksController.Details) 
                        });
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                    endpoints.MapHub<MessageHub>("/message");
                });
        }
    }
}
