using GamingData.Repository;

namespace TransactionsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            //builder.Services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllBlazorOrigin", builder =>
                {
                    builder.WithOrigins("https://localhost:7077","http://localhost:5153");
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllBlazorOrigin");

            app.UseAuthorization();

            app.MapControllers();

            //app.UseStatusCodePagesWithRedirects("/404");

            app.Run();
        }
    }
}
