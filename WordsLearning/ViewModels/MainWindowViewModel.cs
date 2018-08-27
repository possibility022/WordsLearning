using Notifications.Wpf;
using Prism.Mvvm;
using System.Windows.Media;

namespace WordsLearning.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        WordManager _wordManager;
        NotificationManager _notificationManager = new NotificationManager();

        private Brush _translatedBackground = Brushes.White;
        public Brush TranslatedBackground
        {
            get { return _translatedBackground; }
            set { SetProperty(ref _translatedBackground, value); }
        }

        private string _wordToTranslate;
        public string WordToTranslate
        {
            get { return _wordToTranslate; }
            set { SetProperty(ref _wordToTranslate, value); }
        }

        private string _translated;
        public string Translated
        {
            get { return _translated; }
            set { SetProperty(ref _translated, value); }
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
            _wordManager.OnNewWord += _wordManager_OnNewWord;
        }

        internal void WindowLoaded()
        {
            _wordManager.SetNewWord();
        }

        private void _wordManager_OnNewWord(object sender, Word e)
        {
            var notification = new NotificationContent()
            {
                Message = e.Polish,
                Title = "Nowe słówko",
                Type = NotificationType.Information
            };

            WordToTranslate = e.Polish;
            Translated = string.Empty;

            _notificationManager.Show(notification);
        }

        internal void ShowSolution()
        {
            if (Translated == _wordManager.Word.English)
                TranslatedBackground = Brushes.Green;
            else
                TranslatedBackground = Brushes.Red;


                ResultMessage = _wordManager.Word.English;
        }

        internal void WindowClosing()
        {
            _notificationManager.CloseWindow();
        }
    }
}
