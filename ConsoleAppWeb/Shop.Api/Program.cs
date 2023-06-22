using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

var jsonSettingFile = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";

var config = new ConfigurationBuilder()
    .AddJsonFile(jsonSettingFile)
    .AddEnvironmentVariables()
    .Build();

// bind config to _Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        config.Bind("AzureAd", options);
    }, options => { config.Bind("AzureAd", options);});

// development only
IdentityModelEventSource.ShowPII = true;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
