using Microsoft.EntityFrameworkCore;
using NetFullStack.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication.  In a production scenario you would
// retrieve the key and other settings from configuration or environment
// variables rather than hard‑coding them.  This sample uses a simple
// symmetric key for demonstration purposes only.
var jwtKey = builder.Configuration["Jwt:Key"] ?? "SuperSecretKey@345";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

// Configure EF Core to use SQL Server.  The connection string can be
// overridden in appsettings.json.  If you prefer SQLite for local
// development, change UseSqlServer to UseSqlite and update the
// connection string accordingly.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS to allow the front‑end to call the API.  In a
// production setting you should restrict the origins to trusted
// domains.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authentication middleware.  Must be called before UseAuthorization.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Automatically create and seed the database when the application starts.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
    DbSeeder.Initialize(dbContext);
}

app.Run();