using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SportTracker.Models;
using SportTracker.Utilities;

var builder = WebApplication.CreateBuilder(args);

Directory.CreateDirectory( Path.Combine( Environment.CurrentDirectory, "Images") );

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(0, 1);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=Application.db;");
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidAudience = "false",
            ValidIssuer = "false",
            IssuerSigningKey =
                new SymmetricSecurityKey("supersecretkeysupersecretkeysupersecretkeysupersecretkey"u8.ToArray())
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Query.TryGetValue("access_token", out var token)) context.Token = token[0];

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                var catchException = context.Exception;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();





builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "SportTracker API",
            Version = "v0.1"
        }
    );
    
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = async (context) =>
    {
        if (context.Context.User.Identity is { IsAuthenticated: false } && context.Context.Request.Path.StartsWithSegments("/profilePicture"))
        {
            // show access denied message
            context.Context.Response.StatusCode = 401;
            context.Context.Response.ContentType = "text/plain";
            //write a long interesting and funny message about why you can't see the image,
            await context.Context.Response.WriteAsync(
                "Whoa there, Sherlock! You've uncovered our classified collection. " +
                "Looks like you're ahead of the game!" +
                "To unlock these mysteries, you must first authenticate. Return to the login page and " +
                "show them who's the real detective. " +
                "In the meantime, we tip our hats to your sneaky skills!");
        }
    },
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Environment.CurrentDirectory, "Images")),
    RequestPath = "/profilePicture"
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();



void SetupDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

SetupDatabase();
app.Run();