using CustomerManagementService;

var builder = WebApplication.CreateBuilder(args);
Startup.Configure(builder);

var app = builder.Build();

Startup.ConfigureMiddleware(app);

app.Run();
