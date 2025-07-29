using BOs;
using BOs.Entities;
using eShop.Components;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using ServiceLayer;

namespace eShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true; 
                options.Cookie.IsEssential = true; 
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EShopContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDistributedMemoryCache();
            //builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<MemberService>();
            builder.Services.AddScoped<MemberRepository>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<OrderRepository>();
            builder.Services.AddScoped<OrderService>();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EShopContext>();
                context.Database.EnsureCreated();
                Console.WriteLine("EnsureCreated called from Program.cs");
                if (!context.Categories.Any())
                {
                    context.Categories.Add(new Category { CategoryName = "Electronics", Description = "Electronic products" });
                    context.Categories.Add(new Category { CategoryName = "Clothing", Description = "Clothing products" });
                    context.SaveChanges();

                    for (int i = 1; i <= 10; i++)
                    {
                        context.Products.Add(new Product
                        {
                            ProductName = $"Product {i}",
                            UnitPrice = 10 * i,
                            UnitsInStock = 50,
                            CategoryId = i % 2 == 0 ? 1 : 2,
                            Weight = $"{i}kg"
                        });
                    }
                    context.SaveChanges();
                    Console.WriteLine("Sample data added at {DateTime.Now}");
                }
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
           
            app.UseStaticFiles();
            app.UseAntiforgery();
            app.UseSession();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
