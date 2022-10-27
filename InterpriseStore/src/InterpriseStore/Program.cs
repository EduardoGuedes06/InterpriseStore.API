
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Data.context;
using InterpriseStore.Configuration;
using Revisao.Api.Configuration;
using InterpriseStore.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    //MYSQL
    options.UseMySql("server=localhost;initial catalog=InterpriseStoreDb;uid=root;pwd=Root",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //SQLSERVER
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //MYSQL
    options.UseMySql("server=localhost;initial catalog=InterpriseStoreDb;uid=root;pwd=Root",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.0-mysql")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //SQLSERVER
    //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ResolveDependencies();



var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);
app.UseSwaggerConfig(apiVersionDescriptionProvider);

//app.UseAuthorization();
//app.UseAuthentication();
//app.UseHttpsRedirection();
//app.MapControllers();
//app.UseDeveloperExceptionPage();

app.Run();
