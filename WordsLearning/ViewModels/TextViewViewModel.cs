using Prism.Mvvm;

namespace WordsLearning.ViewModels
{
    class TextViewViewModel : BindableBase
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
    }
}
