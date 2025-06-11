
using EHR.Core;
using EHR.Core.ServicesContract;
using EHR.Service.Services;
using MedicalRecords.Service.Core.Helper;
using Microsoft.EntityFrameworkCore;

namespace EHR
{
    public class Program
    {
        public static void Main(string[] args)
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

            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
      //      }

            // app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
