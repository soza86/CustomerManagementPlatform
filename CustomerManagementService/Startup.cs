using CustomerManagementService.BusinessLayer;
using CustomerManagementService.DataLayer;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementService
{
    public class Startup
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CustomerContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomersDbConnection")));

            builder.Services.AddScoped<DbContext, CustomerContext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("BasicPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("BasicPolicy");

            app.MapControllers();
        }
    }
}
