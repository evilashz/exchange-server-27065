using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D46 RID: 3398
	[MessageContract(IsWrapped = false)]
	public class MarkAsJunkSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030D8 RID: 12504
		[MessageBodyMember(Name = "MarkAsJunk", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MarkAsJunkRequest Body;
	}
}
