using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MO.Result;
using System.Net;
using System.Text;
using TS.Bootcamp.ECommerce.WebAPI.Context;
using TS.Bootcamp.ECommerce.WebAPI.Dtos;
using TS.Bootcamp.ECommerce.WebAPI.Middlewares;
using TS.Bootcamp.ECommerce.WebAPI.Models;
using TS.Bootcamp.ECommerce.WebAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger JWT 
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Bearer Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
      {
        new OpenApiSecurityScheme
         {
           Reference = jwtSecuritySheme.Reference,
         },
         new string[] {}
      }
     });
});
#endregion

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


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


#region JWT

// app.settingste belirtilen sectionda yer alan ayarlarý burada ilgili classa program derlendiðinde atama yapýyoruz.
builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));

builder.Services.AddScoped<JwtProvider>();
var provider = builder.Services.BuildServiceProvider();
var jwtOptions = provider.GetRequiredService<IOptionsMonitor<Jwt>>();

string secretKeyValue = jwtOptions.CurrentValue.SecretKey;
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyValue));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.CurrentValue.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.CurrentValue.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey
    };
});
#endregion

builder.Services.AddExceptionHandler<CustomExceptionMiddleware>().AddProblemDetails();


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
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

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();
app.MapControllers().RequireRateLimiting("fixed");

app.UseExceptionHandler();

app.MapPost("api/auth/login", (JwtProvider _jwtProvider, LoginDto request) =>
{
    AppUser? user = GlobalData.Users.FirstOrDefault(p => p.Email == request.Email);
    if (user is null)
    {
        return Results.BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "User not found"));
    }
    bool checkPassword = HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
    if (!checkPassword)
    {
        return Results.BadRequest(Result<string>.Fail((int)HttpStatusCode.BadRequest, "Password is wrong"));
    }

    return Results.Ok(Result<object>.Success(new { Token = _jwtProvider.CreateToken(user) }));
});

app.Run();
