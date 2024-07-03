using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Courses.Models;
using Project_Courses.Repositories;
using Project_Courses.Services.Auth;
using Project_Courses.Services.User;
using System.Reflection;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// SWAGGER CONFIGURATION
builder.Services.AddSwaggerGen(options =>
{
    // Configures Swagger to include a security definition. This setup specifies how Swagger UI should send the authorization token.
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Scheme = "Bearer", // The authorization scheme used, which is Bearer token.
        In = ParameterLocation.Header, // Specifies that the token should be passed in the header.
        Type = SecuritySchemeType.ApiKey, // Indicates the type of security being configured is an API key.
        Name = "Authorization" // The name of the header to be used.
    });

    // Adds a filter to Swagger operations to include the security definitions in each endpoint, enabling you to authenticate via Swagger UI.
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


// REGISTRATIONS
// Registers the IHttpContextAccessor service which allows access to the HttpContext through dependency injection if it's not accessible otherwise.
builder.Services.AddHttpContextAccessor();

// Register the CourseRepository as a scoped service to be injected wherever ICourseRepository is required.
builder.Services.AddTransient<ICourseRepository, CourseRepository>();

// Register the PermissionService as a scoped service to be injected wherever ILoginService is required.
builder.Services.AddTransient<ILoginService, LoginService>();

// Register the UserService as a scoped service to be injected wherever IUserService is required.
builder.Services.AddTransient<IUserService, UserService>();

// Register the DbContext using the SQL Server provider with a connection string from configuration.
builder.Services.AddDbContext<DbAa579fCoursesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Register the FluentValidation service to validate the request models in the application.
builder.Services.AddFluentValidation((options) =>
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly())
);

// Register AutoMapper to manage data mapping in the application. It uses the current assembly to find and configure all AutoMapper profiles.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


// JWT CONFIGURATION
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Does not validate the issuer of the token.
        ValidateAudience = false, // Does not validate the audience of the token.
        ValidateIssuerSigningKey = true, // Validates the security key used to sign the token.
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("If debugging is the process of removing software bugs, then programming must be the process of putting them in.\r\n") // The actual secret key used for signing the tokens.
        )
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// CORS CONFIGURATION
// CORS is a security feature that restricts how resources on a web page can be requested from another domain outside the domain from which the first resource was served.
app.UseCors(options =>
{
    // AllowAnyOrigin is a setting that allows access to the server from any domain.
    // Note: This configuration is not recommended for production environments as it's less secure.
    // In production, it's better to specify the exact origins that should be allowed to access the resources.
    options.AllowAnyOrigin();

    // AllowAnyHeader allows the client to send any HTTP headers with the request.
    options.AllowAnyHeader();

    // AllowAnyMethod allows all HTTP methods from the client.
    options.AllowAnyMethod();
});


app.UseHttpsRedirection();


// This middleware is responsible for attempting to authenticate each request based on data
// in the request, typically looking at cookies or headers. It is crucial for any application
// that limits access to certain resources based on user identity.
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
