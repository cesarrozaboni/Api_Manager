using AutoMapper;
using EscNet.IoC.Cryptography;
using Manager.Api.Controllers.Token;
using Manager.Api.Controllers.ViewModel;
using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                                    {
                                        c.SwaggerDoc("v1", new OpenApiInfo
                                        {
                                            Title = "Manager Api",
                                            Version = "v1",
                                            Description = "Api desenvolvida utilizando o curso do Lucas Eschechola",
                                            Contact = new OpenApiContact
                                            {
                                                Name = "Cesar Rozaboni",
                                                Email = "cesarrozaboni@gmail.com"
                                            },
                                        });
                                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                        {
                                            In = ParameterLocation.Header,
                                            Description = "Por favor utilize Bearer <TOKEN>",
                                            Name = "Authorization",
                                            Type = SecuritySchemeType.ApiKey
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
                                                new string[]{}
                                            }
                                        });
                                    }
);

#region "Cryptography"
builder.Services.AddRijndaelCryptography(builder.Configuration["Cryptography"]);
#endregion

//adiciona uma instancia nova em cada ponto do codigo que é necessario
//builder.Services.AddTransient<>;

#region "JWT"
var secretKay = builder.Configuration["JWT:Key"];
builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKay)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion

#region "Auto Mapper"
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDto>().ReverseMap();
    cfg.CreateMap<CreateUserViewModel, UserDto>().ReverseMap();
    cfg.CreateMap<UpdateUserViewModel, UserDto>().ReverseMap();
});

builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region "Injeção de dependencia"
// Adiciona uma instancia por requisição durante o ciclo de processamento
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
#endregion

builder.Services.AddSingleton(d => builder.Configuration);
builder.Services.AddDbContext<ManagerContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:UserManager"]), ServiceLifetime.Transient);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
