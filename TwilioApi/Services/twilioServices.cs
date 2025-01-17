using TwilioApi.Model;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using TwilioApi.DTO;

namespace TwilioApi.Services
{
    public class twilioServices
    {
        public static async Task<MessageResource> ProcessMessage(TwlCredential twlCredential, TwlMessage msg)
        {
            try
            {
                TwilioClient.Init(twlCredential.CreAccount, twlCredential.CreToken);

                var message = await MessageResource.CreateAsync(
                    to: new PhoneNumber(msg.MsgPhoneTo),
                    from: new PhoneNumber(twlCredential.CrePhNumber),
                    body: msg.MsgMessage
                ); 
                return message;  
            }
            catch (Exception)
            {

                throw;
            }
   

           
        }
    }
}
