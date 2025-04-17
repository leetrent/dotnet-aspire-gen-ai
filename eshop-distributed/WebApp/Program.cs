using WebApp.ApiClients;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

/////////////////////////////////////
// ADD SERVICES TO THE CONTAINER
/////////////////////////////////////
builder.AddServiceDefaults();

builder.Services.AddHttpClient<CatalogApiClient>(client =>
{
    client.BaseAddress = new("https+http://catalog");
});

builder.AddRedisOutputCache("cache");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

/////////////////////////////////////////
// BUILD THE WEB APPLICAITON
////////////////////////////////////////
var app = builder.Build();

/////////////////////////////////////////
// CONFIGURE THE HTTP REQUEST PIPELINE
////////////////////////////////////////

app.MapDefaultEndpoints();
app.UseOutputCache();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
