using TwilioApi.DBContexts;
using TwilioApi.Model;

using Microsoft.EntityFrameworkCore;
using TwilioApi.Repository.Interfaces;
using TwilioApi.Services;
 

namespace TwilioApi.Repository
{
    
    public class TwilioRepository:ITwilioRepository
    {
        private readonly TwilioDbContext _dbContext;
        
        public TwilioRepository(TwilioDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }

        public async Task<IEnumerable<TwlMessage>> GetMessagesBD()
        {

            try
            {
                return await _dbContext.TwlMessages.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
        //Obterner cred
        //Consumir api twi


        public async Task<string> ProcessSmm(TwlMessage message)
        {
            string msg;
            
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //save header
                    _dbContext.TwlMessages.Add(message);
                    _dbContext.SaveChanges();

                    //send twilio msg
                    var credentials = _dbContext.TwlCredentials.FirstOrDefault();
                    var msgProcessed = await twilioServices.ProcessMessage(credentials, message);
                    //Detail
                    var msgDetail = new TwlMessagesSent { MsgId= message.MsgId,SmsgTwilioCode= msgProcessed.Sid,SmsgTwilioMsgStatus=msgProcessed.Status.ToString()  };
                    _dbContext.TwlMessagesSents.Add(msgDetail);
                    _dbContext.SaveChanges();
                    await transaction.CommitAsync();
                    msg = "Ok";
            }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    msg = ex.Message;
                }
            }
            return msg;   
     
        }
    }
}
