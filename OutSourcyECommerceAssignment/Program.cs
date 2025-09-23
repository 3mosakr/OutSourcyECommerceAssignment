using Microsoft.EntityFrameworkCore;
using OutSourcyECommerceAssignment.Data;
using OutSourcyECommerceAssignment.Repositories.Implementations;
using OutSourcyECommerceAssignment.Repositories.Interfaces;
using OutSourcyECommerceAssignment.Services.Implementations;
using OutSourcyECommerceAssignment.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// register services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
