using HealthChecks.UI.Client;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TS.Bootcamp.ECommerce.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS Settings
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(options =>
{
    options.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
}));
#endregion

#region RateLimiter
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", config =>
    {
        config.PermitLimit = 100;
        config.QueueLimit = 100;
        config.Window = TimeSpan.FromSeconds(10);
        config.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});
#endregion

#region HealthCheck
builder.Services.AddHealthChecks().AddCheck("apiInformation", () => HealthCheckResult.Healthy());
#endregion

builder.Services.AddExceptionHandler<CustomExceptionMiddleware>().AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseHealthChecks("/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("fixed");

app.UseExceptionHandler();
app.Run();
