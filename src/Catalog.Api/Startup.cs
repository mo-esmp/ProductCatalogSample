using Catalog.Api.Domain;
using Catalog.Api.Domain.ProductCatalog;
using Catalog.Api.Domain.Shared;
using Catalog.Api.Infrastructure;
using Catalog.Api.Infrastructure.Data;
using Catalog.Api.Infrastructure.ProductCatalog;
using Catalog.Api.Middlewares;
using Catalog.Api.Swagger;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Catalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddApiVersioning();
            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();
                    options.SchemaFilter<SwaggerExcludeFilter>();
                });

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrencyLookup, FixedCurrencyLookup>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IFileSaver, PhotoFileSaver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiExceptionHandling();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(options => { options.RouteTemplate = "docs/{documentName}/docs.json"; });
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "docs";
                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerEndpoint($"/docs/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
                });
        }
    }
}