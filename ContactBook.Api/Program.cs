using CloudinaryDotNet;
using ContactBook.Common.Validations;
using ContactBook.Core.Services.Abstractions.IAuthentication;
using ContactBook.Core.Services.Abstractions.ICloudinary;
using ContactBook.Core.Services.Abstractions.IContacts;
using ContactBook.Core.Services.Abstractions.ICrud;
using ContactBook.Core.Services.Implementations;
using ContactBook.Core.Services.Implementations.Authentication;
using ContactBook.Core.Services.Implementations.Contacts;
using ContactBook.Core.Services.Implementations.Crud;
using ContactBook.Data.DbContext;
using ContactBook.Model.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization", Version = "v1" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Jwt",
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
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

});

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IRouteServices, RouteServices>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<UserValidator>();



builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();

// Add this in the ConfigureServices method
//builder.Services.Configure<CloudinaryConfiguration>(builder.Configuration.GetSection("Cloudinary"));
// In Startup.cs ConfigureServices method
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
// Add this in the ConfigureServices method
var cloudinaryAccount = new Account(
    "dlonxavjl",
    "596477364771399",
    "LJbc7XfBX29zR06t6ch93xjdhPU");

var cloudinary = new Cloudinary(cloudinaryAccount);

builder.Services.AddSingleton(cloudinary);

//Configure JWT authentication options....
var jwtsetting = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtsetting["secret"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
//Jwt configuration ends...




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
    });
}
//app.UseSwagger();
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
//});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
