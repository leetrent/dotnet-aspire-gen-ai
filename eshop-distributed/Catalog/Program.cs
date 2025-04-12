
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.AddNpgsqlDbContext<ProductDbContext>(connectionName: "catalogDb");
builder.Services.AddScoped<ProductService>();
builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseMigration();
}
app.MapProductEndpoints();
app.UseHttpsRedirection();

app.Run();