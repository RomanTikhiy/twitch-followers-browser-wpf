using System.Text.Json.Serialization;

namespace twitch_followage
{
	public class TwitchTokenResponse
	{
		[JsonPropertyName("access_token")]
		public string Token { get; set; }

		[JsonPropertyName("token_type")]
		public string TokenType { get; set; }

		[JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
