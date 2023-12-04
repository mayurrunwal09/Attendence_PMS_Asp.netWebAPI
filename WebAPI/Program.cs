using Domain.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositroy_And_Services.context;
using Repositroy_And_Services.Repository;
using Repositroy_And_Services.Services.CustomService.AttendenceServices;
using Repositroy_And_Services.Services.CustomService.BreakServices;
using Repositroy_And_Services.Services.CustomService.ClockOutServices;
using Repositroy_And_Services.Services.CustomService.FinishBreakService;
using Repositroy_And_Services.Services.CustomService.LeaveServices;
using Repositroy_And_Services.Services.CustomService.ReportServices;
using Repositroy_And_Services.Services.CustomService.UserServices;
using Repositroy_And_Services.Services.CustomService.UserTypeServices;
using Repositroy_And_Services.Services.GenericService;
using System.Text;
using WebAPI.Middleware;
using WebAPI.Middleware.Auth;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Database Connection        
builder.Services.AddDbContext<MainDBContext>(d => d.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS   
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
#endregion


#region Swagger Configuration   
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, Array.Empty<string>()
        }
    });
});
#endregion

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireHRRole", policy => policy.RequireRole("HR"));
});


#region Authentication and JWT Configuration
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Call this to skip the default logic and avoid using the default response
            context.HandleResponse();
            // Write to the response in any way you wish
            context.Response.StatusCode = 401;
            // context.Response.Headers.Append("my-custom-header", "custom-value");

            Response<String> response = new()
            {
                Message = "You are not authorized!",
                Status = (int)System.Net.HttpStatusCode.Unauthorized
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    };
});

#endregion

#region  Dependency Injection 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient(typeof(IUserTypeService), typeof(UserTypeService));
builder.Services.AddTransient(typeof(IUserService), typeof(UserService));
builder.Services.AddTransient(typeof(IReportService), typeof(ReportService));
builder.Services.AddTransient(typeof(IBreakService), typeof(BreakService));
builder.Services.AddTransient(typeof(IAttendenceService), typeof(AttendenceService));
builder.Services.AddTransient(typeof(ILeaveService), typeof(LeaveService));
builder.Services.AddTransient(typeof(IClockOutService), typeof(ClockOutService));
builder.Services.AddTransient(typeof(IFinishBreakService), typeof(FinishBreakService));
builder.Services.AddTransient<IJWTAuthManager, JWTAuthManager>();

builder.Services.AddAutoMapper(typeof(StartupBase));
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseExceptionHandlerMiddleware();
app.UseRouting();
app.UseCors(options => options.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
