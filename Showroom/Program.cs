using NToastNotify;
using Showroom.Core.Interfaces;
using Showroom.Core.Repository;
using Showroom.Core.Services;
using Showroom.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<ShowroomDbContext>();

builder.Services.AddTransient<IRepository, Repository>();
builder.Services.AddTransient<ICarService, CarService>();
builder.Services.AddTransient<IShowroomService, ShowroomService>();
builder.Services.AddTransient<IPartService, PartService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGarageService, GarageService>();

builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseNToastNotify();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
