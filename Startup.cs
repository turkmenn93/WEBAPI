using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyWEBAPI.Data;
using UdemyWEBAPI.Interfaces;
using UdemyWEBAPI.Repositories;

namespace UdemyWEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = "http:localhost:5000",
                    ValidAudience = "http:localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Abdullahabdullah")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                };
            });

            services.AddDbContext<ProductContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("Local"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();    //---> bunun anlamý IProductRepository'i gordugun zaman ProductRepository ornegini bana ver
                                                                            // demis oluyoruz (DI)

            services.AddScoped<IDummyRepository, DummyRepository>();        //IDummyRepository'i gordukten sonra DummyRepository'i ornekle demis oluyoruz.


            services.AddCors(cors =>
            {
                cors.AddPolicy("UdemyCorsPolicy", opt =>
                {
                    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();   // herhangi bir originden gelen istegi kabul edecek anlamýna gelir.
                    //opt.WithOrigins("asdlmasdas.com")   seklinde bir kullaným yaparsam sadece burdan gelecek istekleri kabul ederim anlamýna gelir..

                });
            });


            services.AddControllers().AddNewtonsoftJson(opt=>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UdemyWEBAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UdemyWEBAPI v1"));
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("UdemyCorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
