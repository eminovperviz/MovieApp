using MovieApp.Api.DependencyInjection.Api;
using MovieApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiProjectInjection(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseSwaggerDocumentation();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllCors");

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
