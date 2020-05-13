using System;
using System.IO;
using System.Net;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Newtonsoft.Json;

namespace Deezcord
{
    public static class DeezerAPI
    {
        private const string redirectUri = "http://127.0.0.1:5000/";
        private const string authorizationEndpoint = "https://connect.deezer.com/oauth/auth.php";
        private const string tokenRequestUri = "https://connect.deezer.com/oauth/access_token.php";
        private const string lastTrackEndpoint = "https://api.deezer.com/user/me/history";
        private const string userEndpoint = "https://api.deezer.com/user/me";
        private const string tokenPath = "token.txt";

        private static string token;

        public static async Task Authenticate()
        {
            Console.WriteLine($"Redirect URI: {redirectUri}");

            HttpListener http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            Console.WriteLine("Listening...");
            http.Start();

            string authorizationRequest = $"https://connect.deezer.com/oauth/auth.php?app_id={APIKeys.DeezerClientId}&redirect_uri={Uri.EscapeDataString(redirectUri)}&perms=listening_history";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    FileName = authorizationRequest
                };
                Process.Start(psi);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", authorizationRequest);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", authorizationRequest);
            }

            HttpListenerContext context = await http.GetContextAsync();

            HttpListenerResponse response = context.Response;
            const string responseString = "<html><head><script type=\"text/javascript\">function closeMe() { window.close(); }</script></head><body>Please return to the app.</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((tasl) =>
            {
                responseOutput.Close();
                http.Stop();
                Console.WriteLine("HTTP server stopped.");
            });

            if (context.Request.QueryString.Get("error") != null)
            {
                Console.WriteLine($"OAuth authorization error: {context.Request.QueryString.Get("error")}");
                return;
            }

            if (context.Request.QueryString.Get("code") == null)
            {
                Console.WriteLine($"Malformed authorization response. {context.Request.QueryString}");
                return;
            }

            string code = context.Request.QueryString.Get("code");

            Console.WriteLine($"Authorization code: {code}");

            Console.WriteLine("Exchanging code for tokens...");

            string tokenRequestBody = $"code={code}&app_id={APIKeys.DeezerClientId}&secret={APIKeys.DeezerClientSecret}&response_type=token";

            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestUri);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "application/json";
            byte[] byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = byteVersion.Length;
            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
            stream.Close();

            try
            {
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream() ?? throw new Exception()))
                {
                    string responseText = await reader.ReadToEndAsync();

                    token = responseText.Split('&')[0].Remove(0, 13);

                    System.Console.WriteLine($"Token: {token}");
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                { }

                if (e.Response is HttpWebResponse r)
                {
                    Console.WriteLine($"HTTP: {response.StatusCode}");
                    using (StreamReader reader = new StreamReader(r.GetResponseStream() ?? throw new Exception()))
                    {
                        string responseText = await reader.ReadToEndAsync();
                        Console.WriteLine(responseText);
                    }
                }
            }
        }

        public static async Task<Track> LastTrack()
        {
            string requestUri = $"{lastTrackEndpoint}?access_token={token}";

            HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            userinfoRequest.Method = "GET";
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
            using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream() ?? throw new Exception()))
            {
                string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();

                if (userinfoResponseText.Contains("Invalid OAuth access token."))
                {
                    await Authenticate();
                    return await LastTrack();
                }

                History tracks = JsonConvert.DeserializeObject<History>(userinfoResponseText);

                return tracks.Tracks[0];
            }
        }

        public static async Task<User> User()
        {
            string requestUri = $"{userEndpoint}?access_token={token}";

            HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            userinfoRequest.Method = "GET";
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();

            using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream() ?? throw new Exception()))
            {
                string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();

                if (userinfoResponseText.Contains("Invalid OAuth access token."))
                {
                    await Authenticate();
                    return await User();
                }

                User user = JsonConvert.DeserializeObject<User>(userinfoResponseText);

                return user;
            }
        }
    }
}
