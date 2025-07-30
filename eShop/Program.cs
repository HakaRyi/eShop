using BOs;
using eShop.Components;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer;
using ServiceLayer.Interfaces;

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

            builder.Services.AddSession();
            builder.Services.AddDistributedMemoryCache();

            // Register DbContext with connection string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<EShopContext>(options =>
                options.UseSqlServer(connectionString));

            // ====== REGISTER REPOSITORIES & SERVICES ======

            // Member
            builder.Services.AddScoped<MemberRepository>();              // ✅ Repository
            builder.Services.AddScoped<IMemberService, MemberService>(); // ✅ Service qua interface

			// Product
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IProductService, ProductService>();

			// Category
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<ICategoryService, CategoryService>();

			// Order
			builder.Services.AddScoped<IOrderRepository, OrderRepository>();
			builder.Services.AddScoped<IOrderService, OrderService>();

			// OrderDetail
			builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
			builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

			// Transaction
			builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();

            // Feedback
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();

            // ==================================================

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // 30 days HSTS default
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
