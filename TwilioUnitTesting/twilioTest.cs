using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TwilioApi.Controllers;
using TwilioApi.DTO;
using TwilioApi.Model;
using TwilioApi.Repository;
using TwilioApi.Repository.Interfaces;

namespace TwilioUnitTesting
{
    public class twilioTest
    {
        private readonly SsmTwilioController _TwilioControler; 
        private readonly Moq.Mock<ITwilioRepository> _Mockrepo;
        private readonly Moq.Mock<IMapper> _Mockmapper;

        private readonly IMapper _mapper;

        public twilioTest()
        {
            _Mockmapper = new Moq.Mock<IMapper>();
            _Mockrepo = new Moq.Mock<ITwilioRepository>();
            _TwilioControler = new SsmTwilioController(_Mockrepo.Object, _Mockmapper.Object);
        }
        [Fact]
        public async Task Test1()
        {


            var LtwlMessage = new List<TwlMessage>();
            LtwlMessage.Add(new TwlMessage { MsgCreationDate = DateTime.Now, MsgId = 1, MsgMessage = "prueba", MsgPhoneTo = "46538" } );
            LtwlMessage.Add(new TwlMessage { MsgCreationDate = DateTime.Now, MsgId = 1, MsgMessage = "prueba2", MsgPhoneTo = "46538" });

            _Mockrepo.Setup(r => r.GetMessagesBD()).ReturnsAsync(LtwlMessage);

            //Setting automapper
            _Mockmapper.Setup(m => m.Map<List<TwlMessageDTO>>(It.IsAny<List<TwlMessage>>()))
                .Returns((List<TwlMessage> source) =>
                    source.Select(msg => new TwlMessageDTO
                    {
                        MsgId = msg.MsgId,
                        MsgMessage = msg.MsgMessage,
                        MsgPhoneTo = msg.MsgPhoneTo,
                        MsgCreationDate = msg.MsgCreationDate
                    }).ToList()
                );

            //Get info from controller.
            var resp = await _TwilioControler.GetMessages();
            //Check if the response was an object Ok
            var result = Assert.IsType<OkObjectResult>(resp);
            //Check if the response was a list DTO
            var dtoResult = Assert.IsType<List<TwlMessageDTO>>(result.Value);
            //check if the rows result is the same in the list.
            Assert.Equal(2, dtoResult.Count);



        }

        [Fact]
        public async Task Test2()
        {
            // setting
            var messageDTO = new TwlMessageDTO { MsgCreationDate = DateTime.Now, MsgId = 1, MsgMessage = "prueba", MsgPhoneTo = "46538" };
            var message = new TwlMessage { MsgCreationDate = DateTime.Now, MsgId = 1, MsgMessage = "prueba", MsgPhoneTo = "46538" };

            _Mockrepo.Setup(x => x.ProcessSmm(It.IsAny<TwlMessage>())).ReturnsAsync("ItWorks");

             var result = await _TwilioControler.CreateMessage(messageDTO);

             var okResult = Assert.IsType<OkObjectResult>(result);
   
            var returnedValue = okResult.Value.ToString();

            //Check if the return value its the same.
            Assert.Equal("ItWorks", returnedValue); 

        }
    }
}