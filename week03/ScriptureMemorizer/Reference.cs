using System;

namespace ScriptureMemorizer
{
    // Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6")
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int? _endVerse;

        
        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = null;
        }

        
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            if (endVerse < startVerse)
            {
                throw new ArgumentException("End verse must be >= start verse.");
            }

            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public override string ToString()
        {
            if (_endVerse.HasValue)
            {
                return $"{_book} {_chapter}:{_startVerse}-{_endVerse.Value}";
            }
            else
            {
                return $"{_book} {_chapter}:{_startVerse}";
            }
        }
    }
}
