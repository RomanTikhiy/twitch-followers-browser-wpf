using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using twitch_followage.Model;

namespace twitch_followage
{
	public class TwitchClient : ITwitchClient, IDisposable
	{
		private readonly HttpClient _httpClient;
		private readonly ITwitchTokenClient _tokenClient;

		public TwitchClient(ITwitchTokenClient tokenClient)
		{
			_tokenClient = tokenClient;
			_httpClient = new HttpClient();
			_httpClient.BaseAddress = new System.Uri("https://api.twitch.tv/helix/");
			_httpClient.DefaultRequestHeaders.Add("Client-Id", "5i2u617jmzrnubx7fsndhr3a9t2nsb");
		}

		public async Task<IEnumerable<ChannelModel>> GetFollowedChannelsAsync(string userId)
		{
			await SetTokenAsync();

			var response = await _httpClient.GetAsync($"users/follows?from_id={userId}&first=100");
			var responseString = await response.Content.ReadAsStringAsync();
			var followedChannels = JsonSerializer.Deserialize<TwitchFollowedChannelsResponse>(responseString);

			var followedUsersResponse = await _httpClient.GetAsync($"users?{string.Join("&", followedChannels.Data.Select(c => $"id={c.ToId}"))}");
			var usersString = await followedUsersResponse.Content.ReadAsStringAsync();
			var usersResponse = JsonSerializer.Deserialize<TwitchUsersResponse>(usersString);
			var usersDictionary = usersResponse.Data.ToDictionary(u => u.Id);

			return followedChannels.Data.Select(
				c => new ChannelModel
				{
					ChannelName = c.ToName,
					FollowedAt = DateTime.Parse(c.FollowedAt),
					ImageUrl = usersDictionary.ContainsKey(c.ToId) == true ? usersDictionary[c.ToId].ProfileImageUrl : string.Empty
				});
		}

		public async Task<TwitchUser> GetUserAsync(string username)
		{
			await SetTokenAsync();

			var twitchUserResponse = await _httpClient.GetAsync($"users?login={username}");
			var twitchUserString = await twitchUserResponse.Content.ReadAsStringAsync();
			var userResponse = JsonSerializer.Deserialize<TwitchUsersResponse>(twitchUserString).Data.FirstOrDefault();

			return userResponse;
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}

		private async Task SetTokenAsync()
		{
			var token = await _tokenClient.GetTokenAsync();
			_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
		}
	}
}
