using System;
using System.Collections.Generic;

namespace TwilioApi.Model;

public partial class TwlMessagesSent
{
    public int SmsgId { get; set; }

    public int MsgId { get; set; }

    public string SmsgTwilioCode { get; set; }

    public string? SmsgTwilioMsgStatus { get; set; }
    public virtual TwlMessage? Msg { get; set; }

}
