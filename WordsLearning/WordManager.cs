using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WordsLearning
{
    class WordManager
    {
        public event EventHandler<Word> OnNewWord;

        DateTime _studyStarted = new DateTime(2018, 8, 25);
        IReadOnlyCollection<Word> _words;
        public IReadOnlyList<Word> WordsUntilThisWeek { get; private set; }
        public IReadOnlyList<Word> NewWords { get; private set; }

        private Random random = new Random((int)DateTime.Now.Ticks);

        private Timer _timer;

        private bool _randomFromNew = true;

        public Word Word { get; set; }

        public WordManager()
        {
            _words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("words.json"));
            _timer = new Timer(10 * 60 * 1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();

            SetThisWeakWords();
        }

        private void SetThisWeakWords()
        {
            int week = (DateTime.Now - _studyStarted).Days / 7;
            NewWords = new List<Word>(_words.Skip(week * 10).Take(20));
            WordsUntilThisWeek = new List<Word>(_words.Take(week * 20));
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            SetNewWord();
        }

        public void SetNewWord()
        {
            if (!_randomFromNew && WordsUntilThisWeek.Count > 0)
            {
                var s = random.Next(0, WordsUntilThisWeek.Count);
                Word = WordsUntilThisWeek[s];
            }
            else
            {
                var s = random.Next(0, NewWords.Count);
                Word = NewWords[s];
            }

            _randomFromNew = !_randomFromNew;

            OnNewWord?.Invoke(this, Word);
        }
    }
}
