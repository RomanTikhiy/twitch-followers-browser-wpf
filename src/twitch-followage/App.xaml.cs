using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using twitch_followage.ViewModel;

namespace twitch_followage
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			Services = ConfigureServices();

			InitializeComponent();
		}

		public new static App Current => (App)Application.Current;

		public IServiceProvider Services { get; }

		private static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			services.AddScoped<ITwitchClient, TwitchClient>();
			services.AddScoped<ITwitchTokenClient, TwitchTokenClient>();
			services.AddTransient<TwitchFollowageViewModel>();

			return services.BuildServiceProvider();
		}
	}
}
