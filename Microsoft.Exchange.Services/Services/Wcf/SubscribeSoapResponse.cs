using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CD9 RID: 3289
	[MessageContract(IsWrapped = false)]
	public class SubscribeSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003069 RID: 12393
		[MessageBodyMember(Name = "SubscribeResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SubscribeResponse Body;
	}
}
