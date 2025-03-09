using OldPhonePad.Application.Interfaces;
using OldPhonePad.Domain.Entities;
using OldPhonePad.Infrastructure.Data;

namespace OldPhonePad.Application.Services
{
    public class PhonePadService : IPhonePadService
    {
        private readonly PhonePadDecoder _decoder = new(KeyPadMapping.KeyMap);

        public string DecodeInput(string input)
        {
            return _decoder.Decode(input);
        }
    }
}
