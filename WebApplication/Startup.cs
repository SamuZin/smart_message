using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApplication
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
#pragma warning disable CS8602 // Dereference of a possibly null reference.

        string version = string.Format("v{0}.{1}.{2}",
            Assembly.GetEntryAssembly().GetName().Version.Major.ToString(),
            Assembly.GetEntryAssembly().GetName().Version.Minor.ToString(),
            Assembly.GetEntryAssembly().GetName().Version.Build.ToString());

#pragma warning restore CS8602

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            // services.AddScoped<IExampleUseCase,ExampleUseCase>();

            services.AddCors(options =>
            {
                options.AddPolicy("PolicyAllowAll", policy =>
                {
                    policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Smart Message API's",
                    Version = version,
                });

#pragma warning disable CS8602 // Dereference of a possibly null reference.

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml"; // smart_message.xml
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

#pragma warning restore CS8602

                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer sheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorizarion",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                        return [api.GroupName];

                    var conntrollorActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (conntrollorActionDescriptor != null)
                        return [conntrollorActionDescriptor.ControllerName];

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                c.DocInclusionPredicate((name, api) => true);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
#if DEBUG
            Console.WriteLine("Mode=Debug");
#else
            Console.WriteLine("Mode=Release");
#endif
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelExpandDepth(-1);
                c.SwaggerEndpoint("v1/swagger.json", "Smart Message API's " + version);
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("PolicyAllowAll");
            app.UseAuthentication();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();

                endpoints.MapSwagger("/swagger/{documentName}/swagger.json", options => {
                    options.PreSerializeFilters.Add((swagger, httpRequest) => { });
                });
            });
        }
    }
}
