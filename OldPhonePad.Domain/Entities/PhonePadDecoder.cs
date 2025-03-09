using System.Text;

namespace OldPhonePad.Domain.Entities
{
    public class PhonePadDecoder
    {
        private readonly Dictionary<string, string> _keyMap;

        public PhonePadDecoder(Dictionary<string, string> keyMap)
        {
            _keyMap = keyMap ?? throw new ArgumentNullException(nameof(keyMap));
        }

        public string Decode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove the trailing '#' if present
            if (input.EndsWith("#"))
                input = input[..^1];

            var result = new StringBuilder();
            var buffer = new StringBuilder();

            foreach (var ch in input)
            {
                switch (ch)
                {
                    case '*':
                        HandleBackspace(buffer, result);
                        break;
                    case ' ':
                        FlushBuffer(buffer, result);
                        break;
                    default:
                        ProcessCharacter(ch, buffer, result);
                        break;
                }
            }

            // Flush any remaining characters in the buffer
            FlushBuffer(buffer, result);

            return result.ToString();
        }

        private void HandleBackspace(StringBuilder buffer, StringBuilder result)
        {
            if (buffer.Length > 0)
            {
                buffer.Clear();
            }
            else if (result.Length > 0)
            {
                result.Remove(result.Length - 1, 1);
            }
        }

        private void FlushBuffer(StringBuilder buffer, StringBuilder result)
        {
            if (buffer.Length > 0 && _keyMap.TryGetValue(buffer.ToString(), out var letter))
            {
                result.Append(letter);
                buffer.Clear();
            }
        }

        private void ProcessCharacter(char ch, StringBuilder buffer, StringBuilder result)
        {
            if (buffer.Length > 0 && buffer[0] != ch)
            {
                FlushBuffer(buffer, result);
            }
            buffer.Append(ch);
        }
    }
}