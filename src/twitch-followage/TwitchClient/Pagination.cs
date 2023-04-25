using System.Text.Json.Serialization;

namespace twitch_followage
{
	public class Pagination
	{
		[JsonPropertyName("cursor")]
        public string Cursor { get; set; }
    }
}
