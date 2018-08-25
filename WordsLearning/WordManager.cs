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
        IReadOnlyList<Word> _wordsUntilThisWeek;
        IReadOnlyList<Word> _newWords;

        private Random random = new Random((int)DateTime.Now.Ticks);

        private Timer _timer;

        private bool _randomFromNew = true;

        public Word Word { get; set; }

        public WordManager()
        {
            _words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("words.json"));
            _timer = new Timer(15 * 60 * 1000);
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            
            SetThisWeakWorlds();
        }

        private void SetThisWeakWorlds()
        {
            int week = (DateTime.Now - _studyStarted).Days / 7;
            _newWords = new List<Word>(_words.Skip(week * 10).Take(10));
            _wordsUntilThisWeek = new List<Word>(_words.Take(week * 10));
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            SetNewWorld();
        }

        public void SetNewWorld()
        {
            if (!_randomFromNew && _wordsUntilThisWeek.Count > 0)
            {
                var s = random.Next(0, _wordsUntilThisWeek.Count);
                Word = _wordsUntilThisWeek[s];
            }
            else
            {
                var s = random.Next(0, _newWords.Count);
                Word = _newWords[s];
            }

            _randomFromNew = !_randomFromNew;

            OnNewWord?.Invoke(this, Word);
        }
    }
}
