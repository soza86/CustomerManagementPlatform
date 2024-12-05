using CustomerManagementService;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
Startup.Configure(builder);

var app = builder.Build();

Startup.ConfigureMiddleware(app);

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var email = "dummyuser@example.com";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser { UserName = "dummyuser", Email = email };
        await userManager.CreateAsync(user, "Sample@123");
    }
}

app.Run();
