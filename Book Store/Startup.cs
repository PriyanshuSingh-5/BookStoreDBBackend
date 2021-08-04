using System;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.AdminInterfaces;
using BusinessLayer.AdminServices;
using BusinessLayer.CustomerIntrfaces;
using BusinessLayer.CustomerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.AdminInterfaces;
using RepositoryLayer.AdminServices;
using RepositoryLayer.CustomerServices;
using RepositoryLayer.CutomerInterfaces;

namespace Book_Store
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration["ConnectionStrings:BookStoreConnection"];
        }

        public IConfiguration Configuration { get; }
        public string ConnectionString { get; private set; } 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerAccountBL, CustomerAccountBL>();
            services.AddScoped<ICustomerAccountRL, CustomerAccountRL>();
            services.AddScoped<IAdminAccountBL, AdminAccountBL>();
            services.AddScoped<IAdminAccountRL, AdminAccountRL>();
            services.AddScoped<ICustomerAddressBL, CustomerAddressBL>();
            services.AddScoped<ICustomerAddressRL, CustomerAddressRL>();
            services.AddScoped<IBookManagementBL, BookManagementBL>();
            services.AddScoped<IBookManagementRL, BookManagementRL>();
            services.AddScoped<ICustomerCartBL, CustomerCartBL>();
            services.AddScoped<ICustomerCartRL, CustomerCartRL>();
            services.AddScoped<ICustomerWishListBL, CustomerWishListBL>();
            services.AddScoped<ICustomerWishListRL, CustomerWishListRL>();
            services.AddScoped<ICustomerOrderBL, CustomerOrderBL>();
            services.AddScoped<ICustomerOrderRL, CustomerOrderRL>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireCustomerRole",
                     policy => policy.RequireRole("Customer"));
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        context.Response.Headers.Add("Token-Validated", "true");
                        return Task.CompletedTask;
                    },
                };
            });
            services.AddControllers();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Book Store API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization \r\n\r\n Enter 'Bearer' [space] and token.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book Store");
            });

            ConnectionString = Configuration["ConnectionStrings:BookStoreConnection"];
        }
    }
}
