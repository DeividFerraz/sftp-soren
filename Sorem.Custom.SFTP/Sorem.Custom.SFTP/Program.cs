using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;
using Soren.Custom.Service.IDependencyInjection;

const string PROJECT_NAME = "Soren.Custon.Sftp";
const string API_VERSION = "v1";
const string XML_EXTENSION = ".xml";
const string SWAGGERFILE_PATH = "./swagger/v1/swagger.json";
const string SECURITY_DESCRIPTION = "API Key";
const string SECURITY_HEADER_NAME = "X-API-KEY";

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDependencies();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(API_VERSION, new OpenApiInfo
    {
        Title = PROJECT_NAME,
        Version = API_VERSION
    });

    var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + XML_EXTENSION;
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
    {
        Description = SECURITY_DESCRIPTION,
        In = ParameterLocation.Header,
        Name = SECURITY_HEADER_NAME,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
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