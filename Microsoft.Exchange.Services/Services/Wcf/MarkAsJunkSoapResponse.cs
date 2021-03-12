using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D47 RID: 3399
	[MessageContract(IsWrapped = false)]
	public class MarkAsJunkSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030D9 RID: 12505
		[MessageBodyMember(Name = "MarkAsJunkResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public MarkAsJunkResponse Body;
	}
}
