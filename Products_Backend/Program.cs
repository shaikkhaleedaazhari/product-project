using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;
using AutoMapper;
using ProductService.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 👇 Configure Kestrel to listen on all interfaces (HTTP only)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7001); // Only HTTP
});

// 1️⃣ Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2️⃣ EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3️⃣ Repository
builder.Services.AddScoped<ProductRepository>();

// 4️⃣ AutoMapper (manual registration)
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// 5️⃣ CORS for your frontends
var allowedOrigins = builder.Configuration["Frontend__Origins"];
string[] origins = allowedOrigins?.Split(';', StringSplitOptions.RemoveEmptyEntries)
                    ?? new[] { "http://localhost:8080" }; // fallback

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(origins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// 6️⃣ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⚠️ Removed HTTPS redirection
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
