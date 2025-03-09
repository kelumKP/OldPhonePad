using System.Text;

namespace OldPhonePad.Domain.Entities
{
    public class PhonePadDecoder
    {
        private readonly Dictionary<string, string> _keyMap;

        public PhonePadDecoder(Dictionary<string, string> keyMap)
        {
            _keyMap = keyMap;
        }

        public string Decode(string input)
        {
            if (input.EndsWith("#"))
                input = input[..^1];

            var result = new StringBuilder();
            var buffer = new StringBuilder();

            foreach (var ch in input)
            {
                if (ch == '*')
                {
                    if (buffer.Length > 0)
                    {
                        buffer.Clear();
                    }
                    else if (result.Length > 0)
                    {
                        result.Remove(result.Length - 1, 1);
                    }
                    continue;
                }

                if (ch == ' ')
                {
                    if (buffer.Length > 0 && _keyMap.TryGetValue(buffer.ToString(), out var letter))
                        result.Append(letter);
                    buffer.Clear();
                    continue;
                }

                if (buffer.Length > 0 && buffer[0] != ch)
                {
                    if (_keyMap.TryGetValue(buffer.ToString(), out var letter))
                        result.Append(letter);
                    buffer.Clear();
                }

                buffer.Append(ch);
            }

            if (buffer.Length > 0 && _keyMap.TryGetValue(buffer.ToString(), out var lastLetter))
                result.Append(lastLetter);

            return result.ToString();
        }
    }
}