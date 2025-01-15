namespace Productos.Cliente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Permite cualquier origen (útil para desarrollo)
                          .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, PUT, DELETE, etc.)
                          .AllowAnyHeader(); // Permite cualquier encabezado
                });
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient();

            var app = builder.Build();

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

            // Cambiar Dashboard a Producto o Rol, según corresponda
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Producto}/{action=Index}/{id?}");

            app.Run();
        }
    }
}