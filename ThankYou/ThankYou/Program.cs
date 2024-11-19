using System.Runtime.CompilerServices;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddSession();
        var app = builder.Build();

        app.UseStaticFiles();
        app.UseSession();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Main/Error");
            app.UseHsts();
        }

        MapRouts(app);

        app.Run();
    }

    static void MapRouts(WebApplication app)
    {
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Main}/{action=Index}/{employeeId?}");

        app.MapControllerRoute(
            name: "main",
            pattern: "{controller=Main}/{action=}/{id?}");

        app.MapControllerRoute(
            name: "pay",
            pattern: "{controller=Main}/{action=PayPage}/{id?}");

        app.MapControllerRoute(
            name: "payError",
            pattern: "{controller=Main}/{action=PayErrorPage}/{id?}");

        app.MapControllerRoute(
            name: "payNotenough",
            pattern: "{controller=Main}/{action=PayNotenoughPage}/{id?}");

        app.MapControllerRoute(
            name: "paySucces",
            pattern: "{controller=Main}/{action=PaySuccesPage}/{id?}");
    }
}