﻿using Class_Lib;
using Class_Lib.Backend.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOP_CourseProject.Controls;
using System.Windows;

namespace OOP_CourseProject
{
    public partial class App : Application
    {
        // static instance of the DbContextFactory, used to create DbContext instances
        public static AppDbContextFactory DbContextFactory { get; private set; }

        // used to store the currently logged-in employee
        public static Employee CurrentEmployee { get; internal set; }

        public static IHost AppHost { get; private set; }
        public static IHost LoginHost { get; private set; } // temp for login


        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureAppServices(services, CurrentEmployee))
                .Build();

        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // build a temporary host for login
            LoginHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddBackendServices(
                        "Server=localhost\\SQLEXPRESS;Database=PackageDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
                        null // no user context yet
                    );
                })
                .Build();

            await LoginHost.StartAsync();

            var loginWindow = new LoginWindow();
            bool? loginResult = loginWindow.ShowDialog();

            await LoginHost.StopAsync();
            LoginHost.Dispose();
            LoginHost = null;

            if (loginResult != true || CurrentEmployee == null)
            {
                Shutdown();
                return;
            }

            // main app host with logged-in user context
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureAppServices(services, CurrentEmployee))
                .Build();


            await AppHost.StartAsync();

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            Current.MainWindow = mainWindow;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            mainWindow.Show();
        }


        private static void ConfigureAppServices(IServiceCollection services, Employee? employee)
        {
            services.AddBackendServices(
                "Server=localhost\\SQLEXPRESS;Database=PackageDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
                employee
            );

            // Register UI components
            services.AddSingleton<MainWindow>();
            services.AddTransient<HomePageControl>();
            services.AddTransient<PackageControl>();
            services.AddTransient<EmployeeControl>();
            services.AddTransient<QueryBuilder>();
            services.AddTransient<SendPackageControl>();
        }


        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    // currently on localhost because none of this is real :yum:
        //    var connectionString = "Server=localhost\\SQLEXPRESS;Database=PackageDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        //    DbContextFactory = new AppDbContextFactory(connectionString);
        //}
    }

}
