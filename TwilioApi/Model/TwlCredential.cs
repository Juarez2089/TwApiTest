using System;
using System.Collections.Generic;

namespace TwilioApi.Model;

public partial class TwlCredential
{
    public int CreId { get; set; }

    public string? CreAccount { get; set; }

    public string? CreToken { get; set; }
    public string? CrePhNumber { get; set; }

    public DateTime? CreDt { get; set; }
}
