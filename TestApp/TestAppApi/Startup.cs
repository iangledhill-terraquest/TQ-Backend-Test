using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TestAppApi.Controllers;

namespace TestAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ChatDbContext, ChatDbContext>(serviceProvider =>
            {
                var builder = new DbContextOptionsBuilder<ChatDbContext>()
                    .UseSqlite(CreateConnection());
                
                var db = new ChatDbContext(builder.Options);
                db.Database.OpenConnection();
                db.Database.EnsureCreated();

                return db;

            });
        }

        private static readonly string dbId = Guid.NewGuid().ToString();

        private static DbConnection CreateConnection()
        {
            var connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = dbId,
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared
            };

            var connection = new SqliteConnection(connectionString.ConnectionString);
            
            return connection;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
