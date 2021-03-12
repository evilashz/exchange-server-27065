using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000CFC RID: 3324
	[MessageContract(IsWrapped = false)]
	public class DisconnectPhoneCallSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400308E RID: 12430
		[MessageBodyMember(Name = "DisconnectPhoneCall", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public DisconnectPhoneCallRequest Body;
	}
}
