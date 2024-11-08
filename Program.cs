using Microsoft.EntityFrameworkCore;
using Todo.Web.Data;

namespace Todo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //inject db context
            builder.Services.AddDbContext<ToDoContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ToDoContext")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            { var db = scope.ServiceProvider.GetRequiredService<ToDoContext>(); db.Database.Migrate(); }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
