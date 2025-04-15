var builder = WebApplication.CreateBuilder(args);

////////////////////////////////
// ADD SERVICES TO THE CONTAINER
////////////////////////////////
builder.AddServiceDefaults();
builder.AddRedisDistributedCache(connectionName: "cache"); // ConnectionStrings__cache
builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    client.BaseAddress = new("https+http://catalog");
});

builder.Services.AddMassTransitWithAssemblies(Assembly.GetExecutingAssembly());

builder.Services.AddAuthentication()
    .AddKeycloakJwtBearer
    (
        serviceName: "keycloak",
        realm: "eshop",
        configureOptions: options =>
        {
            options.RequireHttpsMetadata = false;
            options.Audience = "account";
           
        }
    );

builder.Services.AddAuthorization();

var app = builder.Build();

//////////////////////////////////////
// CONFIGURE THE HTTP REQUEST PIPELINE
//////////////////////////////////////

app.MapDefaultEndpoints();
app.MapBasketEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();