using System.Text.Json;
using System.Text.Json.Serialization;
using lab1.Pages;
using lab1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/Updates/Json", async context =>
{
    await context.Response.WriteAsync(JsonSerializer.Serialize(UpdatesService.GetUpdatesInfo().Updates));
});

app.MapGet("/Antivirus/Json", async context =>
{
    await context.Response.WriteAsync(JsonSerializer.Serialize(AntivirusService.GetAntivirusInfo()));
});

app.MapGet("/Json", async context =>
{
    await context.Response.WriteAsync(JsonSerializer.Serialize(HardwareService.GetHardwareInfo()));
});

app.MapGet("/Network/Json", async context =>
{
    await context.Response.WriteAsync(JsonSerializer.Serialize(NetworkService.GetNetworkAdaptersInfo().Adapters));
});
app.Run();