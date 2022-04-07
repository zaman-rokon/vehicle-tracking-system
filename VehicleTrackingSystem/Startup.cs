using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using FluentValidation.AspNetCore;
using VehicleTrackingSystem.Application.Commands;
using VehicleTrackingSystem.Application.Helpers;
using VehicleTrackingSystem.Application.Managers;
using VehicleTrackingSystem.DataAccess.CosmosRepositories;
using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataAccess.DBModels;
using VehicleTrackingSystem.DataAccess.UnitOfWork;
using VehicleTrackingSystem.Validations;
using System.IO;
using System.Reflection;
using System;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace VehicleTrackingSystem
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
            services.Configure<QueryDbSettings>(Configuration.GetSection("QueryDbSettings"));
            services.AddSingleton(_ => new CosmosClient(Configuration["AzureConfiguration:CosmosDbConnectionString"]));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), opt =>
                opt.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            services.AddTransient<IVehicleTrackingUserManager, VehicleTrackingUserManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IVehicleLocationRepository, VehicleLocationRepository>();
            services.AddTransient<IVehicleManager, VehicleManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRolePolicy", policy => policy.RequireRole("Administrator"));
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Description = "Vehicle Tracking API with JWT and Swagger" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                    }
                });
            });

            // Add Authorization to all controller.
            services.AddControllers(option =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                option.Filters.Add(new AuthorizeFilter(policy));

            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<RegisterVehicleCommandValidator>();
            }).AddNewtonsoftJson();

            services.AddAuthorization(option =>
            {
                option.AddPolicy("AdministrationPolicy",
                    policy => policy.RequireRole("Administrator"));
            });

            services.AddMediatR(typeof(UserRegistrationCommand).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Tracking Web API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SeedVTSDB.InsertSeed(app);
        }
    }
}
