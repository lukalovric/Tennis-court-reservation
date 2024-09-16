using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tennis.Repository.Common;
using Swashbuckle.AspNetCore.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Tennis.Service.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Access-Control-Allow-Origin",
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();

        });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    containerBuilder.RegisterType<ReservationRepository>().As<IReservationRepository>()
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();
    containerBuilder.RegisterType<ReservationService>().As<IReservationService>()
    .WithParameter("connectionString",connectionString)
    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<UserRepository>().As<IUserRepository>()
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();
    containerBuilder.RegisterType<UserService>().As<IUserService>()
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();

    containerBuilder.RegisterType<CourtRepository>().As<ICourtRepository>()
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();
    containerBuilder.RegisterType<CourtService>().As<ICourtService>()
    .WithParameter("connectionString", connectionString)
    .InstancePerLifetimeScope();
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Access-Control-Allow-Origin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
