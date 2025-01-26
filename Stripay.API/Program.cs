using Microsoft.EntityFrameworkCore;
using Stripay.API.ExternalServices.PaymentGateway;
using Stripay.API.Persistence;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer().AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "_myAllowSpecificOrigins",
        builder =>
        {
            builder
                .SetIsOriginAllowed((host) => true)
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<DbMigrationsHelper>();

StripeConfiguration.ApiKey = builder.Configuration["StripeSettings:SecretKey"];
builder.Services
    .AddScoped<IPaymentService, StripeService>()
    .AddScoped<CustomerService>()
    .AddScoped<PaymentIntentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI();
}

app.UseCors("_myAllowSpecificOrigins");
app.UseHttpsRedirection();
app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<DbMigrationsHelper>();
    await initialiser.ApplyMigrationsAsync();
}

await app.RunAsync();
