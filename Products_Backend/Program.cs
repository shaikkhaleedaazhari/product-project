using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Repositories;
using AutoMapper;
using ProductService.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 1?? Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2?? EF Core
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3?? Repository
builder.Services.AddScoped<ProductRepository>();

// 4?? AutoMapper (manual registration)
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// 5?? Optional: Enable CORS for your front-ends
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:8080",  // your UI ports
            "http://localhost:8081",
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// 6?? Middleware
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
