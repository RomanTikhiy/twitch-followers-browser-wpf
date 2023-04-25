using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using twitch_followage.ViewModel;

namespace twitch_followage.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class TwitchFollowageView : Window
	{
		public TwitchFollowageView()
		{
			InitializeComponent();
			DataContext = App.Current.Services.GetService<TwitchFollowageViewModel>();
		}

		public TwitchFollowageViewModel ViewModel => (TwitchFollowageViewModel)DataContext;
    }
}
