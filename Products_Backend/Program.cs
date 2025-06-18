using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;
using AutoMapper;
using ProductService.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 👇 Configure Kestrel to listen on all interfaces (0.0.0.0)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7001); // HTTP
    options.ListenAnyIP(7000, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS
    });
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

// 5️⃣ Optional: Enable CORS for your front-ends
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://18.234.196.58",
            "http://localhost:8080",
            "http://localhost:8081",
            "https://localhost:3000"
        )
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

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
