using Microsoft.EntityFrameworkCore;
using MyRestApi.Data;

var builder = WebApplication.CreateBuilder(args);

// register controller class into app framework
builder.Services.AddControllers();

// register the AppDbContext to use an In-Memory Database named "StoreDb"
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("StoreDb"));

//example connection to a real db
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options=> options.UseNpgsql(connectionString));
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();