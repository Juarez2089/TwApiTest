using System;
using System.Collections.Generic;
using TwilioApi.Model;

namespace TwilioApi.DTO;

public partial class TwlMessageDTO
{
    public int MsgId { get; set; }

    public DateTime MsgCreationDate { get; set; }

    public string MsgPhoneTo { get; set; } = null!;

    public string? MsgMessage { get; set; }
     

}
