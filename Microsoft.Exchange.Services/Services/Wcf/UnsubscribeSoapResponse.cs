using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CDB RID: 3291
	[MessageContract(IsWrapped = false)]
	public class UnsubscribeSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400306B RID: 12395
		[MessageBodyMember(Name = "UnsubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UnsubscribeResponse Body;
	}
}
