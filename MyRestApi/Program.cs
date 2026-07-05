var builder = WebApplication.CreateBuilder(args);

// register controller class into app framework
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();
app.Run();