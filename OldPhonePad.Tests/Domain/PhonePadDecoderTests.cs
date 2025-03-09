using OldPhonePad.Domain.Entities;

namespace OldPhonePad.Tests.Domain
{
    public class PhonePadDecoderTests
    {
        private readonly Dictionary<string, string> _keyMap = new()
        {
            {"2", "A"}, {"22", "B"}, {"222", "C"},
            {"3", "D"}, {"33", "E"}, {"333", "F"},
            {"4", "G"}, {"44", "H"}, {"444", "I"},
            {"5", "J"}, {"55", "K"}, {"555", "L"},
            {"6", "M"}, {"66", "N"}, {"666", "O"},
            {"7", "P"}, {"77", "Q"}, {"777", "R"}, {"7777", "S"},
            {"8", "T"}, {"88", "U"}, {"888", "V"},
            {"9", "W"}, {"99", "X"}, {"999", "Y"}, {"9999", "Z"}
        };

        [Theory]
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("2 22 222#", "ABC")]
        [InlineData("7777 9999#", "SZ")]
        public void Decode_ValidInput_ReturnsExpectedOutput(string input, string expected)
        {
            var decoder = new PhonePadDecoder(_keyMap);
            var result = decoder.Decode(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Decode_EmptyInput_ReturnsEmptyString()
        {
            var decoder = new PhonePadDecoder(_keyMap);
            var result = decoder.Decode("#");
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decode_Backspace_RemovesPreviousCharacter()
        {
            var decoder = new PhonePadDecoder(_keyMap);
            var result = decoder.Decode("227*3#");
            Assert.Equal("BD", result);
        }
    }
}