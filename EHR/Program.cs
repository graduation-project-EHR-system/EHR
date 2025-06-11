
using EHR.Core;
using EHR.Core.ServicesContract;
using EHR.Service.Services;
using MedicalRecords.Service.Core.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EHR
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EHRdbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });



            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                       ValidAudience = builder.Configuration["JWT:ValidAudience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });


            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);





           


            var app = builder.Build();
            using var scope = app.Services.CreateScope(); /// instead of using try finally to dispose the scope
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<EHRdbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ILoggerFactory>();
                logger.LogError(ex, "There is Error in Migration");
            }

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
      //      }

            // app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
