using System;
using System.Threading.Tasks;

using ReactiveUI;

namespace DeezCord.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _statusText;
        private string _userInfo;
        private bool _isLoginButtonVisible = true;
        private bool _isTurnOnButtonVisible;
        private string _song;

        private Presence presence;

        public string StatusText
        {
            get => _statusText;
            set => this.RaiseAndSetIfChanged (ref _statusText, value);
        }

        public string UserInfo
        {
            get => _userInfo;
            set => this.RaiseAndSetIfChanged (ref _userInfo, value);
        }

        public bool IsLoginButtonVisible
        {
            get => _isLoginButtonVisible;
            set => this.RaiseAndSetIfChanged (ref _isLoginButtonVisible, value);
        }

        public bool IsTurnOnButtonVisible
        {
            get => _isTurnOnButtonVisible;
            set => this.RaiseAndSetIfChanged (ref _isTurnOnButtonVisible, value);
        }

        public string Song
        {
            get => _song;
            set => this.RaiseAndSetIfChanged (ref _song, value);
        }

        public MainWindowViewModel ()
        {
            StatusText = "Status: off";
            presence = new Presence ();
        }

        public async Task Login ()
        {
            await DeezerAPI.Authenticate ();

            User user = await DeezerAPI.User ();
            UserInfo = $"Deezer user: {user.Name}";

            IsLoginButtonVisible = false;
            IsTurnOnButtonVisible = true;
        }

        public async Task StartPresence ()
        {
            IsTurnOnButtonVisible = false;

            while (true)
            {
                Track t = await DeezerAPI.LastTrack ();
                presence.UpdatePresence (t);
                StatusText = "Status: running";
                Song = $"{t.Title} by {t.Artist.Name} - {t.Album.Title}";
                
                await Task.Delay (30000);
            }
        }
    }
}