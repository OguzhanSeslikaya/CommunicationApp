using Communication.DataAccess;
using Communication.Business;
using Communication.Business.Concretes.StorageServices.LocalStorage;
using Microsoft.AspNetCore.Http.Features;
using SignalR;
using Microsoft.AspNetCore.Identity;
using Validation;
using Communication.Client.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options
    => options.AddPolicy("policy", policy =>
policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(origin => true)));

builder.Services.AddControllersWithViews();

builder.Services.AddSession(conf => conf.Cookie.Name = "GroupCookie");

builder.Services.addDataAccessServices(builder.Configuration.GetConnectionString("postgreSql"));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});



builder.Services.addBusinessServices();
builder.Services.addSignalRServices();
builder.Services.addValidationServices();
builder.Services.addStorage<LocalStorage>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionFilter>();
});


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1000000000;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.mapHubs();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Identity}/{action=LogIn}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();