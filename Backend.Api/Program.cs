
using Backend.Api.DAL;
using Backend.Api.Repositories.Implementations;
using Backend.Api.Repositories.Interface;
using Backend.Api.Services.Implementations;
using Backend.Api.Services.Interface;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            });
            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IGameKeyService, GameKeyService>();
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IGameKeyRepository, GameKeyRepository>();

            builder.Services.AddAutoMapper(typeof(Program));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Security"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(); 


            app.UseHttpsRedirection();
            app.UseCors("AllowFrontend");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        //public void AddBusinessService(IServiceCollection services)
        //{
        //    services.AddControllers();
        //    services.AddScoped<IFeatureService, FeatureService>();
        //    services.AddScoped<IGenreService, GenreService>();
        //    services.AddScoped<IGameKeyService, GameKeyService>();
        //    services.AddScoped<IGameService, GameService>();

        //    services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        //}

        //public void AddDALService(IServiceCollection services)
        //{
        //    services.AddControllers();
        //    services.AddScoped<IFeaturesRepository, FeaturesRepository>();
        //    services.AddScoped<IGenreRepository, GenreRepository>();
        //    services.AddScoped<IGameRepository, GameRepository>();
        //    services.AddScoped<IGameKeyRepository, GameKeyRepository>();


        //}
    }


}
