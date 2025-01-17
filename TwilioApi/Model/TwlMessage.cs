using System;
using System.Collections.Generic;

namespace TwilioApi.Model;

public partial class TwlMessage
{
    public int MsgId { get; set; }

    public DateTime MsgCreationDate { get; set; }

    public string MsgPhoneTo { get; set; } = null!;

    public string? MsgMessage { get; set; }

    public virtual ICollection<TwlMessagesSent> TwlMessagesSents { get; set; } = new List<TwlMessagesSent>();
}
