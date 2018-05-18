using ReactiveUI;

namespace DeezCord.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string statusText;

        public string StatusText
        {
            get => statusText;
            set => this.RaiseAndSetIfChanged (ref statusText, value);
        }

        public MainWindowViewModel ()
        {
            StatusText = "Status: off";
        }

        public void Login ()
        {
            
        }
    }
}