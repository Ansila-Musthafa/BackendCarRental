using BusinessLogicLayer;
using BusinessLogicLayer.Interface;
using DataAccessLayer;
using GlobalEntityLayer.Models.Admin;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
       
//builder.Services.AddSwaggerGen(c=>
//{
//    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme

//    {

//        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",

//        In = ParameterLocation.Header,

//        Name = "Authorization",

//        Type = SecuritySchemeType.ApiKey

//    });
//});
builder.Services.AddScoped<ICars, Cars>();
builder.Services.AddScoped<IAccounts, Accounts>();

builder.Services.AddDbContext<CarDetailsDBcontext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddIdentity<APIuser, IdentityRole>()
    .AddEntityFrameworkStores<CarDetailsDBcontext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "JWT:ValidIssuer",
            ValidAudience = "JWT:ValidAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWT:SecretKey"))

        };
    });
 
builder.Services.AddIdentityCore<APIuser>(i=>i.User.RequireUniqueEmail=true);
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
