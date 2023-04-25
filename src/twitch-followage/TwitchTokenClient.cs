using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace twitch_followage
{
	public class TwitchTokenClient : ITwitchTokenClient
	{
		private TwitchTokenResponse _response;

		private DateTime _expiresAt;

		public async Task<TwitchTokenResponse> GetTokenAsync()
		{
			if (_response == null || _expiresAt < DateTime.UtcNow)
			{
				using var client = new HttpClient();
				client.BaseAddress = new Uri("https://id.twitch.tv");

				var request = new List<KeyValuePair<string, string>>();
				request.Add(new KeyValuePair<string, string>("client_id", ""));
				request.Add(new KeyValuePair<string, string>("client_secret", ""));
				request.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
				var content = new FormUrlEncodedContent(request);
				
				var response = await client.PostAsync("oauth2/token", content);

				var responseString = await response.Content.ReadAsStringAsync();
				var tokenResponse = JsonSerializer.Deserialize<TwitchTokenResponse>(responseString);
				_response = tokenResponse;
				_expiresAt = DateTime.UtcNow.AddMilliseconds(_response.ExpiresIn);
			}

			return _response;
		}
	}
}
