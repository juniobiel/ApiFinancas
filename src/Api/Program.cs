using Api.Config;
using Api.Config.Swagger;
using Data.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddApiConfig();

builder.Services.AddDbContext<FinanceDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.ResolveDependencies();

builder.Services.AddIndentityConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfig();

builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.Run();
