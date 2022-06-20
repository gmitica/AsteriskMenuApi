using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using Data.Mappings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Authorization;
using WebApi.Data.Access;
using WebApi.Helpers;
using WebApi.Services.CategroyService;
using WebApi.Services.CityService;
using WebApi.Services.CountryService;
using WebApi.Services.EmailService;
using WebApi.Services.ItemService;
using WebApi.Services.OrderService;
using WebApi.Services.RestaurantItemCategoryService;
using WebApi.Services.RestaurantService;
using WebApi.Services.StateService;
using WebApi.Services.TableService;
using WebApi.Services.UserRestaurantsService;
using WebApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AsteriskMenu", Version = "v1" });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put only your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<ICountryService, CountryService>();
builder.Services.AddTransient<IStateService, StateService>();
builder.Services.AddTransient<ICityService, CityService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRestaurantService, RestaurantService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IUserRestaurantsService, UserRestaurantsService>();
builder.Services.AddTransient<ITableService, TableService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IRestaurantItemCategoryService, RestaurantItemCategoryService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddHttpContextAccessor();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AsteriskMenu V1");
    });
}


app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();