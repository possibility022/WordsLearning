using Notifications.Wpf;
using Prism.Mvvm;

namespace WordsLearning.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        WordManager _wordManager;
        NotificationManager _notificationManager = new NotificationManager();

        private string _worldToTranslate;
        public string WorldToTranslate
        {
            get { return _worldToTranslate; }
            set { SetProperty(ref _worldToTranslate, value); }
        }

        private string _translated;
        public string Translated
        {
            get { return _translated; }
            set { SetProperty(ref _translated, value); }
        }

        internal void ShowSolution()
        {
            ResultMessage = _wordManager.Word.English;
        }

        private string _resultMessage;
        public string ResultMessage
        {
            get { return _resultMessage; }
            set { SetProperty(ref _resultMessage, value); }
        }

        public MainWindowViewModel()
        {
            _wordManager = new WordManager();
            _wordManager.OnNewWord += _worldManager_OnNewWord;
        }

        internal void WindowLoaded()
        {
            _wordManager.SetNewWorld();
        }

        private void _worldManager_OnNewWord(object sender, Word e)
        {
            var notification = new NotificationContent()
            {
                Message = e.Polish,
                Title = "Nowe słówko",
                Type = NotificationType.Information
            };

            WorldToTranslate = e.Polish;
            Translated = string.Empty;

            _notificationManager.Show(notification);
        }

        internal void WindowClosing()
        {
            _notificationManager.CloseWindow();
        }
    }
}
