using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScriptureMemorizer
{
    
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        private static readonly Regex _splitRegex = new Regex(@"\s+");

        public Scripture(Reference reference, string fullText)
        {
            _reference = reference;
            var tokens = _splitRegex.Split(fullText.Trim());
            _words = tokens.Select(t => new Word(t)).ToList();
        }

        public string GetDisplayText()
        {
            return string.Join(" ", _words.Select(w => w.GetDisplay()));
        }

        public bool AllHidden()
        {
            return _words.All(w => w.IsHidden());
        }

        public int HideRandomWords(int count, Random rng)
        {
            var unhidden = _words
                .Select((w, i) => new { Word = w, Index = i })
                .Where(x => !x.Word.IsHidden())
                .ToList();

            if (!unhidden.Any()) return 0;

            int toHide = Math.Min(count, unhidden.Count);

            for (int i = 0; i < toHide; i++)
            {
                int pickIndex = rng.Next(unhidden.Count);
                unhidden[pickIndex].Word.Hide();
                unhidden.RemoveAt(pickIndex);
            }

            return toHide;
        }
    }
}
