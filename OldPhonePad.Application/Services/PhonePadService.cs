using OldPhonePad.Application.Interfaces;
using OldPhonePad.Domain.Entities;

namespace OldPhonePad.Application.Services
{
    public class PhonePadService : IPhonePadService
    {
        private readonly PhonePadDecoder _decoder;

        public PhonePadService(PhonePadDecoder decoder)
        {
            _decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
        }

        public string DecodeInput(string input)
        {
            return _decoder.Decode(input);
        }
    }
}