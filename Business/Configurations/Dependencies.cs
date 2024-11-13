using Business.Abstract;
using Business.Concrete;
using Business.Encrypt;
using Business.ValidationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Business.Configurations
{
    public static class Dependencies
    {
        public static void AddBusinessDependecies(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<MailSettings>(setting =>
            {
                setting.FromMail = Environment.GetEnvironmentVariable("MAIL_FROM");
                setting.Password = Environment.GetEnvironmentVariable("MAIL_PASSWORD");

            });
            collection.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMq"));

            collection.AddSingleton<IEmailService, EmailService>();
            collection.AddScoped<IQueueService, QueueService>();

            collection.AddScoped<IAuthenticateService, AuthenticateService>();
            collection.AddScoped<AuthenticateService>();

            collection.AddScoped<ICategoryService, CategoryService>();
            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<IToDoService, ToDoService>();

            collection.AddScoped<CategoryValidatorService>();
            collection.AddScoped<UserValidatorService>();
            collection.AddScoped<ToDoValidatorService>();


            var hashMode = configuration.Get<HashSettings>();
            switch (hashMode.HashingMode)
            {
                case "MD5":
                    collection.AddScoped<IEncryptionService, MD5HashingService>();
                    break;
                case "SHA256":
                    collection.AddScoped<IEncryptionService, SHA256HashingService>();
                    break;
            }

            collection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings")["SecurityKey"])),

                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JwtSettings")["Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JwtSettings")["Audience"],

                    ValidateLifetime = true
                };
            });

        }
    }
}
