using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Crochet.Infrastructure;
using Crochet.Infrastructure.Errors;
using Crochet.Infrastructure.Interfaces;
using Crochet.Infrastructure.Pipeline;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace Crochet
{
    public class Startup
    {
        public const string DEFAULT_DATABASE_CONNECTIONSTRING = "Filename=realworld.db";
        public const string DEFAULT_DATABASE_PROVIDER = "sqlite";

        private readonly IConfiguration _config;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false)
            .AddEnvironmentVariables();
            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DBContextTransactionPipelineBehavior<,>));

            //var connectionString = _config.GetConnectionString("ChrochetContext");
            // take the database provider from the environment variable or use hard-coded database provider
            //var databaseProvider = _config.GetValue<string>("ASPNETCORE_Conduit_DatabaseProvider");
            //if (string.IsNullOrWhiteSpace(databaseProvider))
            //    databaseProvider = DEFAULT_DATABASE_PROVIDER;

            services.AddDbContext<CrochetContext>(options =>
            {
                // only works in windows container
                var connectionString = _config.GetConnectionString("ChrochetContext");
                options.UseSqlServer(connectionString);

                //if (databaseProvider.ToLower().Trim().Equals("sqlite"))
                //    options.UseSqlite(connectionString);
                //else if (databaseProvider.ToLower().Trim().Equals("sqlserver"))
                //{
                //    // only works in windows container
                //    options.UseSqlServer(connectionString);
                //}
                //else
                //    throw new Exception("Database provider unknown. Please check configuration");
            });

            services.AddLocalization(x => x.ResourcesPath = "Resources");

            // Inject an implementation of ISwaggerProvider with defaulted settings applied
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }}
                });
                x.SwaggerDoc("v1", new Info { Title = "RealWorld API", Version = "v1" });
                x.CustomSchemaIds(y => y.FullName);
                x.DocInclusionPredicate((version, apiDescription) => true);
                x.TagActionsBy(y => new List<string>()
                {
                    y.GroupName
                });
            });

            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Conventions.Add(new GroupByApiRootConvention());
                opt.Filters.Add(typeof(ValidatorActionFilter));
            })
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddAutoMapper(GetType().Assembly);

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
            //services.AddScoped<IProfileReader, ProfileReader>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddJwt();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogLogging();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "RealWorld API V1");
            });

            app.ApplicationServices.GetRequiredService<CrochetContext>().Database.EnsureCreated();
        }
    }
}
