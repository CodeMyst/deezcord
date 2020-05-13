using System;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using DiscordRPC;

namespace Deezcord
{
    public class Program
    {
        static DiscordRpcClient client = new DiscordRpcClient(APIKeys.DiscordApplicationId);
        public static long currentlyPlaying;
        private static void Main()
        {
            // Console.WriteLine("Deezcord Plus - https://github.com/b1sergiu/deezcord-plus/");
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            Console.WriteLine("Initializing Discord...");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Assets = new Assets()
                {
                    LargeImageKey = "logo"
                }
            });
            await StartPresence();
        }

        public static async Task StartPresence()
        {
            while (true)
            {
                Track track = await DeezerAPI.LastTrack();
                if (currentlyPlaying != track.Id)
                {
                    UpdatePresence(track);
                    Console.WriteLine($"Currently playing: {track.Title} by {track.Artist.Name} - {track.Album.Title}");
                    currentlyPlaying = track.Id;
                }
                await Task.Delay(30000);
            }
        }

        public static void UpdatePresence(Track song)
        {
            client.SetPresence(new RichPresence()
            {
                Details = song.Title,
                State = $"by {song.Artist.Name}",
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = song.Album.Title
                }
            });
            client.Invoke();
        }

        public async Task Login()
        {
            await DeezerAPI.Authenticate();
            User user = await DeezerAPI.User();
            Console.WriteLine($"Deezer user: {user.Name}");
        }
    }
}