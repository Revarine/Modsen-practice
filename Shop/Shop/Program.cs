using BusinessLogic.Services;
using BusinessLogic.Services.DTO;
using BusinessLogic.Services.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Interfaces;
using DataAccess.Data.Repositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Middlewares.GlobalExceptionHandler;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ShopDbContext)));
    });

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<RegisterDto, UserDto>();
    cfg.CreateMap<User, UserDto>();
    cfg.CreateMap<RegisterDto, UserDto>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
    cfg.CreateMap<UserDto, User>();
}, typeof(Program));

// Add controllers
builder.Services.AddControllers();

// Configure JWT authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
// Configure global exception handler
app.UseGlobalExceptionHandler();


app.MapGet("/", () => "Hello world!");

app.Run();
