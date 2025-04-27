using LAHJA_A;
using LAHJA_A.Pages;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

////app.UseHttpsRedirection();

////app.UseStaticFiles();
////app.UseAntiforgery();

////app.MapRazorComponents<App>()
////    .AddInteractiveServerRenderMode();

////app.Run();
//app.UseHttpsRedirection();

//app.UseStaticFiles();

//app.UseRouting();
//app.UseSession();
//app.MapBlazorHub();
//app.MapFallbackToPage("/_Host");
//app.Run();

var builder = WebApplication.CreateBuilder(args);
string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial catalog=MDB_Use;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<ProtectedSessionStorage>();
//builder.Services.AddTransient<Auth>();
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddRoles<IdentityRole>()
//     .AddRoleManager<RoleManager<IdentityRole>>()
//     .AddSignInManager<SignInManager<IdentityUser>>()
//     .AddUserManager<UserManager<IdentityUser>>()
//    //.AddEntityFrameworkStores<UseDbContext>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddSingleton<FoctorHttpClient>(sp => new FoctorHttpClient("https://localhost:7177/"));


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//await ATTK.Load();
app.Run();
