using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using twitch_followage.Model;

namespace twitch_followage.ViewModel
{
	public partial class TwitchFollowageViewModel : ObservableObject
	{
		private readonly ITwitchClient _twitchClient;

		public TwitchFollowageViewModel(ITwitchClient twitchClient)
		{
			IsSearchButtonEnabled = true;
			_twitchClient = twitchClient;
			SearchText = "stepangigachad";
		}

		[ObservableProperty]
		private ObservableCollection<ChannelModel> channels = new ObservableCollection<ChannelModel>();

		[ObservableProperty]
		private string searchText;

		[ObservableProperty]
		private TwitchUser currentUser;

		[ObservableProperty]
		private ChannelModel selectedChannel;

		[RelayCommand]
		public async Task GoToChannelAsync(ChannelModel channel)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = $"https://www.twitch.tv/{channel.ChannelName}",
				UseShellExecute = true
			});
		}

		[RelayCommand]
		public async Task SearchAsync()
		{
			if (string.IsNullOrEmpty(SearchText))
			{
				return;
			}

			IsLoading = true;
			IsSearchButtonEnabled = false;
			Channels.Clear();
			var user = await _twitchClient.GetUserAsync(SearchText);
			CurrentUser = user;
			var channels = await _twitchClient.GetFollowedChannelsAsync(user.Id);
			Channels = new ObservableCollection<ChannelModel>(channels);
			IsSearchButtonEnabled = true;
			IsLoading = false;
			SelectedChannel = null;
		}

		[RelayCommand]
		public async Task SearchOfSelectedChannelAsync(ChannelModel channel)
		{
			SearchText = channel.ChannelName;
			await SearchAsync();
		}

		[ObservableProperty]
		private bool isSearchButtonEnabled;

		[ObservableProperty]
		private bool isLoading;
	}
}
