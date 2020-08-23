using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using EmployeeManagement.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication;
using EmployeeManagement.Contracts.Interfaces;
using EmployeeManagement.Contracts.Models.Entities;
using EmployeeManagement.DataAccess.Repository;
using EmployeeManagement.DataAccess.DBContext;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.BusinessLogic.Services;

namespace EmployeeManagement
{
    public class Startup
    {
        public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
        {
            public const string DefaultScheme = "API Key";
            public string Scheme => DefaultScheme;
        }

        internal class ApiKeyOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var filter = new SecurityRequirementsOperationFilter(securitySchemaName: ApiKeyAuthenticationOptions.DefaultScheme);
                filter.Apply(operation, context);
            }
        }
        public class CustomHeaderSwaggerAttribute : IOperationFilter
        {

            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "String"
                    }
                });
            }

        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
            services.AddScoped<IRepository<Employee>, EmployeeManagementRepository>();
            services.AddDbContext<EmployeeManagementDBContext>(c =>
                        c.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            ConfigureSwagger(services);
        }
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagement API", Version = "v1" });
                x.AddSecurityDefinition(ApiKeyAuthenticationOptions.DefaultScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                x.OperationFilter<ApiKeyOperationFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerConfig = new SwaggerConfig();
            Configuration.GetSection(nameof(SwaggerConfig)).Bind(swaggerConfig);

            app.UseSwagger(config =>
            {
                config.RouteTemplate = swaggerConfig.JsonRoute;
            });

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(swaggerConfig.UIEndpoint, swaggerConfig.Description);
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCors(builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
        }
    }
}
