using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace twitch_followage
{
	public class TwitchUsersResponse
	{
		[JsonPropertyName("data")]
		public IEnumerable<TwitchUser> Data { get; set; }
	}
}
