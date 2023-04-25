using System.Threading.Tasks;

namespace twitch_followage
{
	public interface ITwitchTokenClient
	{
		Task<TwitchTokenResponse> GetTokenAsync();
	}
}
