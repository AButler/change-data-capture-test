using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserEditor.Config;
using UserEditor.Dialogs;
using UserEditor.Repositories;
using UserEditor.ViewModels;

namespace UserEditor;

public partial class App
{
    public static new App Current => (App)Application.Current;
    public IConfigurationRoot Configuration { get; private set; }
    public ServiceProvider Services { get; private set; }

    public App()
    {
        InitializeComponent();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        Configuration = configurationBuilder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        Services = serviceCollection.BuildServiceProvider();

        Current.MainWindow = Services.GetRequiredService<MainWindow>();
        Current.MainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.Configure<DatabaseSettings>(Configuration.GetSection("Database"));

        services.AddSingleton<UserRepository>();
        services.AddSingleton<UserTypeRepository>();

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<AddEditUserViewModel>();

        services.AddTransient<MainWindow>();
        services.AddTransient<AddEditUserDialog>();
    }
}
