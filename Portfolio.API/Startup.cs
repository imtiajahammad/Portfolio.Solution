using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Portfolio.API.Helpers;
using Portfolio.Common;
using Portfolio.Repository;
using Portfolio.Repository.Movies.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.API
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
            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            var connection = Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<PortfolioDbContext>(options => options.UseSqlServer(connection));
            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            //services.AddTransient<IUserMasterQueries, UserMasterQueries>();
            //services.AddTransient<IUserTokensQueries, UserTokensQueries>();
            services.AddTransient<IMoviesQueries, MoviesQueries>();
            services.AddTransient<IUnitOfWorkEntityFramework, UnitOfWorkEntityFramework>();
            services.AddTransient<IUnitOfWorkDapper, UnitOfWorkDapper>();



            #region jwt authentication-start
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["ApplicationSettings:Issuer"],
                       ValidAudience = Configuration["ApplicationSettings:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:SecretKey"]))
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
                       }
                   };
               });
            #endregion jwt authentication-end
            #region api versioning-start
            services.AddApiVersioning(x=> {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                x.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.HeaderApiVersionReader("x-frapper-api-version");
            });
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";
                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            #endregion api versioning-end
            #region Register the Swagger generator-start
            //services.AddSwaggerGen();
            services.AddSwaggerGen(
                (options)=> 
                {
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
                    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v2" });
                    #region jwt portion-start
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                         new string[] { }
                       }
                      });
                    #endregion jwt portion-end
                }
                );
                #endregion Register the Swagger generator-end
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILogger<Startup> logger)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();
            #region Enable middleware to serve generated Swagger as a JSON endpoint-start*/
            app.UseSwagger();
            #endregion Enable middleware to serve generated Swagger as a JSON endpoint-end
            #region specify the Swagger JSON endpoint-start
            app.UseSwaggerUI(c =>
            {
                c.DefaultModelsExpandDepth(-1);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API v2");
            });
            #endregion specify the Swagger JSON endpoint-end
            app.UseStaticFiles();

            #region Add our new middleware to the pipeline to validate request header
            app.UseMiddleware<ValidateHeaderMiddleware>();
            #endregion Add our new middleware to the pipeline to validate request header


            app.UseAuthentication();

            app.UseAuthorization();

            #region Add our new middleware to the pipeline to log request and response
            app.UseMiddleware<RequestResponseLoggingMiddleware>();
            #endregion Add our new middleware to the pipeline to log request and response
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
