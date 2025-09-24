using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutSourcyECommerceAssignment.Data;
using OutSourcyECommerceAssignment.Middlewares;
using OutSourcyECommerceAssignment.Repositories.Implementations;
using OutSourcyECommerceAssignment.Repositories.Interfaces;
using OutSourcyECommerceAssignment.Services.Implementations;
using OutSourcyECommerceAssignment.Services.Interfaces;
using OutSourcyECommerceAssignment.Validators.CustomerValidators;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddControllers();
// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();

// Custom Validation Error Response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new { field = e.Key, error = e.Value?.Errors.First().ErrorMessage })
            .ToList();

        var result = new
        {
            success = false,
            errors,
            statusCode = 400
        };

        return new BadRequestObjectResult(result);
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Global Error Handling Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
