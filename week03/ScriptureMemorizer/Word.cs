using System.Text;

namespace ScriptureMemorizer
{
    
    public class Word
    {
        private string _original;
        private bool _hidden;

        public Word(string token)
        {
            _original = token;
            _hidden = false;
        }

        public void Hide()
        {
            _hidden = true;
        }

        public bool IsHidden()
        {
            return _hidden || !_original.Any(char.IsLetter);
        }

        public string GetDisplay()
        {
            if (!_hidden) return _original;

            var sb = new StringBuilder(_original.Length);
            foreach (char c in _original)
            {
                sb.Append(char.IsLetter(c) ? '_' : c);
            }
            return sb.ToString();
        }
    }
}
