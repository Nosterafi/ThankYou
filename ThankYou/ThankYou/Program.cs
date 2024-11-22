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
            pattern: "{controller=Main}/{action=Index}");

        app.MapControllerRoute(
            name: "pay",
            pattern: "{controller=Main}/{action=PayPage}/{employeeId?}");
    }
}