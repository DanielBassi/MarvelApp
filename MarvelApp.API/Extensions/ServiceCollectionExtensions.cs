﻿using FluentValidation;
using global::MarvelApp.Application;
using global::MarvelApp.Application.Queries.GetComics;
using MarvelApp.Application.Commands.CreateUser;
using MarvelApp.Application.Ports;
using MarvelApp.Domain.Ports;
using MarvelApp.Infrastructure.Adapters;
using MarvelApp.Infrastructure.Persistence;
using MarvelApp.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MarvelApp.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var allowedOrigins = new[] { "http://localhost:4200" };

            services.AddCors(options =>
            {
                options.AddPolicy("LocalCorsPolicy", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("default")));

            services.AddHttpClient("marvelApi", client =>
            {
                client.BaseAddress = new Uri(config["Services:marvel:url"] ?? "");
            });

            services.AddScoped<IMarvelApiService, MarvelApiService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFavoriteRepository, FavoriteRepository>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextService, UserContextService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(GetComicsQuery).Assembly));

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped(typeof(ValidationFilter<>));
            return services;
        }

        public static IServiceCollection AddAuthenticationWithJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection("jwt");
            services.Configure<JwtSettings>(jwtSection);
            var jwtSettings = jwtSection.Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings?.Key ?? ""))
                };
            });

            return services;
        }
    }

}
