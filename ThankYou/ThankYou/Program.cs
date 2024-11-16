using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Main/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        MapRouts(app);

        app.Run();
    }

    static void MapRouts(WebApplication app)
    {
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Main}/{action=MainPage}/{id?}");

        app.MapControllerRoute(
            name: "pay",
            pattern: "{controller=Main}/{action=PayPage}/{id?}"
            );

        app.MapControllerRoute(
            name: "payError",
            pattern: "{controller=Main}/{action=PayErrorPage}/{id?}"
            );

        app.MapControllerRoute(
            name: "payNotenough",
            pattern: "{controller=Main}/{action=PayNotenoughPage}/{id?}"
            );

        app.MapControllerRoute(
            name: "paySucces",
            pattern: "{controller=Main}/{action=PaySuccesPage}/{id?}"
            );
    }
}