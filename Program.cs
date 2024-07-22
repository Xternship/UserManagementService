using UserManagementService.Data;
using UserManagementService.Services;
using Microsoft.EntityFrameworkCore;
using EmailService.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Data Source=C:/Users/MalithiAbayadeera/source/repos/Xternship/Database/Xternship.db";
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddGrpc();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService.Services.EmailService>();

var app = builder.Build();

// Change port here
app.Urls.Add("http://localhost:5001"); // Use a different port

app.MapGrpcService<UserManageService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
