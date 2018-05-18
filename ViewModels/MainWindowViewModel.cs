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

        public MainWindowViewModel ()
        {
            StatusText = "Status: off";
        }

        public async Task Login ()
        {
            await DeezerAPI.Authenticate ();

            User user = await DeezerAPI.User ();
            UserInfo = $"Deezer user: {user.Name}";

            IsLoginButtonVisible = false;
            IsTurnOnButtonVisible = true;
        }
    }
}