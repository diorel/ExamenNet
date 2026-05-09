using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.Middleware;
using PaymentAPI.Repositories;
using PaymentAPI.Repositories.Interfaces;
using PaymentAPI.Services;
using PaymentAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Controllers
builder.Services.AddControllers();

// 2. DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Repository
builder.Services.AddScoped<IPaymentRequestRepository, PaymentRequestRepository>();

// 4. Service
builder.Services.AddScoped<IPaymentRequestService, PaymentRequestService>();

// 5. CORS
var allowedOrigins = builder.Configuration["AllowedOrigins"] ?? "http://localhost:4200";
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 6. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 7. Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// 8. Swagger (solo en Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 9. CORS
app.UseCors("FrontendPolicy");

// 10. Authorization
app.UseAuthorization();

// 11. Controllers
app.MapControllers();

app.Run();
