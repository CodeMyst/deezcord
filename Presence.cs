using System;
using System.Threading.Tasks;

namespace DeezCord
{
    public class Presence
    {
        private readonly DiscordRpc.RichPresence presence = new DiscordRpc.RichPresence ();

        public Presence ()
        {
            Console.WriteLine ("Discord: Init");

            DiscordRpc.EventHandlers handlers = new DiscordRpc.EventHandlers
            {
                ReadyCallback = ReadyCallback,
                DisconnectedCallback = DisconnectedCallback,
                ErrorCallback = ErrorCallback
            };

            DiscordRpc.Initialize (APIKeys.DiscordApplicationId, ref handlers, true, "");

            presence.largeImageKey = "deezer_logo_large";
        }

        public void UpdatePresence (Track song)
        {
            presence.details = song.Title;
            presence.state = $"by {song.Artist.Name} - {song.Album.Title}";
            DiscordRpc.UpdatePresence (presence);
        }

        private static void ReadyCallback (ref DiscordRpc.DiscordUser connectedUser)
        {
            Console.WriteLine ($"Discord: connected to {connectedUser.Username}#{connectedUser.Discriminator}: {connectedUser.UserId}");
        }

        private static void DisconnectedCallback (int errorCode, string message)
        {
            Console.WriteLine ($"Discord: disconnect {errorCode}: {message}");
        }

        private static void ErrorCallback (int errorCode, string message)
        {
            Console.WriteLine ($"Discord: error {errorCode}: {message}");
        }
    }
}