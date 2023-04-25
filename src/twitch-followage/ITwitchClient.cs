using System.Collections.Generic;
using System.Threading.Tasks;
using twitch_followage.Model;

namespace twitch_followage
{
	public interface ITwitchClient
	{
		Task<TwitchUser> GetUserAsync(string username);

		Task<IEnumerable<ChannelModel>> GetFollowedChannelsAsync(string userId);
	}
}
