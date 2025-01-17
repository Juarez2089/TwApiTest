using TwilioApi.DTO;
using TwilioApi.Model;

namespace TwilioApi.Repository.Interfaces
{
    public interface ITwilioRepository
    {
        Task<string> ProcessSmm(TwlMessage message);
        Task<IEnumerable<TwlMessage>> GetMessagesBD();
    }
}
