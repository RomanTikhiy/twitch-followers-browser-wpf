using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace twitch_followage
{
	public class TwitchFollowedChannelsResponse
	{
		[JsonPropertyName("data")]
		public IEnumerable<TwitchChannel> Data { get; set; }

		[JsonPropertyName("pagination")]
		public Pagination Pagination { get; set; }

		[JsonPropertyName("total")]
		public int Total { get; set; }
    }
}
